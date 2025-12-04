using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public static class AdministradorRepository
    {
        public static Administrador Login(int dni, string password)
        {

            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password
                FROM administradores
                WHERE Dni = @dni AND Password = @password";

            var parametros = new[]
            {
                new MySqlParameter("@dni", MySqlDbType.Int32) { Value = dni },
                new MySqlParameter("@password", MySqlDbType.Text) { Value = password }
            };

            DataTable dt = Database.Consultar(sql, parametros);

            if (dt.Rows.Count == 0)
                throw new UnauthorizedAccessException("DNI o contraseña de administrador incorrectos.");

            DataRow r = dt.Rows[0];

            return new Administrador
                {
                    Id = Convert.ToInt32(r["Id"]),
                    Nombre = r["Nombre"].ToString().Trim(),
                    Apellido = r["Apellido"].ToString().Trim(),
                    Dni = Convert.ToInt32(r["Dni"]),
                    Password= r["Password"].ToString().Trim()
                };

        }

        public static Administrador GetById(int id)
        {
            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password 
                FROM administradores 
                WHERE Id = @Id";

            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public static List<Administrador> GetAll()
        {
            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password 
                FROM administradores";

            var dt = Database.Consultar(sql);
            var lista = new List<Administrador>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public static int Insert(Administrador administrador)
        {

            const string sql = @"
                INSERT INTO administradores (Nombre, Apellido, DNI, Password) 
                VALUES (@Nombre, @Apellido, @DNI, @Password)";

            var parametros = new[]
            {
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = administrador.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = administrador.Apellido },
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = administrador.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = administrador.Password }
            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);

        }

        public static void Update(Administrador administrador)
        {
            const string sql = @"
                UPDATE administrado
res 
                SET Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    DNI = @DNI,
                    Password = @Password
                WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = administrador.Id },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = administrador.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = administrador.Apellido},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = administrador.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = administrador.Password }            };

            Database.Ejecutar(sql, parametros);

        }

        public static void Delete(int id)
        {

            const string sql = "DELETE FROM administradores WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        private static Administrador MapRow(DataRow row)
        {
            return new Administrador
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString().Trim(),
                Apellido = row["Apellido"].ToString().Trim(),
                Dni = Convert.ToInt32(row["Dni"]),
                Password = row["Password"].ToString().Trim()
            };

        }

    }

}
