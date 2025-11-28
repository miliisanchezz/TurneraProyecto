using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTurnera.Controlador
{
    internal class Turno
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public Consultorio Consultorio { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }

    }
}
