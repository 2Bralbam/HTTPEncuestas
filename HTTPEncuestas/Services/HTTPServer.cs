using HTTPEncuestas.Models;
using HTTPEncuestas.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;

namespace HTTPEncuestas.Services
{
    public class HTTPServer
    {
        HttpListener Server = new();
        public string TituloEncuesta { get; set; } = null!;
        public HTTPServer()
        {
            Server.Prefixes.Add("http://*:22024/");
            VMMsg.SetServerStatus += (s, e) =>
            {
                if (e)
                {
                    StartServer();
                }
                else
                {
                    StopServer();
                }
            };
        }
        public bool StartServer()
        {
            if (!Server.IsListening)
            {
                Server.Start();
                new Thread(Escuchar)
                { IsBackground = true }.Start();
                return true;
            }
            else
            {
                return false;
            }
            return true;
        }

        void Escuchar()
        {
            try
            {
                while (Server.IsListening)
                {
                    var context = Server.GetContext();
                    if (context.Request.Url != null)
                    {
                        if (context.Request.Url.LocalPath == "/")
                        {
                            //VMMsg.ListaDatos.First().Promedio += 10;
                            //VMMsg.ListaDatos.First().Tamaño += 10;
                            //VMMsg.UpdateGraficas();
                            //string Pagina = File.ReadAllText("Assets/vistasencuesta/encuesta.html");
                            var resourceUri = new Uri("/Assets/vistasencuesta/nombre.html", UriKind.Relative);
                            string Pagina = new StreamReader(Application.GetResourceStream(resourceUri).Stream).ReadToEnd();
                            
                            var bufferPagina = Encoding.UTF8.GetBytes(Pagina);
                            context.Response.ContentLength64 = bufferPagina.Length;
                            context.Response.OutputStream.Write(bufferPagina, 0, bufferPagina.Length);
                            context.Response.StatusCode = 200;

                        }
                        else if (context.Request.Url.LocalPath == "/encuesta")
                        {
                            var resourceUri = new Uri("/Assets/vistasencuesta/encuesta.html", UriKind.Relative);
                            string Pagina = new StreamReader(Application.GetResourceStream(resourceUri).Stream).ReadToEnd();
                            Pagina = Pagina.Replace("{{TituloEncuesta}}", TituloEncuesta);
                            string PreguntaPreset = "<label class=\"pregunta\" >{{Pregunta}}</label>\r\n            <ul class=\"respuesta\">\r\n              <li>\r\n                <input type=\"radio\" name=\"respuesta1\" value=\"5\">\r\n                <label>Muy buena</label>\r\n              </li>\r\n              <li>\r\n                <input type=\"radio\" name=\"respuesta1\" value=\"4\">\r\n                <label>Buena</label>\r\n              </li>\r\n              <li>\r\n                <input type=\"radio\" name=\"respuesta1\" value=\"3\">\r\n                <label>Neutral</label>\r\n              </li>\r\n              <li>\r\n                <input type=\"radio\" name=\"respuesta1\" value=\"2\">\r\n                <label>Mala</label>\r\n              </li>\r\n              <li>\r\n                <input type=\"radio\" name=\"respuesta1\" value=\"1\">\r\n                <label>Muy mala</label>\r\n              </li>\r\n            </ul>}";
                            string Preguntas = "";
                            int i = 0;
                            foreach (var item in VMMsg.ListaDatos)
                            {
                                i++;
                                Preguntas += new String(new String(PreguntaPreset).Replace("name=\"respuesta1\"", $"name=\"R{i}\"").Replace("{{Pregunta}}", item.Pregunta));

                            }
                            Pagina = Pagina.Replace("{{Preguntas}}", Preguntas);
                            var bufferPagina = Encoding.UTF8.GetBytes(Pagina);
                            context.Response.ContentLength64 = bufferPagina.Length;
                            context.Response.OutputStream.Write(bufferPagina, 0, bufferPagina.Length);
                            context.Response.StatusCode = 200;
                            context.Response.Close();
                        }
                        else if (context.Request.Url.LocalPath == "/respuesta" && context.Request.HttpMethod == "POST")
                        {
                            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                            {
                                string contenido = reader.ReadToEnd();
                                var a = contenido.Split('&');
                                //VMMsg.ListaDatos.First().Promedio += 10;
                                //VMMsg.ListaDatos.First().Tamaño += 10;
                                //VMMsg.UpdateGraficas();
                                int r = 0;
                                foreach (var p in VMMsg.ListaDatos)
                                {
                                    p.Cantidad++;
                                    int Calif = int.Parse(a[r].Split('=')[1]);
                                    //p.Calificaciones.Append(int.Parse(a[0].Split('=')[1]));
                                    if (p.Calificaciones == null)
                                    {
                                        p.Calificaciones = new float[1];
                                        p.Calificaciones[0] = Calif;
                                    }
                                    else
                                    {
                                        float[] temp = new float[p.Calificaciones.Length + 1];
                                        for (int i = 0; i < p.Calificaciones.Length; i++)
                                        {
                                            temp[i] = p.Calificaciones[i];
                                        }
                                        temp[p.Calificaciones.Length] = Calif;
                                        p.Calificaciones = temp;
                                    }
                                    UltimaRespuesta u = new()
                                    {
                                        NombreUser = "Anonimo",
                                        Promedio = p.prom.ToString("F2")
                                    };
                                    VMMsg.UpdateGraficas();
                                    r++;
                                }
                            }
                            context.Response.StatusCode = 200;
                            context.Response.OutputStream.Close();
                        }

                        else if (context.Request.Url.LocalPath.EndsWith(".css"))
                        {
                            context.Response.ContentType = "text/css";
                            var resourceUri = new Uri("/Assets/vistasencuesta/encuesta2.css", UriKind.Relative);
                            string cssContent = new StreamReader(Application.GetResourceStream(resourceUri).Stream).ReadToEnd();
                            var buffer = Encoding.UTF8.GetBytes(cssContent);
                            context.Response.ContentLength64 = buffer.Length;
                            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                            context.Response.StatusCode = 200;
                            
                            context.Response.Close();
                        }
                        else if (context.Request.Url.LocalPath.EndsWith(".png"))
                        {
                            // Extraer el nombre del archivo de la URL de la petición
                            string fileName = Path.GetFileName(context.Request.Url.LocalPath);

                            // Crear la URI del recurso
                            var resourceUri = new Uri($"/Assets/vistasencuesta/{fileName}", UriKind.Relative);

                            // Leer los bytes de la imagen
                            Stream imageStream = Application.GetResourceStream(resourceUri).Stream;
                            byte[] imageBytes = new byte[imageStream.Length];
                            imageStream.Read(imageBytes, 0, imageBytes.Length);

                            // Configurar la respuesta
                            context.Response.ContentType = "image/png";
                            context.Response.ContentLength64 = imageBytes.Length;
                            context.Response.OutputStream.Write(imageBytes, 0, imageBytes.Length);
                            context.Response.StatusCode = 200;

                            // Cerrar la respuesta
                            context.Response.Close();
                        }
                        else
                        {
                            context.Response.StatusCode = 404;
                            context.Response.Close();
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        context.Response.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en el servidor");
            }
        }

        public bool StopServer()
        {
            if (Server.IsListening)
            {
                Server.Stop();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
