using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormConsultorios : Form
    {
        private ConsultorioManager consultorioManager;

        public FormConsultorios()
        {
            InitializeComponent();
            consultorioManager = new ConsultorioManager();
            CargarConsultorios();
        }

        private void CargarConsultorios()
        {
            listaConsultorios.DataSource = consultorioManager.ObtenerConsultorios();
            listaConsultorios.DisplayMember = "Nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingresá un nombre.");
                return;
            }

            consultorioManager.CrearConsultorio(nombre);
            CargarConsultorios();
            txtNombre.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listaConsultorios.SelectedItem == null)
            {
                MessageBox.Show("Elegí un consultorio.");
                return;
            }

            dynamic c = listaConsultorios.SelectedItem;
            consultorioManager.EliminarConsultorio(c.Id);

            CargarConsultorios();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
