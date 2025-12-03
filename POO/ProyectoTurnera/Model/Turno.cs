using System;

namespace ProyectoTurnera.Model
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public Medico Medico { get; set; }

        public Consultorio Consultorio { get; set; }

        public Especialidad Epecialidad { get; set; }

        public Prestador PrestadorMedico { get; set; }

        public double PrecioCoosulta { get; set; }

        public Paciente Paciente { get; set; }

        public Prestador MrestadoPacienteo { get; set; }

        public int Estado { get; set; } // 0: Pendiente, 1: Confirmado, 2: Cancelado    

        public Turno() { } // Para binding

        public Turno(int id, DateTime fecha, Medico medico, Consultorio consultorio, Especialidad especialidad, Prestador prestadormedico,
            double precioconsulta, Paciente paciente, Prestador prestadorpaciente, int estado)
        {
            Id = id;
            Fecha = fecha;
            Medico = medico;
            Consultorio = consultorio;
            Epecialidad = especialidad;
            PrestadorMedico = prestadormedico;
            PrecioCoosulta = precioconsulta;
            Paciente = paciente;
            MrestadoPacienteo = prestadorpaciente;
            Estado = estado;

        }

        public override string ToString() => Fecha.ToString();

    }
}