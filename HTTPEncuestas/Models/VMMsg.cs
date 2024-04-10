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
        public static event EventHandler<string>? CambioDeVista;
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
    }
    public class GraficaModel
    {
        public int Tamaño { get; set; }
        public string Pregunta { get; set; } = null!;
        public float Promedio { get; set; }
    }
}
