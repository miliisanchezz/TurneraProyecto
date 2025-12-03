using System;

namespace ProyectoTurnera.Model
{
    public class Medico : Persona
    {
        public Especialidad Especialidad { get; set; }
        public int Matricula { get; set; }
        public double PrecioConsulta { get; set; }
        public Prestador Prestador { get; set; }

        public Medico(int id, string nombre, string apellido, int dni, string passwordHash,
                      Prestador prestador, Especialidad especialidad, int matricula, double precioConsulta)
            : base(id, nombre, apellido, dni, passwordHash)
        {
            Prestador = prestador ?? throw new ArgumentNullException(nameof(prestador));
            Especialidad = especialidad ?? throw new ArgumentNullException(nameof(especialidad));
            Matricula = matricula;
            PrecioConsulta = precioConsulta >= 0 ? precioConsulta : throw new ArgumentException("El precio no puede ser negativo.");
        }

        public string NombreCompleto => $"{Apellido}, {Nombre}";
        public string InfoCompleta => $"Dr/a. {NombreCompleto} - {Especialidad} (Matrícula {Matricula})";
    }
}