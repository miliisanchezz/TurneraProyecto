using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public static class EspecialidadRepository
    {

        public static Especialidad GetById(int id)
        {
            const string sql = "SELECT Id, Nombre FROM especialidades WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public static List<Especialidad> GetAll()
        {
            const string sql = "SELECT Id, Nombre FROM especialidades";
            var dt = Database.Consultar(sql);
            var lista = new List<Especialidad>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public static int Insert(Especialidad especialidad)
        {
            const string sql = "INSERT INTO especialidades (Nombre) VALUES (@Nombre)";

            var param = new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = especialidad.Nombre };
            var result = Database.EjecutarEscalar(sql, param);

            return Convert.ToInt32(result);
        }

        public static void Update(Especialidad especialidad)
        {
            const string sql = "UPDATE especialidades SET Nombre = @Nombre WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = especialidad.Id },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = especialidad.Nombre.Trim() }
            };

            Database.Ejecutar(sql, parametros);
        }

        public static void Delete(int id)
        {
            const string sql = "DELETE FROM especialidades WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        private static Especialidad MapRow(DataRow row)
        {
            return new Especialidad
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"] == DBNull.Value ? null : row["Nombre"].ToString().Trim()
            };
        }
    }
}