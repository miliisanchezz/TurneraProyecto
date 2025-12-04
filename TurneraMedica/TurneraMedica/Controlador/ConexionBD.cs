using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TurneraMedica.Controlador
{
    public class ConexionDB
    {
        private static string cadena =
            "server=localhost;database=turnera;user=root;password='';";

        public static MySqlConnection GetConexion()
        {
            return new MySqlConnection(cadena);
        }
    }
}

