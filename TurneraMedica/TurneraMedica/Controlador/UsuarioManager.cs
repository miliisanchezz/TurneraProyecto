using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TurneraMedica.Controlador
{
    public class UsuarioManager
    {
        public static bool Login(string usuario, string password, string tipo)
        {
            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();

                string tabla = tipo == "medico" ? "medico" : "paciente";

                MySqlCommand cmd = new MySqlCommand(
                    $"SELECT id FROM {tabla} WHERE usuario=@u AND password=@p",
                    cn
                );

                cmd.Parameters.AddWithValue("@u", usuario);
                cmd.Parameters.AddWithValue("@p", password);

                var dr = cmd.ExecuteReader();
                return dr.Read();
            }
        }
    }
}

