using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{ 

    public class AdministradorController : IEntityController<Administrador>
    {

        public Administrador Login(int dni, string password)
        {
            return AdministradorRepository.Login(dni, password);
        }

        public List<Administrador> ObtenerTodos() => AdministradorRepository.GetAll();

        public Administrador ObtenerPorId(int id) => AdministradorRepository.GetById(id);

        public int Crear(Administrador administrador)
        {
            return AdministradorRepository.Insert(administrador);
        }

        public void Actualizar(Administrador administrador)
        {
            AdministradorRepository.Update(administrador);
        }

        public void Eliminar(int id)
        {
            AdministradorRepository.Delete(id);
        }

    }

}