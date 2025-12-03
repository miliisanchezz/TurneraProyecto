using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public class EspecialidadRepository
    {
        private const string TableName = "especialidades";

        public Especialidad GetById(int id)
        {
            const string sql = "SELECT Id, Nombre FROM especialidades WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public List<Especialidad> GetAll()
        {
            const string sql = "SELECT Id, Nombre FROM especialidades ORDER BY Nombre";
            var dt = Database.Consultar(sql);
            var lista = new List<Especialidad>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public int Insert(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la especialidad es obligatorio.");

            const string sql = "INSERT INTO especialidades (Nombre) VALUES (@Nombre); SELECT SCOPE_IDENTITY();";

            var param = new MySqlParameter("@Nombre", SqlDbType.Text) { Value = nombre.Trim() };
            var result = Database.EjecutarEscalar(sql, param);

            return Convert.ToInt32(result);
        }

        public void Update(Especialidad especialidad)
        {
            if (especialidad == null) throw new ArgumentNullException(nameof(especialidad));
            if (especialidad.Id <= 0) throw new ArgumentException("ID inválido.");
            if (string.IsNullOrWhiteSpace(especialidad.Nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            const string sql = "UPDATE especialidades SET Nombre = @Nombre WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = especialidad.Id },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = especialidad.Nombre.Trim() }
            };

            Database.Ejecutar(sql, parametros);
        }

        public void Delete(int id)
        {
            if (id <= 0) throw new ArgumentException("ID inválido.");

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