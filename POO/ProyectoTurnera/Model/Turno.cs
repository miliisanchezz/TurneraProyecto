using System;

namespace ProyectoTurnera.Model
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public Medico Medico { get; set; }

        public Consultorio Consultorio { get; set; }

        public Especialidad Especialidad { get; set; }

        public Prestador PrestadorMedico { get; set; }

        public double PrecioConsulta { get; set; }

        public Paciente Paciente { get; set; }

        public Prestador PrestadorPaciente { get; set; }

        public int? Estado { get; set; } // 0: Pendiente, 1: Confirmado, 2: Cancelado    

        public Turno() { } // binding

        public Turno(int id, DateTime fecha, Medico medico, Consultorio consultorio, Especialidad especialidad, Prestador prestadormedico,
            double precioconsulta, Paciente paciente = null, Prestador prestadorpaciente = null, int? estado = null)
        {
            Id = id;
            Fecha = fecha;
            Medico = medico;
            Consultorio = consultorio;
            Especialidad = especialidad;
            PrestadorMedico = prestadormedico;
            PrecioConsulta = precioconsulta;
            Paciente = paciente;
            PrestadorPaciente = prestadorpaciente;
            Estado = estado;

        }

        public override string ToString() => Fecha.ToString();

    }
}