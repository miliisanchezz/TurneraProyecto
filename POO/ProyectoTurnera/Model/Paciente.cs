using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace ProyectoTurnera.Model
{
    public class Paciente : Persona
    {
        public Prestador Prestador { get; set; }
        public string Telefono { get; set; }

        // Esta propiedad es SOLO para el binding en la grilla
        [DisplayName("Prestador")]
        public string PrestadorNombre => Prestador?.Nombre ?? "";
        [Browsable(false)]
        public int PrestadorId
        {
            get => Prestador?.Id ?? 0;
            set => Prestador = Paciente.PrestadoresDisponibles?.FirstOrDefault(p => p.Id == value);
        }
        public static List<Prestador> PrestadoresDisponibles { get; set; } = new List<Prestador>();

        public Paciente() : base() { } // Necesario para binding

        public Paciente(int id, string nombre, string apellido, int dni, string password,
                        Prestador prestador, string telefono)
            : base(id, nombre, apellido, dni, password)
        {
            Prestador = prestador;
            Telefono = telefono;
        }
    }
}