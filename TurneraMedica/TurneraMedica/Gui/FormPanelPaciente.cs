using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormPanelPaciente : Form
    {
        private int pacienteId;
        private string nombrePaciente;

        public FormPanelPaciente(int idPaciente, string nombre)
        {
            InitializeComponent();
            pacienteId = idPaciente;
            nombrePaciente = nombre;

            lblBienvenida.Text = "Hola " + nombrePaciente;
        }

        private void btnVerTurnos_Click(object sender, EventArgs e)
        {
            TurnoManager tm = new TurnoManager();
            dgvTurnos.DataSource = tm.ObtenerTurnosPaciente(pacienteId);
        }

        private void btnSacarTurno_Click(object sender, EventArgs e)
        {
            FormCrearTurno f = new FormCrearTurno(0, pacienteId);
            f.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            new FormInicio().Show();
        }
    }
}
