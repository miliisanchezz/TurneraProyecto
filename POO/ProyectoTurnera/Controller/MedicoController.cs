using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class MedicoController : IEntityController<Medico>
    {

        public Medico Login(int dni, string password)
        {
            return MedicoRepository.Login(dni, password);
        }

        public List<Medico> ObtenerTodos() => MedicoRepository.GetAll();

        public Medico ObtenerPorId(int id) => MedicoRepository.GetById(id);

        public int Crear(Medico medico)
        {
            return MedicoRepository.Insert(medico);
        }

        public void Actualizar(Medico medico)
        {
            MedicoRepository.Update(medico);
        }

        public void Eliminar(int id)
        {
            MedicoRepository.Delete(id);
        }

    }
}