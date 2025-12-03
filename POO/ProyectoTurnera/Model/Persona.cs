using System;

namespace ProyectoTurnera.Model
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public string Password { get; set; }
        public string NombreCompleto
        {
            get
            {
                return $"{Nombre} {Apellido}";
            }
        }
        protected Persona(int id, string nombre, string apellido, int dni, string password)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Password = password;
        }
    }
}