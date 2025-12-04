using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurneraMedica.Modelo
{
    public class Medico : Usuario
    {
        
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public decimal Precio { get; set; }
    }
}

