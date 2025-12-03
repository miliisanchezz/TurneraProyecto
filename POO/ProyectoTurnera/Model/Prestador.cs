using System;

namespace ProyectoTurnera.Model
{
    public class Prestador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Prestador() { } // Necesario para binding

        public Prestador(int id, string nombre)
        {
            Id = id;
            Nombre = nombre?.Trim() ?? throw new ArgumentNullException(nameof(nombre));
        }

        public override string ToString() => Nombre;

    }
}