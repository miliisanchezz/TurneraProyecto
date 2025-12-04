using System;
using MySql.Data.MySqlClient;

namespace TurneraMedica.Controlador
{
    public class TurnoManager
    {
        public static bool CrearTurno(int idMedico, int idPaciente, int idConsultorio, DateTime fecha)
        {
            using (var cn = ConexionDB.GetConexion())
            {
                cn.Open();

                // Primero verifica si ya existe turno en esa fecha/hora y consultorio
                string check = "SELECT COUNT(*) FROM turno WHERE consultorio_id = @consultorio AND fecha = @fecha";
                MySqlCommand cmdCheck = new MySqlCommand(check, cn);
                cmdCheck.Parameters.AddWithValue("@consultorio", idConsultorio);
                cmdCheck.Parameters.AddWithValue("@fecha", fecha);
                int existe = Convert.ToInt32(cmdCheck.ExecuteScalar());

                if (existe > 0)
                    return false; // ya está ocupado

                // Si no está ocupado, lo crea
                string sql = "INSERT INTO turno (medico_id, paciente_id, consultorio_id, fecha) VALUES (@medico, @paciente, @consultorio, @fecha)";
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@medico", idMedico);
                cmd.Parameters.AddWithValue("@paciente", idPaciente);
                cmd.Parameters.AddWithValue("@consultorio", idConsultorio);
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.ExecuteNonQuery();

                return true;
            }
        }
    }
}