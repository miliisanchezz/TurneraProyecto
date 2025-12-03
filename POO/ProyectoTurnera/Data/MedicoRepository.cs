using Org.BouncyCastle.Crypto.Generators;
using ProyectoTurnera.Controlador;
using ProyectoTurnera.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTurnera.Data
{
    public class MedicoRepository
    {
        public Medico Login(int dni, string passwordIngresado)
        {
            const string sql = @"
                SELECT  m.Id, m.Nombre, m.Apellido, m.Dni, m.PasswordHash,
                        m.Matricula, m.PrecioConsulta,
                        m.Especialidad, m.Prestador,
                        e.Id AS EspId, e.Nombre AS EspNombre,
                        p.Id AS PrestId, p.Nombre AS PrestNombre
                FROM medicos m
                INNER JOIN especialidades e ON m.Especialidad = e.Id
                INNER JOIN prestadores p ON m.Prestador = p.Id
                WHERE m.Dni = @Dni";

            var param = new SqlParameter("@Dni", SqlDbType.Int) { Value = dni };
            var dt = BD.Consultar(sql, param);

            if (dt.Rows.Count == 0)
                throw new UnauthorizedAccessException("DNI o contraseña incorrectos.");

            DataRow r = dt.Rows[0];

            string hashAlmacenado = r["PasswordHash"].ToString();

            // Verificación segura de contraseña
            if (!BCrypt.Net.BCrypt.Verify(passwordIngresado, hashAlmacenado))
                throw new UnauthorizedAccessException("DNI o contraseña incorrectos.");

            var especialidad = new Especialidad(
                Convert.ToInt32(r["EspId"]),
                r["EspNombre"].ToString().Trim()
            );

            var prestador = new Prestador(
                Convert.ToInt32(r["PrestId"]),
                r["PrestNombre"].ToString().Trim(),
                especialidadActual: null // opcional, si Prestador también tiene especialidad
            );

            return new Medico(
                id: Convert.ToInt32(r["Id"]),
                nombre: r["Nombre"].ToString().Trim(),
                apellido: r["Apellido"].ToString().Trim(),
                dni: Convert.ToInt32(r["Dni"]),
                passwordHash: hashAlmacenado,
                prestador: prestador,
                especialidad: especialidad,
                matricula: Convert.ToInt32(r["Matricula"]),
                precioConsulta: Convert.ToDouble(r["PrecioConsulta"])
            );
        }

        // Puedes agregar GetAll(), GetById(), etc. cuando los necesites
    }
}