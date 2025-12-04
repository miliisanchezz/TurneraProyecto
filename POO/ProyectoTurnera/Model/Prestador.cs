namespace ProyectoTurnera.Model
{
    public class Prestador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Prestador() { } // binding

        public Prestador(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString() => Nombre;

    }
}