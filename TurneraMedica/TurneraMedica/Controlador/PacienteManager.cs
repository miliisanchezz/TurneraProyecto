using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TurneraMedica.Modelo;

namespace TurneraMedica.Controlador
{
    public class PacienteManager
    {
        public static List<Paciente> GetPacientes()
        {
            List<Paciente> lista = new List<Paciente>();

            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT id, nombre, apellido FROM paciente", cn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Paciente p = new Paciente();
                    p.Id = Convert.ToInt32(dr["id"]);
                    p.Nombre = dr["nombre"].ToString();
                    p.Apellido = dr["apellido"].ToString();
                    lista.Add(p);
                }
            }
            return lista;
        }
    }
}