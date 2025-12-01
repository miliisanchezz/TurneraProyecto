using System;
using System.Windows.Forms;
using ProyectoTurnera.Gui;

namespace ProyectoTurnera
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Iniciar con la pantalla de login
            Application.Run(new LoginForm());
        }
    }
}
