using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TurneraMedica.Modelo;

namespace TurneraMedica.Controlador
{
    public class ObraSocialManager
    {
        public static List<ObraSocial> GetObras()
        {
            var lista = new List<ObraSocial>();

            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM obra_social", cn);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new ObraSocial
                    {
                        Id = dr.GetInt32("id"),
                        Nombre = dr.GetString("nombre")
                    });
                }
            }

            return lista;
        }
    }
}

