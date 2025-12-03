using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{ 

    public class TurnoController : IEntityController<Turno>
    {

        public List<Turno> ObtenerTodos() => TurnoRepository.GetAll();

        public Turno ObtenerPorId(int id) => TurnoRepository.GetById(id);

        public int Crear(Turno turno)
        {
            return TurnoRepository.Insert(turno);
        }

        public void Actualizar(Turno turno)
        {
            TurnoRepository.Update(turno);
        }

        public void Eliminar(int id)
        {
            TurnoRepository.Delete(id);
        }

    }

}