using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnera.Controlador
{
    public class Paciente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string ObraSocial { get; set; }
        public string Telefono { get; set; }

        public Paciente() { }

        public Paciente(int id, string nombre, string apellido, string dni, string obraSocial, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            ObraSocial = obraSocial;
            Telefono = telefono;
        }

        public string MostrarDatos()
        {
            return $"{Nombre} {Apellido} - DNI: {Dni} - Obra Social: {ObraSocial}";
        }
    }
}