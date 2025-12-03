using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public class AdministradorRepository
    {
        public Administrador Login(int dni, string password)
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

        public Administrador GetById(int id)
        {
            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password 
                FROM consultorios 
                WHERE Id = @Id";

            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public List<Administrador> GetAll()
        {
            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password 
                FROM consultorios 
                ORDER BY Nombre";

            var dt = Database.Consultar(sql);
            var lista = new List<Administrador>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public int Insert(string nombre, string apellido, int dni, string password)
        {

            const string sql = @"
                INSERT INTO administradores (Nombre, Apellido, DNI, Password) 
                VALUES (@Nombre, @Apellido, @DNI,@Password);
                SELECT SCOPE_IDENTITY();";

            var parametros = new[]
            {
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = (object)nombre.Trim() },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = (object)apellido?.Trim()},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = (object)dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = (object)password.Trim() }
            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);

        }

        public void Update(Administrador administrador)
        {

            const string sql = @"
                UPDATE administradpres 
                SET Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    DNI = @DNI,
                    @Password = @Password
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

        public void Delete(int id)
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
