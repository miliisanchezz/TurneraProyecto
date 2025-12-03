// ProyectoTurnera.Controller/PacienteController.cs
using ProyectoTurnera.Data;
using ProyectoTurnera.Model;
using System;

namespace ProyectoTurnera.Controller
{
    public class PacienteController
    {
        private readonly PacienteRepository _repo = new PacienteRepository();

        public Paciente Login(int dni, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Contraseña requerida.");

            return _repo.Login(dni, password);
        }
    }
}