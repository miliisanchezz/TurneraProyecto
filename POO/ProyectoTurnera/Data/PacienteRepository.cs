using ProyectoTurnera.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTurnera.Data
{
    public class PacienteRepository
    {
        public Paciente Login(int dni, string password)
        {
            const string sql = @"
                SELECT p.Id, p.Nombre, p.Apellido, p.Dni, p.Password, p.Telefono, p.Prestador
                FROM pacientes
                WHERE p.Dni = @Dni AND Password = @pass";

            var parametros = new[]
            {
                new SqlParameter("@Dni", SqlDbType.Int) { Value = dni },
                new SqlParameter("@Password", SqlDbType.Int) { Value = password }
            };

            DataTable dt = BD.Consultar(sql, parametros);

            if (dt.Rows.Count == 0)
                throw new UnauthorizedAccessException("DNI o contraseña incorrectos.");

            return new Paciente(
                Id: Convert.ToInt32(r["Id"]),
                Nombre: r["Nombre"].ToString(),
                Apellido: r["Apellido"].ToString(),
                Dni: Convert.ToInt32(r["Dni"]),
                Prestador: prestador,
                Telefono: r["Telefono"]?.ToString()
            );
        }
    }
}