using HTTPEncuestas.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPEncuestas.Models
{
    public static class VMMsg
    {
        public static List<GraficaModel> ListaDatos { get; set; } = new();
        public static string TituloEncuesta { get; set; } = null!;
        public static event EventHandler<string>? CambioDeVista;
        public static event Action? UpdateHistorialView;
        public static void OnUpdateHistorialView()
        {
            UpdateHistorialView?.Invoke();
        }
        public static void OnCambioDeVista(string vista)
        {
            CambioDeVista?.Invoke(null, vista);
        }
        public static event EventHandler<bool>? SetServerStatus;
        public static void OnSetServerStatus(bool status)
        {
            SetServerStatus?.Invoke(null, status);
        }
        public static event Action? UpdateGraficaView;
        public static void UpdateGraficas()
        {
            UpdateGraficaView?.Invoke();
        }
        public static void OnUpdateGraficasLive(List<GraficaModel> Lista)
        {
            UpdateGraficaView?.Invoke();
            ListaDatos = Lista;
        }
        public static event EventHandler<UltimaRespuesta>? UpdateHistorial;
        public static void OnUpdateHistorial(UltimaRespuesta r)
        {
            UpdateHistorial?.Invoke(null, r);
        }
    }
    public class GraficaModel
    {
        public int Tamaño { get {
                return (int)(40 * prom);
            }

        }
        public string Pregunta { get; set; } = null!;
        public string Promedio { get
            {
                return prom.ToString("F2");
            }
        }
        public float prom {
            get
            {
                return Calificaciones?.Average() ?? 0;
            }

        }
        public float[]? Calificaciones { get; set; }
        public int Cantidad { get; set; }
    }
}
