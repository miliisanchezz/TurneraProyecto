using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class ConsultorioController: IEntityController<Consultorio>
    {

        public List<Consultorio> ObtenerTodos() => ConsultorioRepository.GetAll();

        public Consultorio ObtenerPorId(int id) => ConsultorioRepository.GetById(id);

        public int Crear(Consultorio consultorio)
        {
            return ConsultorioRepository.Insert(consultorio);
        }

        public void Actualizar(Consultorio consultorio)
        {
            ConsultorioRepository.Update(consultorio);
        }

        public void Eliminar(int id)
        {
            ConsultorioRepository.Delete(id);
        }
    }
}