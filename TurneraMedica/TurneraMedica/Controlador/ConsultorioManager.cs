using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TurneraMedica.Modelo;

namespace TurneraMedica.Controlador
{
    public class ConsultorioManager
    {
        public static List<Consultorio> GetConsultorios()
        {
            var lista = new List<Consultorio>();

            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM consultorio", cn);
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Consultorio
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

