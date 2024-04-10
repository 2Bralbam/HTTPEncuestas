using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPEncuestas.Models.Entities
{
    public class PreguntasDTO
    {
        public string Nombre { get; set; } = null!;
        public int[]? Respuestas { get; set; }
    }
}
