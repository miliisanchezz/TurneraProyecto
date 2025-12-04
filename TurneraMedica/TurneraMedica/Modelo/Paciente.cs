using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurneraMedica.Modelo
{
    public class Paciente
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Password { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Dni { get; set; }
        public int ObraSocialId { get; set; }
    }
}
