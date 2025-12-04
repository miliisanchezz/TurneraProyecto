using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TurneraMedica.Controlador;
using TurneraMedica.Gui;
using TurneraMedica.Modelo;

namespace ProyectoTurnera.Gui
{
    public partial class FormPacientes : Form
    {
        private PacienteManager pacienteManager;

        public FormPacientes()
        {
            InitializeComponent();
            pacienteManager = new PacienteManager();
        }

        private void FormPacientes_Load(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        private void CargarPacientes()
        {
            List<Paciente> lista = pacienteManager.ObtenerTodos();
            dgvPacientes.DataSource = lista;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un paciente.");
                return;
            }

            int id = Convert.ToInt32(dgvPacientes.CurrentRow.Cells["Id"].Value);

            pacienteManager.Eliminar(id);
            CargarPacientes();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            FormRegistroPaciente frm = new FormRegistroPaciente();
            frm.ShowDialog();
            CargarPacientes();
        }
    }
}
