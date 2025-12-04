using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public static class MedicoRepository
    {

        public static Medico Login(int dni, string password)
        {

            const string sql = @"
                SELECT  Id, Nombre, Apellido, Dni, Password, Matricula, PrecioConsulta, Especialidad, Prestador
                FROM medicos
                WHERE Dni = @dni AND Password = @password";

            var parametros = new[]
            {
                new MySqlParameter("@dni", MySqlDbType.Int32) { Value = dni },
                new MySqlParameter("@password", MySqlDbType.Text) { Value = password }
            };

            DataTable dt = Database.Consultar(sql, parametros);

            if (dt.Rows.Count == 0)
                throw new UnauthorizedAccessException("DNI o contraseña incorrectos.");

            DataRow r = dt.Rows[0];

            var especialidad =  EspecialidadRepository.GetById(Convert.ToInt32(r["Especialidad"]));

            var prestador = PrestadorRepository.GetById(Convert.ToInt32(r["Prestador"]));

            return new Medico(
                id: Convert.ToInt32(r["Id"]),
                nombre: r["Nombre"].ToString(),
                apellido: r["Apellido"].ToString(),
                dni: Convert.ToInt32(r["Dni"]),
                password: r["Password"].ToString(),
                prestador: prestador,
                especialidad: especialidad,
                matricula: Convert.ToInt32(r["Matricula"]),
                precioConsulta: Convert.ToDouble(r["PrecioConsulta"])
            );
        }

        public static Medico GetById(int id)
        {
            const string sql = @"
                SELECT  Id, Nombre, Apellido, Dni, Password, Matricula, PrecioConsulta, Especialidad, Prestador
                FROM medicos
                WHERE Id = @Id";

            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public static List<Medico> GetAll()
        {
            const string sql = @"
                SELECT  Id, Nombre, Apellido, Dni, Password, Matricula, PrecioConsulta, Especialidad, Prestador 
                FROM medicos";

            var dt = Database.Consultar(sql);
            var lista = new List<Medico>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public static int Insert(Medico medico)
        {

            const string sql = @"
                INSERT INTO medicos (Nombre, Apellido, DNI, Password, Matricula, PrecioConsulta, Especialidad, Prestador) 
                VALUES (@Nombre, @Apellido, @DNI,@Password, @Matricula, @PrecioConsulta, @Especialidad, @Prestador)";

            var parametros = new[]
            {
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = medico.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = medico.Apellido},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = medico.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = medico.Password },
                new MySqlParameter("@Matricula", MySqlDbType.Int32) { Value = medico.Matricula },
                new MySqlParameter("@PrecioConsulta", MySqlDbType.Double) { Value = medico.PrecioConsulta },
                new MySqlParameter("@Prestador", MySqlDbType.Int32) { Value = medico.Prestador.Id },
                new MySqlParameter("@Especialidad", MySqlDbType.Int32) { Value = medico.Especialidad.Id }
            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);

        }

        public static void Update(Medico medico)
        {

            const string sql = @"
                UPDATE medicos 
                SET Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    DNI = @DNI,
                    Password = @Password,
                    Matricula = @Matricula,
                    PrecioConsulta = @PrecioConsulta,
                    Prestador = @Prestador,
                    Especialidad = @Especialidad
                WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = medico.Id },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = medico.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = medico.Apellido},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = medico.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = medico.Password },
                new MySqlParameter("@Matricula", MySqlDbType.Int32) { Value = medico.Matricula },
                new MySqlParameter("@PrecioConsulta", MySqlDbType.Double) { Value = medico.PrecioConsulta },
                new MySqlParameter("@Prestador", MySqlDbType.Int32) { Value = medico.Prestador.Id },
                new MySqlParameter("@Especialidad", MySqlDbType.Int32) { Value = medico.Especialidad.Id }
            };

            Database.Ejecutar(sql, parametros);

        }

        public static void Delete(int id)
        {

            const string sql = "DELETE FROM medicos WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        private static Medico MapRow(DataRow row)
        {
            return new Medico
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Dni = Convert.ToInt32(row["Dni"]),
                Password = row["Password"].ToString(),
                Matricula = Convert.ToInt32(row["Matricula"]),
                PrecioConsulta = Convert.ToDouble(row["PrecioConsulta"]),
                Especialidad = EspecialidadRepository.GetById(Convert.ToInt32(row["Especialidad"])),
                Prestador = PrestadorRepository.GetById(Convert.ToInt32(row["Prestador"]))
            };

        }



    }
}