using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurneraMedica.Modelo
{
    public class Turno
    {
        public int Id { get; set; }

        public System.DateTime Fecha { get; set; }
        public System.TimeSpan Hora { get; set; }

        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int ConsultorioId { get; set; }

        public decimal PrecioFinal { get; set; }
    }
}

