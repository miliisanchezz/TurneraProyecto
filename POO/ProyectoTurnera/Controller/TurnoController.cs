using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System;
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

        public List<Turno> ObtenerFiltrando(DateTime? fromdate = null, DateTime? todate = null, Paciente paciente = null, Medico medico = null, Especialidad especialidad = null, bool? solodisponibles = null)
        {
            return TurnoRepository.GetByFilter(fromdate, todate, paciente, medico, especialidad, solodisponibles);
        }

        public void Cancelar(int id)
        {
            TurnoRepository.Unassign(id);
        }

        public void Reservar(int id, Paciente paciente)
        {
            TurnoRepository.Assign(id, paciente);
        }


    }

}