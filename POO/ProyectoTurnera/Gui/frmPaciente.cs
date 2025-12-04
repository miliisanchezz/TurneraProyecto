using Org.BouncyCastle.Crypto;
using ProyectoTurnera.Controller;
using ProyectoTurnera.Gui.Helpers;
using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public class frmPaciente : Form
    {
        private readonly Paciente paciente;

        private readonly AdministradorController _administradorController = new AdministradorController();
        private readonly PacienteController _pacienteController = new PacienteController();
        private readonly MedicoController _medicoController = new MedicoController();
        private readonly PrestadorController _prestadorController = new PrestadorController();
        private readonly EspecialidadController _especialidadController = new EspecialidadController();
        private readonly ConsultorioController _consultorioController = new ConsultorioController();
        private readonly TurnoController _turnoController = new TurnoController();

        // UI
        private TabControl tabControl;
        private TabPage tabPendientes;
        private TabPage tabGestionar;

        // Pendientes
        private DataGridView dgvPendientes;

        // Gestionar (buscar y reservar)
        private Panel pnlGestionTop;
        private ComboBox cbEspecialidad;
        private DateTimePicker dtpGestionMonth;
        private Button btnBuscarDisponibles;
        private DataGridView dgvDisponibles;
        private Button btnReservarTurno;

        public frmPaciente(Paciente pacientelogueado)
        {
            this.paciente = pacientelogueado;
            InitializeComponents();
            LoadPendientes();
            LoadEspecialidades();
        }

        private void InitializeComponents()
        {

            this.Text = "Paciente :: " + this.paciente.ToString();

            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(900, 600);

            tabControl = new TabControl { Dock = DockStyle.Fill };

            tabPendientes = new TabPage("Pendientes");
            tabGestionar = new TabPage("Gestionar");

            // Pendientes grid (only control in this tab)
            dgvPendientes = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,                      // no editing
                AllowUserToAddRows = false,           // no inserts
                AllowUserToDeleteRows = true,        // deletion handled via Delete key
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            tabPendientes.Controls.Add(dgvPendientes);

            // Gestionar top panel
            pnlGestionTop = new Panel { Dock = DockStyle.Top, Height = 72, Padding = new Padding(6) };

            var lblEsp = new Label { Text = "Especialidad:", Left = 8, Top = 12, AutoSize = true };
            cbEspecialidad = new ComboBox { Left = lblEsp.Right + 8, Top = lblEsp.Top - 3, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            
            btnBuscarDisponibles = new Button { Text = "Buscar disponibles", Left = cbEspecialidad.Right + 12, Top = lblEsp.Top - 3, Width = 140 };
            btnBuscarDisponibles.Click += (s, e) => LoadDisponibles();

            pnlGestionTop.Controls.Add(lblEsp);
            pnlGestionTop.Controls.Add(cbEspecialidad);

            pnlGestionTop.Controls.Add(btnBuscarDisponibles);

            // Disponibles grid + reservar button
            dgvDisponibles = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            btnReservarTurno = new Button { Text = "Reservar turno seleccionado", Dock = DockStyle.Bottom, Height = 34 };
            btnReservarTurno.Click += (s, e) => ReservarTurnoSeleccionado();

            tabGestionar.Controls.Add(dgvDisponibles);
            tabGestionar.Controls.Add(btnReservarTurno);
            tabGestionar.Controls.Add(pnlGestionTop);

            tabControl.TabPages.Add(tabPendientes);
            tabControl.TabPages.Add(tabGestionar);

            Controls.Add(tabControl);

            this.FormClosed += FrmPacientes_FormClosed;
        }

        private void FrmPacientes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoadPendientes()
        {

            var first = DateTime.Today.AddDays(1);

            BindingList<Turno> pendientes = new BindingList<Turno>(_turnoController.ObtenerFiltrando(first, null, this.paciente)); 

            dgvPendientes.DataSource = pendientes;

            GridHelper.AttachCrudHandlers(dgvPendientes, "pendientes", LoadPendientes);

            GridHelper.ConfigurarColumnasTurnos(dgvPendientes);
            GridHelper.ConfigureIdColumn(dgvPendientes, visible: true, allowEdit: false);
            GridHelper.HabilitarEliminacionConConfirmacion(dgvPendientes,
                pendientes,
                id => _turnoController.Cancelar(id));

        }
        private void LoadEspecialidades()
        {
            var especialidades = new BindingList<Especialidad>(_especialidadController.ObtenerTodos());
            cbEspecialidad.DataSource = especialidades;
            cbEspecialidad.ValueMember = "Id";
            cbEspecialidad.DisplayMember = "";
        }

        private void LoadDisponibles()
        {

            var first = DateTime.Today.AddDays(1);

            int especialidadId = Convert.ToInt32(cbEspecialidad.SelectedValue);

            Especialidad especialidad = _especialidadController.ObtenerPorId(especialidadId);

            BindingList<Turno> disponibles = new BindingList<Turno>(_turnoController.ObtenerFiltrando(first, null, null, null, especialidad, true));

            dgvDisponibles.DataSource = disponibles;

            GridHelper.AttachCrudHandlers(dgvDisponibles, "disponibles", LoadDisponibles);

            GridHelper.ConfigurarColumnasTurnos(dgvDisponibles);
            GridHelper.ConfigureIdColumn(dgvDisponibles, visible: true, allowEdit: false);

        }

        private void ReservarTurnoSeleccionado()
        { 
            try
            {
                if (dgvDisponibles.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un turno para reservar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var row = dgvDisponibles.CurrentRow;
                if (row.Cells["Id"] == null || row.Cells["Id"].Value == null)
                {
                    MessageBox.Show("Fila seleccionada inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int turnoId = Convert.ToInt32(row.Cells["Id"].Value);

                _turnoController.Reservar(turnoId, this.paciente);

                MessageBox.Show("Turno reservado correctamente.", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh both lists
                LoadDisponibles();
                LoadPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reservar turno: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

    }
}