using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public static class PrestadorRepository
    {
        public static Prestador GetById(int id)
        {
            const string sql = "SELECT Id, Nombre FROM prestadores WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public static List<Prestador> GetAll()
        {
            const string sql = "SELECT Id, Nombre FROM prestadores";
            var dt = Database.Consultar(sql);
            var lista = new List<Prestador>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public static int Insert(Prestador prestador)
        {
            const string sql = @"
                INSERT INTO prestadores(Nombre) 
                VALUES (@Nombre)";

            var param = new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = prestador.Nombre };
            var result = Database.EjecutarEscalar(sql, param);

            return Convert.ToInt32(result);
        }

        public static  void Update(Prestador prestador)
        {
            const string sql = "UPDATE prestadores SET Nombre = @Nombre WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = Convert.ToInt32(prestador.Id) },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = prestador.Nombre.Trim() }
            };

            Database.Ejecutar(sql, parametros);
        }

        public static  void Delete(int id)
        {
            const string sql = "DELETE FROM prestadores WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        private static Prestador MapRow(DataRow row)
        {
            return new Prestador
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre = row["Nombre"] == DBNull.Value ? null : row["Nombre"].ToString().Trim()
            };
        }
    }
}