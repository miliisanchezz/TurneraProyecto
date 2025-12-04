namespace ProyectoTurnera.Model
{

    public class Administrador : Persona
    {
        public Administrador() : base() { } // binding

        public Administrador(int id, string nombre, string apellido, int dni, string password)
            : base(id, nombre, apellido, dni, password)
        {
        }

    }

}