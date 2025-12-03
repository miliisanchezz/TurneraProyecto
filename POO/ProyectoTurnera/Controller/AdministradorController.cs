using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{ 

    public class AdministradorController
    {
        private readonly AdministradorRepository _repo = new AdministradorRepository();

        public Administrador Login(int dni, string password)
        {
            return _repo.Login(dni, password);
        }

        public List<Administrador> ObtenerTodos() => _repo.GetAll();

        public Administrador ObtenerPorId(int id) => _repo.GetById(id);

        public int Crear(string nombre, string apellido, int dni, string pass)
        {
            return _repo.Insert(nombre, apellido, dni, pass);
        }

        public void Actualizar(Administrador administrador)
        {
            _repo.Update(administrador);
        }

        public void Eliminar(int id)
        {
            _repo.Delete(id);
        }

    }

}