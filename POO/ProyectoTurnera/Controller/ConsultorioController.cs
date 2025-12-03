using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;

namespace ProyectoTurnera.Controller
{
    public class ConsultorioController
    {
        private readonly ConsultorioRepository _repo = new ConsultorioRepository();

        public List<Consultorio> ObtenerTodos() => _repo.GetAll();

        public Consultorio ObtenerPorId(int id) => _repo.GetById(id);

        public int Crear(string nombre, string direccion, string numero)
        {
            return _repo.Insert(nombre, direccion, numero);
        }

        public void Actualizar(Consultorio consultorio)
        {
            _repo.Update(consultorio);
        }

        public void Eliminar(int id)
        {
            _repo.Delete(id);
        }
    }
}