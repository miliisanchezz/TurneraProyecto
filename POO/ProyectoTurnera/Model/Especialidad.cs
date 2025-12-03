namespace ProyectoTurnera.Model
{
    public class Especialidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public Especialidad() { } // Para binding

        public Especialidad(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }

        public override string ToString() => Nombre;

    }
}