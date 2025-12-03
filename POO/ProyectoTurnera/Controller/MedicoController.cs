using ProyectoTurnera.Model;
using ProyectoTurnera.Data;
using System;

namespace ProyectoTurnera.Controller
{
    public class MedicoController
    {
        private readonly MedicoRepository _repo = new MedicoRepository();

        public Medico Login(int dni, string password)
        {
            if (dni <= 0)
                throw new ArgumentException("El DNI debe ser mayor a 0.", nameof(dni));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña es obligatoria.", nameof(password));

            return _repo.Login(dni, password);
        }
    }
}