using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class PrestadorController : IEntityController<Prestador>
    {

        public List<Prestador> ObtenerTodos() => PrestadorRepository.GetAll();

        public Prestador ObtenerPorId(int id) => PrestadorRepository.GetById(id);

        public int Crear(Prestador prestador)
        {
            return PrestadorRepository.Insert(prestador);
        }

        public void Actualizar(Prestador prestador)
        {
            PrestadorRepository.Update(prestador);
        }
        public void Eliminar(int id)
        {
            PrestadorRepository.Delete(id);
        }
    }
}