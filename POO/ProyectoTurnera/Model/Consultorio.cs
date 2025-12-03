using System.Linq;

namespace ProyectoTurnera.Model
{
    public class Consultorio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public int NumeroConsultorio { get; set; }

        public Consultorio() { }

        public Consultorio(int id, string nombre, string direccion, int numeroConsultorio)
        {
            Id = id;
            Nombre = nombre?.Trim();
            Direccion = direccion?.Trim();
            NumeroConsultorio = numeroConsultorio;
        }
        public override string ToString() => $"{Nombre} / {Direccion} / {NumeroConsultorio}";

    }
}