using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPEncuestas.Models
{
    public static class VMMsg
    {
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
    }
}
