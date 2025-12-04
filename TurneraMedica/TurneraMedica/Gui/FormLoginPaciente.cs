using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormLoginPaciente : Form
    {
        public FormLoginPaciente()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            bool ok = UsuarioManager.Login(txtUsuario.Text, txtPassword.Text, "paciente");

            if (ok)
            {
                new FormPanelPaciente().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormInicio().Show();
            this.Hide();
        }
    }
}
