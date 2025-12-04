using MySql.Data.MySqlClient;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ProyectoTurnera.Data
{
    public static class PacienteRepository
    {

        public static Paciente Login(int dni, string password)
        {

            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password, Telefono, Prestador
                FROM pacientes
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

            var prestador = PrestadorRepository.GetById(Convert.ToInt32(r["Prestador"]));

            return new Paciente(
                id: Convert.ToInt32(r["Id"]),
                nombre: r["Nombre"].ToString(),
                apellido: r["Apellido"].ToString(),
                dni: Convert.ToInt32(r["Dni"]),
                password: r["Password"].ToString(),
                prestador: prestador,
                telefono: r["Telefono"].ToString()
            );
        }

        public static Paciente GetById(int id)
        {
            const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password, Telefono, Prestador FROM pacientes WHERE Id = @Id";

            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public static List<Paciente> GetAll()
        {
            const string sql = @"
                SELECT 
                    Id, Nombre, Apellido, Dni, Password, Telefono, Prestador
                FROM pacientes";

            var dt = Database.Consultar(sql);
            var lista = new List<Paciente>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public static int Insert(Paciente paciente)
        {

            const string sql = @"
                INSERT INTO pacientes (Nombre, Apellido, DNI, Password, Telefono, Prestador) 
                VALUES (@Nombre, @Apellido, @DNI,@Password, @Telefono, @Prestador);";

            var parametros = new[]
            {
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = paciente.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = paciente.Apellido},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = paciente.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = paciente.Password },
                new MySqlParameter("@Telefono", MySqlDbType.Text) { Value = paciente.Telefono },
                new MySqlParameter("@Prestador", MySqlDbType.Int32) { Value = paciente.Prestador.Id }
            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);

        }
        public static void Update(Paciente paciente)
        {

            const string sql = @"
                UPDATE pacientes 
                SET Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    DNI = @DNI,
                    Password = @Password,
                    Telefono = @Telefono,
                    Prestador = @Prestador
                WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = paciente.Id },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = paciente.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = paciente.Apellido},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = paciente.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = paciente.Password },
                new MySqlParameter("@Telefono", MySqlDbType.Text) { Value = paciente.Telefono },
                new MySqlParameter("@Prestador", MySqlDbType.Int32) { Value = paciente.Prestador.Id }
            };

            Database.Ejecutar(sql, parametros);

        }

        public static void Delete(int id)
        {

            const string sql = "DELETE FROM pacientes WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        //cf
        private static Paciente MapRow(DataRow row)
        {
            return new Paciente
            {
                //agd
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                Dni = Convert.ToInt32(row["Dni"]),
                Password = row["Password"].ToString(),
                Telefono = row["Telefono"].ToString(),
                Prestador = PrestadorRepository.GetById(Convert.ToInt32(row["Prestador"]))
            };

        }



    }
}