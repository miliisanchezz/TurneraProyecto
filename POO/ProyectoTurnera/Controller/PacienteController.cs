using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller

{
    public class PacienteController : IEntityController<Paciente>
    {

        public Paciente Login(int dni, string password)
        {
            return PacienteRepository.Login(dni, password);
        }

        public List<Paciente> ObtenerTodos() => PacienteRepository.GetAll();

        public Paciente ObtenerPorId(int id) => PacienteRepository.GetById(id);

        public int Crear(Paciente paciente)
        {
            return PacienteRepository.Insert(paciente);
        }

        public void Actualizar(Paciente paciente)
        {
            PacienteRepository.Update(paciente);
        }

        public void Eliminar(int id)
        {
            PacienteRepository.Delete(id);
        }

    }
}