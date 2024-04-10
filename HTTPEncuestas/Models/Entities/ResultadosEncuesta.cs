using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPEncuestas.Models.Entities
{
    public class ResultadosEncuesta
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal PromedioSatisfaccion { get; set; }
    }
}
