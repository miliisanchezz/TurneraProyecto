using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormObrasSociales : Form
    {
        private ObraSocialManager obraManager;

        public FormObrasSociales()
        {
            InitializeComponent();
            obraManager = new ObraSocialManager();
            CargarObras();
        }

        private void CargarObras()
        {
            listaObras.DataSource = obraManager.ObtenerObras();
            listaObras.DisplayMember = "Nombre";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingresá un nombre.");
                return;
            }

            obraManager.CrearObra(nombre);
            CargarObras();
            txtNombre.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listaObras.SelectedItem == null)
            {
                MessageBox.Show("Elegí una obra social.");
                return;
            }

            dynamic o = listaObras.SelectedItem;
            obraManager.EliminarObra(o.Id);

            CargarObras();
        }
    }
}
