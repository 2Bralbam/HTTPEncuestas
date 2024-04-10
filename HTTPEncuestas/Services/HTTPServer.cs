using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HTTPEncuestas.Services
{
    public class HTTPServer
    {
        HttpListener Server = new();
        public string TituloEncuesta { get; set; } = null!;
        public HTTPServer()
        {
            Server.Prefixes.Add("http://*:1234/");   
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
        }

        void Escuchar()
        {
            while (Server.IsListening)
            {
                var context = Server.GetContext();
                string Pagina = File.ReadAllText("Assets/Pagina.html");
                var bufferPagina = Encoding.UTF8.GetBytes(Pagina);
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
