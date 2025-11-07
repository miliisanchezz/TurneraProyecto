using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turnera.Controlador
    {
        internal class Program
        {
            static void Main(string[] args)
            {
            Turnera turnera = new Turnera();

                var m1 = new Medico(1, "Carlos", "Pérez", "30111222", "Cardiología", "MP123", 5000);
                var m2 = new Medico(2, "Ana", "Gómez", "29999111", "Dermatología", "MP456", 4000);

                var p1 = new Paciente(1, "Tomás", "López", "44111222", "Cardiología", "11223344");
                var p2 = new Paciente(2, "Lucía", "Martínez", "42123456", "OSDE", "22334455");

                var c1 = new Consultorio(1, "Consultorio A", "Av. Rivadavia 1000");

                turnera.Medicos.AddRange(new[] { m1, m2 });
                turnera.Pacientes.AddRange(new[] { p1, p2 });
                turnera.Consultorios.Add(c1);

                var turno1 = new Turno(1, m1, p1, c1, new DateTime(2025, 11, 7, 10, 0, 0));
                var turno2 = new Turno(2, m1, p2, c1, new DateTime(2025, 11, 7, 11, 0, 0));

                turnera.AgregarTurno(turno1);
                turnera.AgregarTurno(turno2);

                m1.Turnos.AddRange(new[] { turno1, turno2 });

                turnera.ReporteMedico(new DateTime(2025, 11, 1), new DateTime(2025, 11, 30));

                Console.WriteLine("\nTurnos registrados:");
                foreach (var t in turnera.Turnos)
                    Console.WriteLine(t.MostrarDatos());
            }
        }
    }

