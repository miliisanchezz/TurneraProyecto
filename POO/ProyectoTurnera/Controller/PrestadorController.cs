using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class PrestadorController
    {
        private readonly PrestadorRepository _repo = new PrestadorRepository();

        public List<Prestador> ObtenerTodos() => _repo.GetAll();

        public Prestador ObtenerPorId(int id) => _repo.GetById(id);

        public int Crear(string nombre)
        {
            return _repo.Insert(nombre);
        }

        public void Actualizar(Prestador prestador)
        {
            _repo.Update(prestador);
        }
        public void Eliminar(int id)
        {
            _repo.Delete(id);
        }
    }
}