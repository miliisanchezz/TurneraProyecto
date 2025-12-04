using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProyectoTurnera.Data
{
    public static class Database
    {
        
        private static readonly string ConnectionString =
            "server=localhost; database=turnera; uid=usuario; pwd=clave; CharSet=utf8mb4;";

        
        public static DataTable Consultar(string sql, params MySqlParameter[] parametros)
        {
            var dt = new DataTable();

            using (var con = new MySqlConnection(ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            {
                if (parametros != null && parametros.Length > 0)
                    cmd.Parameters.AddRange(parametros);

                try
                {
                    con.Open();
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return dt;
        }

        // IUD
        public static void Ejecutar(string sql, params MySqlParameter[] parametros)
        {
            using (var con = new MySqlConnection(ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            {
                if (parametros != null && parametros.Length > 0)
                    cmd.Parameters.AddRange(parametros);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        
        public static int EjecutarEscalar(string sql, params MySqlParameter[] parametros)
        {
            using (var con = new MySqlConnection(ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            {
                if (parametros != null && parametros.Length > 0)
                    cmd.Parameters.AddRange(parametros);

                
                if (!sql.Trim().ToUpper().Contains("SELECT LAST_INSERT_ID()"))
                    sql += "; SELECT LAST_INSERT_ID();";

                cmd.CommandText = sql;

                try
                {
                    con.Open();
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        // solo para consultas simples sin riesgp
        public static DataTable Consultar(string sql) => Consultar(sql, null);
        public static void Ejecutar(string sql) => Ejecutar(sql, null);
    }
}