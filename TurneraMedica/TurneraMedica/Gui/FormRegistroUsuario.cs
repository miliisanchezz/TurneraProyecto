using System;
using System.Windows.Forms;

namespace TurneraMedica.Gui
{
    public partial class FormRegistroUsuario : Form
    {
        public FormRegistroUsuario()
        {
            InitializeComponent();
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            new FormRegistroMedico().Show();
            this.Hide();
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            new FormRegistroPaciente().Show();
            this.Hide();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormInicio().Show();
            this.Hide();
        }

        private void FormRegistroUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}
