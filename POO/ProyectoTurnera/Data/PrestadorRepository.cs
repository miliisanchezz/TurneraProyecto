using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public class PrestadorRepository
    {
        public Prestador GetById(int id)
        {
            const string sql = "SELECT Id, Nombre FROM prestadores WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public List<Prestador> GetAll()
        {
            const string sql = "SELECT Id, Nombre FROM prestadores ORDER BY Nombre";
            var dt = Database.Consultar(sql);
            var lista = new List<Prestador>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public int Insert(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del prestador es obligatorio.");

            const string sql = @"
                INSERT INTO prestadores (Nombre) 
                VALUES (@Nombre); 
                SELECT SCOPE_IDENTITY();";

            var param = new MySqlParameter("@Nombre", MySqlDbType.VarChar, 150) { Value = nombre.Trim() };
            var result = Database.EjecutarEscalar(sql, param);

            return Convert.ToInt32(result);
        }

        public void Update(Prestador prestador)
        {
            if (prestador == null) throw new ArgumentNullException(nameof(prestador));
            if (prestador.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(prestador.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            const string sql = "UPDATE prestadores SET Nombre = @Nombre WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = Convert.ToInt32(prestador.Id) },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = prestador.Nombre.Trim() }
            };

            Database.Ejecutar(sql, parametros);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");

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