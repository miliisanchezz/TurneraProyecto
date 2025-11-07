using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Turnera.Controlador
{
    public class Turno
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public Consultorio Consultorio { get; set; }
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }

        public Turno() { }

        public Turno(int id, Medico medico, Paciente paciente, Consultorio consultorio, DateTime fecha)
        {
            Id = id;
            Medico = medico;
            Paciente = paciente;
            Consultorio = consultorio;
            Fecha = fecha;

            if (paciente.ObraSocial != null && paciente.ObraSocial == medico.Especialidad)
                Monto = medico.PrecioConsulta * 0.5;
            else
                Monto = medico.PrecioConsulta;
        }

        public string MostrarDatos()
        {
            return $"{Fecha:g} - Dr. {Medico.Apellido}, {Medico.Especialidad} - Paciente: {Paciente.Apellido} - ${Monto}";
        }
    }
}
