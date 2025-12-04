using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProyectoTurnera.Data
{
    public static class TurnoRepository
    {
      
        public static Turno GetById(int id)
        {
            return new Turno();
            /*const string sql = @"
                SELECT Id, Nombre, Apellido, Dni, Password 
                FROM administradores 
                WHERE Id = @Id"/

            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            var dt = Database.Consultar(sql, param);

            return dt.Rows.Count == 0 ? null : MapRow(dt.Rows[0]); */
        }

        public static List<Turno> GetAll()
        {
            return TurnoRepository.GetByFilter(null, null, null, null);

        }

        public static List<Turno> GetByFilter(DateTime? fromdate = null, DateTime? todate=null, Paciente paciente=null, Medico medico = null )
        {

             string sql = @"
             SELECT 
                 Id,
                 Fecha,
                 Medico,
                 Consultorio,
                 Especialidad,
                 PrestadorMedico,
                 PrecioConsulta,
                 Paciente,
                 PrestadorPaciente,
                 Estado
             FROM turnos
                WHERE 1=1
            ";

            if (fromdate != null)
            { 
                sql += $" AND turnos.Fecha >= '{fromdate:yyyy-MM-dd 00:00:00}' ";
            }

            if (todate != null)
            {
                sql += $" AND turnos.Fecha <= '{todate:yyyy-MM-dd HH:mm:ss}' ";
            }

            if (paciente != null)
            {
                sql += $" AND turnos.Paciente = {paciente.Id} ";
            }

            if (medico != null)
            {
                sql += $" AND turnos.Medico = {medico.Id} ";
            }

            var dt = Database.Consultar(sql);
            var lista = new List<Turno>();

            foreach (DataRow row in dt.Rows)
                lista.Add(MapRow(row));

            return lista;
        }

        public static int Insert(Turno turno)
        {
            const string sql = @"
                INSERT INTO turnos (Fecha, Medico, Especialidad, Consultorio, PrestadorMedico, PrecioConsulta, Estado) 
                VALUES (@Fecha, @Medico, @Especialidad, @Consultorio, @PrestadorMedico, @PrecioCOnsulta, @Estado)";

            var parametros = new[]
            {
                new MySqlParameter("@Fecha", MySqlDbType.DateTime) { Value = turno.Fecha },
                new MySqlParameter("@Medico", MySqlDbType.Int32) { Value = turno.Medico.Id },
                new MySqlParameter("@Especialidad", MySqlDbType.Int32) { Value = turno.Especialidad.Id },   
                new MySqlParameter("@Consultorio", MySqlDbType.Int32) { Value = turno.Consultorio.Id }, 
                new MySqlParameter("@PrestadorMedico", MySqlDbType.Int32) { Value = turno.PrestadorMedico.Id },
                new MySqlParameter("@PrecioCOnsulta", MySqlDbType.Double) { Value = turno.PrecioConsulta },
                new MySqlParameter("@Estado", MySqlDbType.Int32) { Value = turno.Estado }   

            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);
            
        }

        public static void Update(Turno turno)
        {
            return;
            /*const string sql = @"
                UPDATE administradores 
                SET Nombre = @Nombre, 
                    Apellido = @Apellido, 
                    DNI = @DNI,
                    Password = @Password
                WHERE Id = @Id";

            var parametros = new[]
            {
                new MySqlParameter("@Id", MySqlDbType.Int32) { Value = administrador.Id },
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = administrador.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = administrador.Apellido},
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = administrador.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = administrador.Password }            };

            Database.Ejecutar(sql, parametros);*/

        }

        public static void Delete(int id)
        {
            const string sql = "DELETE FROM turnos WHERE Id = @Id AND Paciente IS  NULL";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }

        public static void Unassign(int id)
        {
            const string sql = "UPDATE turnos set Paciente = null, PrestadorPaciente = null WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);
        }


        private static Turno MapRow(DataRow row)
        {

            return new Turno()
            {
                Id = Convert.ToInt32(row["Id"]),
                Fecha = Convert.ToDateTime(row["Fecha"]), 
                Medico = MedicoRepository.GetById(Convert.ToInt32(row["Medico"])),
                Consultorio = ConsultorioRepository.GetById(Convert.ToInt32(row["Consultorio"])),
                Especialidad = EspecialidadRepository.GetById(Convert.ToInt32(row["Especialidad"])),
                PrestadorMedico = PrestadorRepository.GetById(Convert.ToInt32(row["PrestadorMedico"])),
                PrecioConsulta = Convert.ToDouble(row["PrecioConsulta"]),
                Paciente = row["Paciente"] == DBNull.Value ? null : PacienteRepository.GetById((int)row["Paciente"]),
                PrestadorPaciente = row["PrestadorPaciente"] == DBNull.Value ? null : PrestadorRepository.GetById(Convert.ToInt32(row["PrestadorPaciente"])) /*,
                Estado = row["Estado"] != DBNull.Value ? (int?)row["Estado"] : null */

            }; 

        }

    }

}
