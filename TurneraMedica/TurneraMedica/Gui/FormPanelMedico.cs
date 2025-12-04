using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormPanelMedico : Form
    {
        private int medicoId;
        private string nombreMedico;

        public FormPanelMedico(int idMedico, string nombre)
        {
            InitializeComponent();
            medicoId = idMedico;
            nombreMedico = nombre;

            lblBienvenida.Text = "Bienvenido Dr. " + nombreMedico;
        }

        private void btnVerTurnos_Click(object sender, EventArgs e)
        {
            TurnoManager tm = new TurnoManager();
            dgvTurnos.DataSource = tm.ObtenerTurnosPorFecha(medicoId, dtpFecha.Value.Date);
        }

        private void btnCrearTurno_Click(object sender, EventArgs e)
        {
            FormCrearTurno f = new FormCrearTurno(medicoId);
            f.ShowDialog();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FormReportesMedico f = new FormReportesMedico(medicoId);
            f.ShowDialog();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormInicio f = new FormInicio();
            f.Show();
        }
    }
}
