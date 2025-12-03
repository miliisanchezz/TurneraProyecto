using ProyectoTurnera.Controlador;
using System;

namespace ProyectoTurnera.Model
{
    public class Paciente : Persona
    {
        public Prestador Prestador { get; set; }
        public string Telefono { get; set; }

        public Paciente(int id, string nombre, string apellido, int dni, string passwordHash,
                        Prestador prestador, string telefono)
            : base(id, nombre, apellido, dni, passwordHash)
        {
            Prestador = prestador ?? throw new ArgumentNullException(nameof(prestador));
            Telefono = telefono ?? "";
        }
    }
}