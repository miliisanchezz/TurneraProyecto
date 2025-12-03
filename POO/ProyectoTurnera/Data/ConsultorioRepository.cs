using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProyectoTurnera.Data
{
    public class ConsultorioRepository
    {
        public Consultorio GetById(int id)
        {
            const string sql = @"
                SELECT Id, Nombre, Direccion, NumeroConsultorio 
                FROM consultorios 
                WHERE Id = @Id";

            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]);
        }

        public List<Consultorio> GetAll()
        {
            const string sql = @"
                SELECT Id, Nombre, Direccion, NumeroConsultorio 
                FROM consultorios 
                ORDER BY Nombre";

            var dt = Database.Consultar(sql);
            var lista = new List<Consultorio>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public int Insert(string nombre, string direccion, string numeroConsultorio)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del consultorio es obligatorio.");

            const string sql = @"
                INSERT INTO consultorios (Nombre, Direccion, NumeroConsultorio) 
                VALUES (@Nombre, @Direccion, @NumeroConsultorio);
                SELECT SCOPE_IDENTITY();";

            var parametros = new[]
            {
                new MySqlParameter("@Nombre", MySqlDbType.VarChar, 100) { Value = (object)nombre.Trim() ?? DBNull.Value },
                new MySqlParameter("@Direccion", MySqlDbType.VarChar, 200) { Value = (object)direccion?.Trim() ?? DBNull.Value },
                new MySqlParameter("@NumeroConsultorio", MySqlDbType.Int32) { Value = (object)numeroConsultorio ?? DBNull.Value }
            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);

        }

        public void Update(Consultorio consultorio)
        {
         
            const string sql = @"
                UPDATE consultorios 
                SET Nombre = @Nombre, 
                    Direccion = @Direccion, 
                    NumeroConsultorio = @NumeroConsultorio 
                WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = consultorio.Id },
                new MySqlParameter("@Nombre", MySqlDbType.VarChar, 100) { Value =  (object)consultorio.Nombre.Trim() },
                new MySqlParameter("@Direccion", MySqlDbType.VarChar, 200) { Value = (object)consultorio.Direccion?.Trim() },
                new MySqlParameter("@NumeroConsultorio", MySqlDbType.Int32) { Value = (object)consultorio.NumeroConsultorio }
            };

            Database.Ejecutar(sql, parametros);

        }

        public void Delete(int id)
        {

            const string sql = "DELETE FROM consultorios WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        private static Consultorio MapRow(DataRow row)
        {
            return new Consultorio
            {
                Id = Convert.ToInt32(row["Id"]),
                Nombre =  row["Nombre"].ToString().Trim(),
                Direccion =  row["Direccion"].ToString().Trim(),
                NumeroConsultorio =  Convert.ToInt32(row["NumeroConsultorio"].ToString())
            };
        }
    }
}