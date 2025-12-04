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
            List<Medico> lista = new List<Medico>();

            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id, nombre, apellido FROM medico", cn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Medico m = new Medico();
                    m.Id = Convert.ToInt32(dr["id"]);
                    m.Nombre = dr["nombre"].ToString();
                    m.Apellido = dr["apellido"].ToString();
                    lista.Add(m);
                }
            }
            return lista;
        }
    }
}