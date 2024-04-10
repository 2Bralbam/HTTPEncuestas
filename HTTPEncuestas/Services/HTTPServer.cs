using HTTPEncuestas.Models;
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
                    if(context.Request.Url != null)
                    {
                        if(context.Request.Url.LocalPath == "")
                        {
                            string Pagina = File.ReadAllText("Assets/Index.html");
                            var bufferPagina = Encoding.UTF8.GetBytes(Pagina);
                            context.Response.ContentLength64 = bufferPagina.Length;
                            context.Response.OutputStream.Write(bufferPagina, 0, bufferPagina.Length);
                            context.Response.StatusCode = 200;
                            context.Response.Close();
                        }
                        else if(context.Request.Url.LocalPath == "/encuesta")
                        {
                            string Pagina = File.ReadAllText("Assets/Encuesta.html");
                            var bufferPagina = Encoding.UTF8.GetBytes(Pagina);
                            context.Response.ContentLength64 = bufferPagina.Length;
                            context.Response.OutputStream.Write(bufferPagina, 0, bufferPagina.Length);
                            context.Response.StatusCode = 200;
                            context.Response.Close();
                        }
                        else if(context.Request.Url.LocalPath == "/encuesta/respuesta" && context.Request.HttpMethod == "POST")
                        {
                            byte[] buffer = new byte[context.Request.ContentLength64];
                            context.Request.InputStream.Read(buffer, 0, buffer.Length);
                            string datos = Encoding.UTF8.GetString(buffer);
                            context.Response.StatusCode = 200;
                            var diccionario = HttpUtility.ParseQueryString(datos);
                            string Pagina = File.ReadAllText("Assets/PaginaAgradecimiento.html");
                            var bufferPagina = Encoding.UTF8.GetBytes(Pagina);
                            context.Response.ContentLength64 = bufferPagina.Length;
                            context.Response.OutputStream.Write(bufferPagina, 0, bufferPagina.Length);
                            context.Response.StatusCode = 200;
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error en el servidor");
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
