using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TurneraMedica.Modelo;

namespace TurneraMedica.Controlador
{
    public class PacienteManager
    {
        // Para los combos (FormCrearTurno)
        public static List<Paciente> GetPacientes()
        {
            return ObtenerTodos(); // reutiliza el mismo método
        }

        // Para el formulario de lista (FormPacientes)
        public List<Paciente> ObtenerTodos()
        {
            var lista = new List<Paciente>();
            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();
                var cmd = new MySqlCommand("SELECT id, nombre, apellido FROM paciente", cn);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var p = new Paciente
                    {
                        Id = (int)dr["id"],
                        Nombre = dr["nombre"].ToString(),
                        Apellido = dr["apellido"].ToString(),
                        NombreCompleto = dr["nombre"] + " " + dr["apellido"]
                    };
                    lista.Add(p);
                }
            }
            return lista;
        }

        // Para eliminar (FormPacientes)
        public void Eliminar(int id)
        {
            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();
                var cmd = new MySqlCommand("DELETE FROM paciente WHERE id = " + id, cn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}