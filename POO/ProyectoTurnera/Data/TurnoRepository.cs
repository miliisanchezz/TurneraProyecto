using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

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

            const string sql = @"
             SELECT 
                 Id,
                 Fecha,
                 MedicoId,
                 ConsultorioId,
                 EspecialidadId,
                 PrestadorMedicoId,
                 PrecioConsulta AS PrecioConsulta,
                 PacienteId,
                 PrestadorEstadoPacienteId AS MrestadoPacienteoId,  -- ajusta el nombre real de la columna
                 Estado
             FROM turnos";

             var dt = Database.Consultar(sql);
             var lista = new List<Turno>();

             foreach (DataRow row in dt.Rows)
                 lista.Add(MapRow(row));

             return lista;
        }

        public static int Insert(Turno turno)
        {
            return -1;
            /*
            const string sql = @"
                INSERT INTO administradores (Nombre, Apellido, DNI, Password) 
                VALUES (@Nombre, @Apellido, @DNI, @Password)";

            var parametros = new[]
            {
                new MySqlParameter("@Nombre", MySqlDbType.Text) { Value = administrador.Nombre },
                new MySqlParameter("@Apellido", MySqlDbType.Text) { Value = administrador.Apellido },
                new MySqlParameter("@DNI", MySqlDbType.Int32) { Value = administrador.Dni },
                new MySqlParameter("@Password", MySqlDbType.Text) { Value = administrador.Password }
            };

            var result = Database.EjecutarEscalar(sql, parametros);

            return Convert.ToInt32(result);*/
            
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
            return;
            /*
            const string sql = "DELETE FROM administradores WHERE Id = @Id";
            var param = new MySqlParameter("@Id", MySqlDbType.Int32) { Value = id };
            Database.Ejecutar(sql, param);*/
        }

        private static Turno MapRow(DataRow row)
        {

            return new Turno()
            {
                Fecha = Convert.ToDateTime(row["Fecha"]),
                Medico = MedicoRepository.GetById(Convert.ToInt32(row["Medico"])),
                Consultorio = ConsultorioRepository.GetById(Convert.ToInt32(row["Consultorio"])),
                Especialidad = EspecialidadRepository.GetById(Convert.ToInt32(row["Especialidad"])),
                PrestadorMedico = PrestadorRepository.GetById(Convert.ToInt32(row["PrestadorMedico"])),
                PrecioConsulta = Convert.ToDouble(row["PrecioConsulta"]),
                Paciente = row["Paciente"] == null ? null : PacienteRepository.GetValueOrDefault((int)row["Paciente"]),
                PrestadorPaciente = row["PrestadorPaciente"] == null ? null : PrestadorRepository.GetValueOrDefault((int)row["PrestadorPaciente"]),
                Estado

            }; 

        }

    }

}
