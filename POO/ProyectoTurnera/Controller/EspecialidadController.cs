using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class EspecialidadController : IEntityController<Especialidad>
    {

        public List<Especialidad> ObtenerTodos() => EspecialidadRepository.GetAll();

        public Especialidad ObtenerPorId(int id) => EspecialidadRepository.GetById(id);

        public int Crear(Especialidad especialidad)
        {
            return EspecialidadRepository.Insert(especialidad);
        }

        public void Actualizar(Especialidad especialidad)
        {
            EspecialidadRepository.Update(especialidad);
        }

        public void Eliminar(int id)
        {
            EspecialidadRepository.Delete(id);
        }
    }
}