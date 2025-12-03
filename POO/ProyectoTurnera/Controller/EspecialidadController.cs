using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class EspecialidadController
    {
        private readonly EspecialidadRepository _repo = new EspecialidadRepository();

        public List<Especialidad> ObtenerTodos() => _repo.GetAll();

        public Especialidad ObtenerPorId(int id) => _repo.GetById(id);

        public int Crear(string nombre)
        {
            return _repo.Insert(nombre);
        }

        public void Actualizar(Especialidad especialidad)
        {
            _repo.Update(especialidad);
        }

        public void Eliminar(int id)
        {
            _repo.Delete(id);
        }
    }
}