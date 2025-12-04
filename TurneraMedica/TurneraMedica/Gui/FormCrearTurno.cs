using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;

namespace TurneraMedica.Gui
{
    public partial class FormCrearTurno : Form
    {
        public FormCrearTurno()
        {
            InitializeComponent();
            CargarCombos();
        }

        private void CargarCombos()
        {
            // MÉDICOS
            comboMedicos.DataSource = MedicoManager.GetMedicos();
            comboMedicos.DisplayMember = "NombreCompleto"; 
            comboMedicos.ValueMember = "Id";

            // PACIENTES
            comboPacientes.DataSource = PacienteManager.GetPacientes();
            comboPacientes.DisplayMember = "NombreCompleto";
            comboPacientes.ValueMember = "Id";

            // CONSULTORIOS
            comboConsultorios.DataSource = ConsultorioManager.GetConsultorios();
            comboConsultorios.DisplayMember = "Nombre";
            comboConsultorios.ValueMember = "Id";
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            int idMedico = (int)comboMedicos.SelectedValue;
            int idPaciente = (int)comboPacientes.SelectedValue;
            int idConsultorio = (int)comboConsultorios.SelectedValue;
            DateTime fecha = dateTurno.Value;

            bool ok = TurnoManager.CrearTurno(idMedico, idPaciente, idConsultorio, fecha);

            if (ok)
            {
                MessageBox.Show("Turno creado correctamente.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error: el turno ya está tomado.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
