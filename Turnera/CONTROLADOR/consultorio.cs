using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnera.Controlador
{
    public class Consultorio
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Direccion { get; set; }

            public Consultorio() { }

            public Consultorio(int id, string nombre, string direccion)
            {
                Id = id;
                Nombre = nombre;
                Direccion = direccion;
            }

            public override string ToString()
            {
                return $"{Nombre} - {Direccion}";
            }
        }
    }



