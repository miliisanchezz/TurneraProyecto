using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnera.Controlador
{
    public class Turnera
    {
        public List<Medico> Medicos { get; set; } = new List<Medico>();
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public List<Consultorio> Consultorios { get; set; } = new List<Consultorio>();
        public List<Turno> Turnos { get; set; } = new List<Turno>();

        public void AgregarTurno(Turno turno)
        {
            bool ocupado = Turnos.Any(t =>
                t.Medico.Id == turno.Medico.Id &&
                t.Fecha == turno.Fecha);

            if (ocupado)
                Console.WriteLine("El médico ya tiene un turno en ese horario.");
            else
                Turnos.Add(turno);
        }

        public void ReporteMedico(DateTime desde, DateTime hasta)
        {
            Console.WriteLine("Recaudación por médico:");
            foreach (var medico in Medicos)
            {
                double total = medico.CalcularRecaudacion(desde, hasta);
                int cant = medico.ContarConsultas(desde, hasta);
                Console.WriteLine($"{medico.Nombre} {medico.Apellido}: ${total} ({cant} consultas)");
            }
        }
    }
}

