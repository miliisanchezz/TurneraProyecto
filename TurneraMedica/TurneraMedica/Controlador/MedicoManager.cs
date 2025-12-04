using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TurneraMedica.Modelo;

namespace TurneraMedica.Controlador
{
    public class MedicoManager
    {
        public static List<Medico> GetMedicos()
        {
            var lista = new List<Medico>();
            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();
                var cmd = new MySqlCommand("SELECT id, nombre, apellido FROM medico", cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var m = new Medico
                    {
                        Id = (int)dr["id"],
                        Nombre = dr["nombre"].ToString(),
                        Apellido = dr["apellido"].ToString(),
                        NombreCompleto = dr["nombre"] + " " + dr["apellido"]
                    };
                    lista.Add(m);
                }
            }
            return lista;
        }
    }
}