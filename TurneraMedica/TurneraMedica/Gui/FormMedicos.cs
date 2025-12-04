using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TurneraMedica.Controlador;
using TurneraMedica.Gui;
using TurneraMedica.Modelo;

namespace ProyectoTurnera.Gui
{
    public partial class FormMedicos : Form
    {
        private MedicoManager medicoManager;

        public FormMedicos()
        {
            InitializeComponent();
            medicoManager = new MedicoManager();
        }

        private void FormMedicos_Load(object sender, EventArgs e)
        {
            CargarMedicos();
        }

        private void CargarMedicos()
        {
            List<Medico> lista = medicoManager.ObtenerTodos();
            dgvMedicos.DataSource = lista;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMedicos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMedicos.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un médico.");
                return;
            }

            int id = Convert.ToInt32(dgvMedicos.CurrentRow.Cells["Id"].Value);

            medicoManager.Eliminar(id);
            CargarMedicos();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormRegistroMedico frm = new FormRegistroMedico();
            frm.ShowDialog();
            CargarMedicos();
        }
    }
}
