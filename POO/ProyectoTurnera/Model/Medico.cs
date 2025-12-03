namespace ProyectoTurnera.Model
{
    public class Medico : Persona
    {
        public Especialidad Especialidad { get; set; }
        public int Matricula { get; set; }
        public double PrecioConsulta { get; set; }
        public Prestador Prestador { get; set; }

        public Medico() : base() { } // Necesario para binding

        public Medico(int id, string nombre, string apellido, int dni, string password,
                      Prestador prestador, Especialidad especialidad, int matricula, double precioConsulta)
            : base(id, nombre, apellido, dni, password)
        {
            Prestador = prestador;
            Especialidad = especialidad;
            Matricula = matricula;
            PrecioConsulta = precioConsulta;
        }
    }
}