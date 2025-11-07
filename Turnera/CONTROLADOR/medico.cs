using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnera.Controlador
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Especialidad { get; set; }
        public string Matricula { get; set; }
        public double PrecioConsulta { get; set; }

        public List<Turno> Turnos { get; set; } = new List<Turno>();

        public Medico() { }

        public Medico(int id, string nombre, string apellido, string dni, string especialidad, string matricula, double precio)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Especialidad = especialidad;
            Matricula = matricula;
            PrecioConsulta = precio;
        }

        public string MostrarDatos()
        {
            return $"{Nombre} {Apellido} - {Especialidad} (Matrícula: {Matricula})";
        }

        public double CalcularRecaudacion(DateTime desde, DateTime hasta)
        {
            return Turnos
                .Where(t => t.Fecha >= desde && t.Fecha <= hasta)
                .Sum(t => t.Monto);
        }

        public int ContarConsultas(DateTime desde, DateTime hasta)
        {
            return Turnos.Count(t => t.Fecha >= desde && t.Fecha <= hasta);
        }
    }
}

