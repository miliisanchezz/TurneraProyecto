using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ProyectoTurnera.Data
{
    public static class Database
    {
        // Ocultamos la cadena en appsettings.json o variables de entorno (recomendado)
        // Pero por ahora la dejamos aquí (mejor que hardcodeada en cada clase)
        private static readonly string ConnectionString =
            "server=localhost; database=turnera; uid=usuario; pwd=clave; CharSet=utf8mb4;";

        // Método genérico y seguro para consultas con parámetros
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

        // Para INSERT, UPDATE, DELETE
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

        // Para INSERT que devuelven el ID generado
        public static int EjecutarEscalar(string sql, params MySqlParameter[] parametros)
        {
            using (var con = new MySqlConnection(ConnectionString))
            using (var cmd = new MySqlCommand(sql, con))
            {
                if (parametros != null && parametros.Length > 0)
                    cmd.Parameters.AddRange(parametros);

                // Para MySQL: usar LAST_INSERT_ID()
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

        // Versión sobrecargada sin parámetros (para consultas simples sin riesgo)
        public static DataTable Consultar(string sql) => Consultar(sql, null);
        public static void Ejecutar(string sql) => Ejecutar(sql, null);
    }
}