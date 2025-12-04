using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormTurnos : Form
    {
        private TurnoManager turnoManager;

        public FormTurnos()
        {
            InitializeComponent();
            turnoManager = new TurnoManager();
            CargarTurnos();
        }

        private void CargarTurnos()
        {
            listaTurnos.DataSource = turnoManager.ObtenerTurnos();
            listaTurnos.DisplayMember = "DescripcionCompleta";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormCrearTurno f = new FormCrearTurno();
            f.ShowDialog();
            CargarTurnos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listaTurnos.SelectedItem == null)
            {
                MessageBox.Show("Elegí un turno.");
                return;
            }

            dynamic t = listaTurnos.SelectedItem;
            turnoManager.EliminarTurno(t.Id);

            CargarTurnos();
        }
    }
}
