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
    public class frmAdministrador : Form
    {

        private readonly Administrador administrador;

        private readonly AdministradorController _administradorController = new AdministradorController();
        private readonly PacienteController _pacienteController = new PacienteController();
        private readonly MedicoController _medicoController = new MedicoController();
        private readonly PrestadorController _prestadorController = new PrestadorController();
        private readonly EspecialidadController _especialidadController = new EspecialidadController();
        private readonly ConsultorioController _consultorioController = new ConsultorioController();

        private TabControl tabControl1;
        private TabPage tabPrestadores;
        private TabPage tabPacientes;
        private TabPage tabMedicos;
        private TabPage tabEspecialidades;
        private TabPage tabConsultorios;
        private TabPage tabAdministradores;
        private TabPage tabTurnos;
        private TabPage tabReportes;

        private DataGridView dgvPrestadores;
        private DataGridView dgvPacientes;
        private DataGridView dgvMedicos;
        private DataGridView dgvEspecialidades;
        private DataGridView dgvConsultorios;
        private DataGridView dgvAdministradores;
        // private DataGridView dgvTurno;
        private DataGridView dgvReportes;

        private DataGridView dgvTurnos;
        private Panel pnlTurnosTop;
        private Panel pnlTurnosBottom;
        private DateTimePicker dtpMonthYear;
        private Button btnLoadTurnos;
        private ComboBox cbMedicoTurnos;
        private ComboBox cbConsultorioTurnos;
        private DateTimePicker dtpHoraInicio;
        private DateTimePicker dtpHoraFin;
        private CheckBox chkLunes;
        private CheckBox chkMartes;
        private CheckBox chkMiercoles;
        private CheckBox chkJueves;
        private CheckBox chkViernes;
        private Button btnGenerateTurnos;

        private Panel pnlReportParams;
        private ComboBox cbReportMedico;
        private DateTimePicker dtpReportFrom;
        private DateTimePicker dtpReportTo;
        private CheckBox chkReportShowDetail;
        private Button btnRunReport;

        private Panel pnlReportResults;
        private DataGridView dgvReportResults;
        private Label lblReportTotal;


        // Columns that must be forced to uppercase
        private static readonly HashSet<string> UppercaseColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Nombre",
            "Apellido",
            "Direccion"
        };

        public frmAdministrador(Administrador administradorlogueado)
        {

            this.administrador = administradorlogueado;

            InitializeComponents();

            LoadPrestadores();
            LoadEspecialidades();
            LoadConsultorios();
            LoadAdministradores();
            LoadPacientes();
            LoadMedicos();
            
        }

        private void InitializeComponents()
        {

            this.Text  = "Administrador :: " + this.administrador.ToString();

            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;

            TabControl tabControl1 = new TabControl
            {
                Dock = DockStyle.Fill
            };

            tabPrestadores = new TabPage("Prestadores");
            tabPacientes = new TabPage("Pacientes");
            tabMedicos = new TabPage("Médicos");
            tabEspecialidades = new TabPage("Especialidades");
            tabConsultorios = new TabPage("Consultorios");
            tabTurnos = new TabPage("Turnos");
            tabAdministradores = new TabPage("Administradores");
            tabReportes = new TabPage("Reportes");

            dgvPrestadores = CreateDefaultGrid("dgvPrestadores");
            dgvPacientes = CreateDefaultGrid("dgvPacientes");
            dgvMedicos = CreateDefaultGrid("dgvMedicos");
            dgvEspecialidades = CreateDefaultGrid("dgvEspecialidades");
            dgvAdministradores = CreateDefaultGrid("dgvAdministradores");
            dgvConsultorios = CreateDefaultGrid("dgvConsultorios");
            dgvReportes = CreateDefaultGrid("dgvReportes");

            tabPrestadores.Controls.Add(dgvPrestadores);
            tabPacientes.Controls.Add(dgvPacientes);
            tabMedicos.Controls.Add(dgvMedicos);
            tabEspecialidades.Controls.Add(dgvEspecialidades);
            tabAdministradores.Controls.Add(dgvAdministradores);
            tabConsultorios.Controls.Add(dgvConsultorios);
            tabReportes.Controls.Add(dgvReportes);

            tabControl1.TabPages.Add(tabPrestadores);
            tabControl1.TabPages.Add(tabPacientes);
            tabControl1.TabPages.Add(tabMedicos);
            tabControl1.TabPages.Add(tabEspecialidades);
            tabControl1.TabPages.Add(tabConsultorios);
            tabControl1.TabPages.Add(tabTurnos);
            tabControl1.TabPages.Add(tabAdministradores);
            tabControl1.TabPages.Add(tabReportes);

            Controls.Add(tabControl1);

            // Conectar handlers CRUD a las grillas que usan tablas
            AttachCrudHandlers(dgvPrestadores, "prestadores", LoadPrestadores);
            AttachCrudHandlers(dgvEspecialidades, "especialidades", LoadEspecialidades);
            AttachCrudHandlers(dgvConsultorios, "consultorios", LoadConsultorios);
            AttachCrudHandlers(dgvAdministradores, "administradores", LoadAdministradores);
            AttachCrudHandlers(dgvPacientes, "pacientes", LoadPacientes);
            AttachCrudHandlers(dgvMedicos, "medicos", LoadMedicos);
            
            SetupTurnosTab();

            SetupReportTab();

            this.FormClosed += FrmAdministrador_FormClosed;

        }

        private void FrmAdministrador_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private DataGridView CreateDefaultGrid(string name)
        {
            var dgv = new DataGridView
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AllowUserToAddRows = true,
                AllowUserToDeleteRows = true,
                ReadOnly = false,
                AutoGenerateColumns = true
            };
            return dgv;
        }

        private void AttachCrudHandlers(DataGridView dgv, string tableName, Action reloadAction)
        {
            if (dgv == null)
                return;

            // Mask display and editing for Password column:
            dgv.CellFormatting += (s, e) =>
            {
                try
                {
                    var grid = (DataGridView)s;
                    if (e.RowIndex < 0 || e.ColumnIndex < 0)
                        return;

                    var col = grid.Columns[e.ColumnIndex];
                    var colNameFmt = string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.Name : col.DataPropertyName;
                    if (!string.Equals(colNameFmt, "Password", StringComparison.OrdinalIgnoreCase))
                        return;

                    // Avoid masking while editing
                    var cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.IsInEditMode)
                        return;

                    var real = cell.Value?.ToString() ?? "";
                    // show a fixed minimal mask length to avoid leaking length, or match length if acceptable
                    var mask = new string('•', Math.Max(4, real.Length));
                    e.Value = mask;
                    e.FormattingApplied = true;
                }
                catch
                {
                    // swallow formatting errors to avoid interfering with grid rendering
                }
            };

            dgv.EditingControlShowing += (s, e) =>
            {
                try
                {
                    var grid = (DataGridView)s;
                    int colIndex = grid.CurrentCell?.ColumnIndex ?? -1;
                    if (colIndex < 0)
                        return;

                    var col = grid.Columns[colIndex];
                    var colNameEdit = string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.Name : col.DataPropertyName;

                    var tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        // password masking (existing behavior)
                        tb.UseSystemPasswordChar = string.Equals(colNameEdit, "Password", StringComparison.OrdinalIgnoreCase);

                        // Detach previous numeric handler to avoid duplicates
                        tb.KeyPress -= NumericKeyPress;

                        // Attach numeric-only handler for DNI, Matricula and NumeroConsultorio

                        /*
                        if (string.Equals(colNameEdit, "DNI", StringComparison.OrdinalIgnoreCase) ||
                           str ing.Equals(colNameEdit, "Matricula", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colNameEdit, "NumeroConsultorio", StringComparison.OrdinalIgnoreCase))
                        { 
                            tb.KeyPress += NumericKeyPress;
                        }*/
                    }
                }
                catch
                {
                    // ignore editing-control errors
                }

                // Local helper for numeric-only input
                void NumericKeyPress(object sender, KeyPressEventArgs ke)
                {
                    // Allow control keys (backspace, arrows) and digits only
                    if (!char.IsControl(ke.KeyChar) && !char.IsDigit(ke.KeyChar))
                    {
                        ke.Handled = true;
                    }
                }
            };

        }
        
        private void LoadPrestadores()
        {
            BindingList<Prestador> prestadores = new BindingList<Prestador>(_prestadorController.ObtenerTodos());
            dgvPrestadores.DataSource = prestadores;
            GridHelper.ConfigurarColumnasPrestadores(dgvPrestadores);
            GridHelper.ConfigureIdColumn(dgvPrestadores, visible: true, allowEdit: false);
            GridHelper.HabilitarEdicionInline(
                dgvPrestadores,
                prestadores,
                _prestadorController,
                "Nombre"
            );
            GridHelper.HabilitarEliminacionConConfirmacion(dgvPrestadores, 
                prestadores,
                id => _prestadorController.Eliminar(id));
        }

        private void LoadEspecialidades()
        {
            BindingList<Especialidad> especialidades = new BindingList<Especialidad>(_especialidadController.ObtenerTodos());
            dgvEspecialidades.DataSource = especialidades;
            GridHelper.ConfigurarColumnasEspecialidades(dgvEspecialidades);
            GridHelper.ConfigureIdColumn(dgvEspecialidades, visible: true, allowEdit: false);
            GridHelper.HabilitarEdicionInline(
                dgvEspecialidades,
                especialidades,
                _especialidadController,
                "Nombre"
            );
            GridHelper.HabilitarEliminacionConConfirmacion(dgvEspecialidades,
                especialidades,
                id => _especialidadController.Eliminar(id));
        }

        private void LoadConsultorios()
        {
            BindingList<Consultorio> consultorios = new BindingList<Consultorio>(_consultorioController.ObtenerTodos());
            dgvConsultorios.DataSource = consultorios;
            GridHelper.ConfigurarColumnasConsultorios(dgvConsultorios);
            GridHelper.ConfigureIdColumn(dgvConsultorios, visible: true, allowEdit: false);
            GridHelper.HabilitarEdicionInline(
                dgvConsultorios,
                consultorios,
                _consultorioController,
                "Nombre", "Direccion"
            );
            GridHelper.HabilitarEliminacionConConfirmacion(dgvConsultorios,
                consultorios,
                id => _consultorioController.Eliminar(id));
        }

        private void LoadAdministradores()
        {
            BindingList<Administrador> administradores = new BindingList<Administrador>(_administradorController.ObtenerTodos());
            dgvAdministradores.DataSource = administradores;
            GridHelper.ConfigurarColumnasAdministradores(dgvAdministradores);
            GridHelper.ConfigureIdColumn(dgvAdministradores, visible: true, allowEdit: false);
            GridHelper.HabilitarEdicionInline(
                dgvAdministradores,
                administradores,
                _administradorController,
                "Nombre", "Apellido"
            );
            GridHelper.HabilitarEliminacionConConfirmacion(dgvAdministradores,
                administradores,
                id => _administradorController.Eliminar(id));
        }

        private void LoadPacientes()
        {
            List<Prestador> prestadores = _prestadorController.ObtenerTodos();

            var pacientes = new BindingList<Paciente>(_pacienteController.ObtenerTodos());

            // 1. Configurar columnas (aún sin Prestador como combo)
            GridHelper.ConfigurarColumnasPacientes(dgvPacientes, prestadores);

            // 2. CONVERTIR a ComboBox ANTES del DataSource !!!
            GridHelper.HabilitarComboConActualizacion(
                dgvPacientes,
                "Prestador",
                prestadores.ToList()
            );

            // 3. AHORA sí asignar DataSource (aquí se crean las celdas correctas)
            dgvPacientes.DataSource = pacientes;

            // 4. Resto de configuraciones
            GridHelper.ConfigureIdColumn(dgvPacientes, visible: true, allowEdit: false);
            GridHelper.HabilitarEdicionInline(dgvPacientes, pacientes, _pacienteController, "Nombre", "Apellido");
            GridHelper.HabilitarEliminacionConConfirmacion(dgvPacientes, pacientes, id => _pacienteController.Eliminar(id));
        }

        private void LoadMedicos()
        {
            List<Prestador> prestadores = _prestadorController.ObtenerTodos();
            List<Especialidad> especialidades = _especialidadController.ObtenerTodos();

            var medicos = new BindingList<Medico>(_medicoController.ObtenerTodos());

            // 1. Configurar columnas (aún sin Prestador como combo)
            GridHelper.ConfigurarColumnasMedicos(dgvMedicos);

            // 2. CONVERTIR a ComboBox ANTES del DataSource !!!
            GridHelper.HabilitarComboConActualizacion(
                dgvMedicos,
                "Prestador",
                prestadores.Cast<object>().ToList()
            );

            GridHelper.HabilitarComboConActualizacion(
                dgvMedicos,
                "Especialidad",
                especialidades.Cast<object>().ToList()
            );

            // 3. AHORA sí asignar DataSource (aquí se crean las celdas correctas)
            dgvMedicos.DataSource = medicos;

            // 4. Resto de configuraciones
            GridHelper.ConfigureIdColumn(dgvMedicos, visible: true, allowEdit: false);
            GridHelper.HabilitarEdicionInline(dgvMedicos, medicos, _medicoController, "Nombre", "Apellido");
            GridHelper.HabilitarEliminacionConConfirmacion(dgvMedicos, medicos, id => _medicoController.Eliminar(id));
        }

        private void SetupTurnosTab()
        {
            // Top panel (controls)
            pnlTurnosTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160,
                Padding = new Padding(8)
            };

            // Month/year picker (select month to view)
            dtpMonthYear = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MMMM yyyy",
                ShowUpDown = true,
                Width = 140,
                Left = 8,
                Top = 8
            };

            btnLoadTurnos = new Button
            {
                Text = "Cargar turnos (mes)",
                Left = dtpMonthYear.Right + 8,
                Top = dtpMonthYear.Top - 2,
                Width = 140
            };
            btnLoadTurnos.Click += (s, e) => LoadTurnos();

            // Generación inputs
            var lblMed = new Label { Text = "Médico:", Left = 8, Top = dtpMonthYear.Bottom + 10, AutoSize = true };
            cbMedicoTurnos = new ComboBox { Left = lblMed.Right + 8, Top = lblMed.Top - 3, Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblCons = new Label { Text = "Consultorio:", Left = cbMedicoTurnos.Right + 12, Top = lblMed.Top, AutoSize = true };
            cbConsultorioTurnos = new ComboBox { Left = lblCons.Right + 8, Top = lblMed.Top - 3, Width = 380, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblHoraIni = new Label { Text = "Hora inicio:", Left = 8, Top = lblMed.Bottom + 10, AutoSize = true };
            dtpHoraInicio = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true, Left = lblHoraIni.Right + 8, Top = lblHoraIni.Top - 3, Width = 90, Value = DateTime.Today.AddHours(8) };

            var lblHoraFin = new Label { Text = "Hora fin:", Left = dtpHoraInicio.Right + 12, Top = lblHoraIni.Top, AutoSize = true };
            dtpHoraFin = new DateTimePicker { Format = DateTimePickerFormat.Time, ShowUpDown = true, Left = lblHoraFin.Right + 8, Top = lblHoraIni.Top - 3, Width = 90, Value = DateTime.Today.AddHours(17) };

            // Weekday checkboxes (Mon-Fri)
            chkLunes = new CheckBox { Text = "Lun", Left = dtpHoraFin.Right + 16, Top = lblHoraIni.Top - 3, AutoSize = true };
            chkMartes = new CheckBox { Text = "Mar", Left = chkLunes.Right + 3, Top = chkLunes.Top, AutoSize = true };
            chkMiercoles = new CheckBox { Text = "Mié", Left = chkMartes.Right + 3, Top = chkLunes.Top, AutoSize = true };
            chkJueves = new CheckBox { Text = "Jue", Left = chkMiercoles.Right + 3, Top = chkLunes.Top, AutoSize = true };
            chkViernes = new CheckBox { Text = "Vie", Left = chkJueves.Right + 3, Top = chkLunes.Top, AutoSize = true };

            btnGenerateTurnos = new Button { Text = "Generar turnos", Left = chkViernes.Right + 12, Top = chkLunes.Top - 3, Width = 120 };
            btnGenerateTurnos.Click += (s, e) => GenerateTurnos();

            // Add controls to top panel
            pnlTurnosTop.Controls.Add(dtpMonthYear);
            pnlTurnosTop.Controls.Add(btnLoadTurnos);
            pnlTurnosTop.Controls.Add(lblMed);
            pnlTurnosTop.Controls.Add(cbMedicoTurnos);
            pnlTurnosTop.Controls.Add(lblCons);
            pnlTurnosTop.Controls.Add(cbConsultorioTurnos);
            pnlTurnosTop.Controls.Add(lblHoraIni);
            pnlTurnosTop.Controls.Add(dtpHoraInicio);
            pnlTurnosTop.Controls.Add(lblHoraFin);
            pnlTurnosTop.Controls.Add(dtpHoraFin);
            pnlTurnosTop.Controls.Add(chkLunes);
            pnlTurnosTop.Controls.Add(chkMartes);
            pnlTurnosTop.Controls.Add(chkMiercoles);
            pnlTurnosTop.Controls.Add(chkJueves);
            pnlTurnosTop.Controls.Add(chkViernes);
            pnlTurnosTop.Controls.Add(btnGenerateTurnos);

            // Bottom panel (grid)
            pnlTurnosBottom = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            dgvTurnos = CreateDefaultGrid("dgvTurnos");
            dgvTurnos.ReadOnly = true;
            dgvTurnos.AllowUserToAddRows = false;
            dgvTurnos.AllowUserToDeleteRows = false;
            dgvTurnos.AutoGenerateColumns = true;
            dgvTurnos.Dock = DockStyle.Fill;
            pnlTurnosBottom.Controls.Add(dgvTurnos);

            dgvTurnos.KeyDown += DgvTurnos_KeyDown;

            // Put panels into the tabTurnos page
            tabTurnos.Controls.Add(pnlTurnosBottom);
            tabTurnos.Controls.Add(pnlTurnosTop);

            // Populate medicos and consultorios lists for the generator
            LoadMedicosForTurnos();
            LoadConsultoriosForTurnos();

            // Load current month by default
            dtpMonthYear.Value = DateTime.Today;
            LoadTurnos();
        }

        private void DgvTurnos_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode != Keys.Delete)
                return;

            var grid = (DataGridView)sender;
            if (grid.SelectedRows == null || grid.SelectedRows.Count == 0)
                return;

            var rows = grid.SelectedRows.Cast<DataGridViewRow>().ToList();
            var deletableIds = new List<int>();

            // First pass: validate all selected rows and collect ids to delete
            foreach (var r in rows)
            {
                if (r.Cells["Id"] == null || r.Cells["Id"].Value == null || r.Cells["Id"].Value == DBNull.Value)
                    continue;

                if (!int.TryParse(r.Cells["Id"].Value.ToString(), out int id))
                    continue;

                try
                {
                    DataTable dt = Database.Consultar($"SELECT Paciente FROM turnos WHERE Id = {id} LIMIT 1");
                    // allow delete only if no Paciente assigned
                    if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Paciente"] == DBNull.Value || string.IsNullOrWhiteSpace(dt.Rows[0]["Paciente"]?.ToString()))
                    {
                        deletableIds.Add(id);
                    }
                }
                catch
                {
                    // ignore this row on DB error (or optionally surface error per row)
                }
            }

            if (deletableIds.Count == 0)
            {
                MessageBox.Show("No hay turnos seleccionados que puedan eliminarse (tienen pacientes asignados).", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"Eliminar {deletableIds.Count} turno(s) seleccionados?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            foreach (var id in deletableIds)
            {
                try
                {
                    Database.Ejecutar($"DELETE FROM turnos WHERE Id = {id}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar turno {id}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Refresh grid after deletes
            LoadTurnos();*/
        }

        private void LoadMedicosForTurnos()
        {

            var medicos = new BindingList<Medico>(_medicoController.ObtenerTodos());
            cbMedicoTurnos.DataSource = medicos;
            cbMedicoTurnos.ValueMember = "Id";
            cbMedicoTurnos.DisplayMember = "";

        }

        private void LoadConsultoriosForTurnos()
        {

            var consultorios = new BindingList<Consultorio>(_consultorioController.ObtenerTodos());
            cbConsultorioTurnos.DataSource = consultorios;
            cbConsultorioTurnos.ValueMember = "Id";
            cbConsultorioTurnos.DisplayMember = "";

        }

        private void LoadTurnos()
        {  /*
            try
            {
                var first = new DateTime(dtpMonthYear.Value.Year, dtpMonthYear.Value.Month, 1);
                var last = first.AddMonths(1).AddDays(-1).Date.AddSeconds(86399); // end of last day
                string sql = $"SELECT " +
                    $"turnos.Id, " +
                    $"turnos.Fecha, " +
                    $"CONCAT(medicos.Apellido, ', ', medicos.Nombre) as MedicoNombre, " +
                    $"especialidades.Nombre as MedicoEspecialidad, " +
                    $"CONCAT(consultorios.Nombre, ' ', consultorios.Direccion, ' No. ', CAST(consultorios.NumeroConsultorio AS CHAR)) AS Consultorio, " +
                    $"prestadores_medico.Nombre as PrestadorMedico, " +
                    $"turnos.PrecioConsulta, " +
                    $"CONCAT(pacientes.apellido, ', ', pacientes.nombre) as PacienteNombre, " +
                    $"prestadores_paciente.nombre as PrestadorPaciente, " +
                    $"CASE WHEN Estado = 1 THEN 'Presente' WHEN Estado = 2  THEN 'Ausente'  WHEN Paciente IS NOT NULL THEN 'En curso' WHEN Fecha < NOW() THEN '' ELSE 'Disponible'  END as Estado" +
                    $" FROM turnos " +
                    $" JOIN medicos ON medicos.Id = turnos.Medico " +
                    $" JOIN especialidades ON especialidades.Id = turnos.Especialidad " +
                    $" JOIN consultorios ON consultorios.Id = turnos.Consultorio " +
                    $" JOIN prestadores as prestadores_medico ON prestadores_medico.Id = turnos.PrestadorMedico " +
                    $" LEFT JOIN pacientes ON pacientes.Id = turnos.Paciente " +
                    $" LEFT JOIN prestadores as prestadores_paciente ON prestadores_paciente.Id = turnos.PrestadorPaciente " + 
                    $"WHERE turnos.Fecha >= '{first:yyyy-MM-dd 00:00:00}' AND turnos.Fecha <= '{last:yyyy-MM-dd HH:mm:ss}' ORDER BY fecha";
                DataTable dt = Database.Consultar(sql);
                dgvTurnos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar turnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void GenerateTurnos()
        {
            /*
            try
            {
                if (cbMedicoTurnos.SelectedValue == null || cbConsultorioTurnos.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione médico y consultorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int medicoId = Convert.ToInt32(cbMedicoTurnos.SelectedValue);
                int consultorioId = Convert.ToInt32(cbConsultorioTurnos.SelectedValue);

                // Get medico data (Especialidad, PrecioConsulta)
                DataTable dtMed = Database.Consultar($"SELECT Especialidad, PrecioConsulta, Prestador FROM medicos WHERE Id = {medicoId} LIMIT 1");
                string especialidad = "";
                decimal precio = 0m;
                string prestador_medico = "";

                if (dtMed != null && dtMed.Rows.Count > 0)
                {
                    especialidad = dtMed.Rows[0]["Especialidad"]?.ToString() ?? "";
                    decimal.TryParse(dtMed.Rows[0]["PrecioConsulta"]?.ToString(), out precio);
                    prestador_medico = dtMed.Rows[0]["Prestador"]?.ToString() ?? "";
                }

                var first = new DateTime(dtpMonthYear.Value.Year, dtpMonthYear.Value.Month, 1);
                var last = first.AddMonths(1).AddDays(-1);

                TimeSpan tStart = dtpHoraInicio.Value.TimeOfDay;
                TimeSpan tEnd = dtpHoraFin.Value.TimeOfDay;
                if (tEnd < tStart)
                {
                    MessageBox.Show("La hora fin debe ser mayor o igual a la hora inicio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var daysSelected = new HashSet<DayOfWeek>();
                if (chkLunes.Checked) daysSelected.Add(DayOfWeek.Monday);
                if (chkMartes.Checked) daysSelected.Add(DayOfWeek.Tuesday);
                if (chkMiercoles.Checked) daysSelected.Add(DayOfWeek.Wednesday);
                if (chkJueves.Checked) daysSelected.Add(DayOfWeek.Thursday);
                if (chkViernes.Checked) daysSelected.Add(DayOfWeek.Friday);

                if (daysSelected.Count == 0)
                {
                    MessageBox.Show("Seleccione al menos un día de la semana.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var inserts = new List<string>();
                for (var date = first.Date; date <= last.Date; date = date.AddDays(1))
                {
                    if (!daysSelected.Contains(date.DayOfWeek))
                        continue;

                    for (var ts = tStart; ts <= tEnd; ts = ts.Add(TimeSpan.FromMinutes(20)))
                    {
                        var fecha = date.Date + ts;
                        // Build INSERT: fecha, Medico, Especialidad, Consultorio, PrecioConsulta
                        var fechaSql = fecha.ToString("yyyy-MM-dd HH:mm:ss");
                        var especialEsc = EscapeSql(especialidad);
                        var sql = $"INSERT IGNORE INTO turnos (fecha, Medico, Especialidad, Consultorio, PrecioConsulta, PrestadorMedico) VALUES ('{fechaSql}', {medicoId}, '{especialEsc}', {consultorioId}, {precio.ToString(System.Globalization.CultureInfo.InvariantCulture)}, {prestador_medico})";
                        inserts.Add(sql);
                    }
                }

                if (inserts.Count == 0)
                {
                    MessageBox.Show("No se generaron turnos con los parámetros indicados.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Execute inserts inside a simple loop (could be optimized into batch if needed)
                foreach (var ins in inserts)
                {
                    Database.Ejecutar(ins);
                }

                MessageBox.Show($"Generados {inserts.Count} turnos.", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload grid
                LoadTurnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar turnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void SetupReportTab()
        {/*
            try
            {
                tabReportes.Controls.Clear();

                pnlReportParams = new Panel { Dock = DockStyle.Top, Height = 64, Padding = new Padding(8) };

                var lblMed = new Label { Text = "Médico:", Left = 8, Top = 8, AutoSize = true };
                cbReportMedico = new ComboBox { Left = lblMed.Right + 8, Top = lblMed.Top - 3, Width = 260, DropDownStyle = ComboBoxStyle.DropDownList };

                var lblFrom = new Label { Text = "Desde:", Left = cbReportMedico.Right + 12, Top = lblMed.Top, AutoSize = true };
                dtpReportFrom = new DateTimePicker { Format = DateTimePickerFormat.Short, Left = lblFrom.Right + 8, Top = lblMed.Top - 3, Width = 100, Value = DateTime.Today.AddDays(-7) };

                var lblTo = new Label { Text = "Hasta:", Left = dtpReportFrom.Right + 8, Top = lblMed.Top, AutoSize = true };
                dtpReportTo = new DateTimePicker { Format = DateTimePickerFormat.Short, Left = lblTo.Right + 8, Top = lblMed.Top - 3, Width = 100, Value = DateTime.Today };

                chkReportShowDetail = new CheckBox { Text = "Mostrar detalle", Left = dtpReportTo.Right + 12, Top = lblMed.Top - 3, AutoSize = true, Checked = true };

                btnRunReport = new Button { Text = "Generar reporte", Left = chkReportShowDetail.Right + 12, Top = lblMed.Top - 6, Width = 120 };
                btnRunReport.Click += (s, e) => RunReport();

                pnlReportParams.Controls.Add(lblMed);
                pnlReportParams.Controls.Add(cbReportMedico);
                pnlReportParams.Controls.Add(lblFrom);
                pnlReportParams.Controls.Add(dtpReportFrom);
                pnlReportParams.Controls.Add(lblTo);
                pnlReportParams.Controls.Add(dtpReportTo);
                pnlReportParams.Controls.Add(chkReportShowDetail);
                pnlReportParams.Controls.Add(btnRunReport);

                pnlReportResults = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };

                dgvReportResults = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AutoGenerateColumns = true,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect
                };

                lblReportTotal = new Label { Dock = DockStyle.Bottom, Height = 28, TextAlign = ContentAlignment.MiddleRight, Padding = new Padding(0, 4, 8, 0) };

                pnlReportResults.Controls.Add(dgvReportResults);
                pnlReportResults.Controls.Add(lblReportTotal);

                tabReportes.Controls.Add(pnlReportResults);
                tabReportes.Controls.Add(pnlReportParams);

                // Populate medicos combo (allow empty = all)
                try
                {
                    DataTable dt = Database.Consultar("SELECT Id, CONCAT(Apellido, ', ', Nombre) AS DisplayName FROM medicos ORDER BY Apellido, Nombre");
                    if (dt != null)
                    {
                        var rowAll = dt.NewRow();
                        rowAll["Id"] = DBNull.Value;
                        rowAll["DisplayName"] = "-- Todos --";
                        dt.Rows.InsertAt(rowAll, 0);

                        cbReportMedico.DataSource = dt;
                        cbReportMedico.ValueMember = "Id";
                        cbReportMedico.DisplayMember = "DisplayName";
                    }
                }
                catch
                {
                    // ignore load failures for combo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar Reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void RunReport()
        {
            /*
            try
            {
                DateTime start = dtpReportFrom.Value.Date;
                DateTime endExclusive = dtpReportTo.Value.Date.AddDays(1); // half-open

                int? medicoFilter = null;
                if (cbReportMedico.SelectedValue != null && cbReportMedico.SelectedValue != DBNull.Value)
                {
                    int parsed;
                    if (int.TryParse(cbReportMedico.SelectedValue.ToString(), out parsed))
                        medicoFilter = parsed;
                }

                string whereBase = "WHERE turnos.Paciente IS NOT NULL AND Estado = 1 " +
                                   $"AND turnos.Fecha >= '{start:yyyy-MM-dd 00:00:00}' AND turnos.Fecha < '{endExclusive:yyyy-MM-dd 00:00:00}'";
                if (medicoFilter.HasValue)
                    whereBase += $" AND turnos.Medico = {medicoFilter.Value}";

                string detailSql = "SELECT " +
                             "turnos.Id, " +
                             "turnos.Fecha, " +
                             "CONCAT(medicos.Apellido, ', ', medicos.Nombre) AS MedicoNombre, " +
                             "especialidades.Nombre AS Especialidad, " +
                             "CONCAT(consultorios.Nombre, ' ', consultorios.Direccion, ' ', consultorios.NumeroConsultorio) AS Consultorio, " +
                             "turnos.PrecioConsulta, " +
                             "CASE WHEN turnos.PrestadorMedico IS NOT NULL AND turnos.PrestadorPaciente IS NOT NULL AND turnos.PrestadorMedico = turnos.PrestadorPaciente " +
                             "THEN ROUND(turnos.PrecioConsulta * 0.5, 2) ELSE 0 END AS Bonificacion, " +
                             "ROUND(turnos.PrecioConsulta - (CASE WHEN turnos.PrestadorMedico IS NOT NULL AND turnos.PrestadorPaciente IS NOT NULL AND turnos.PrestadorMedico = turnos.PrestadorPaciente THEN turnos.PrecioConsulta * 0.5 ELSE 0 END), 2) AS PrecioFinal, " +
                             "CONCAT(pacientes.Apellido, ', ', pacientes.Nombre) AS PacienteNombre " +
                             "FROM turnos " +
                             "JOIN medicos ON medicos.Id = turnos.Medico " +
                             "JOIN especialidades ON especialidades.Id = turnos.Especialidad " +
                             "JOIN consultorios ON consultorios.Id = turnos.Consultorio " +
                             "LEFT JOIN pacientes ON pacientes.Id = turnos.Paciente " +
                             $"{whereBase} " +
                             "ORDER BY turnos.Fecha;";

                string aggSql = "SELECT " +
                                "COUNT(*) AS TurnosCount, " +
                                "IFNULL(SUM(turnos.PrecioConsulta),0) AS TotalPrecio, " +
                                "IFNULL(SUM(CASE WHEN turnos.PrestadorMedico IS NOT NULL AND turnos.PrestadorPaciente IS NOT NULL AND turnos.PrestadorMedico = turnos.PrestadorPaciente THEN ROUND(turnos.PrecioConsulta * 0.5,2) ELSE 0 END),0) AS TotalBonificacion, " +
                                "IFNULL(SUM(ROUND(turnos.PrecioConsulta - (CASE WHEN turnos.PrestadorMedico IS NOT NULL AND turnos.PrestadorPaciente IS NOT NULL AND turnos.PrestadorMedico = turnos.PrestadorPaciente THEN turnos.PrecioConsulta * 0.5 ELSE 0 END),2)),0) AS TotalPrecioFinal " +
                                "FROM turnos " +
                                $"{whereBase};";

                DataTable dtDetail = Database.Consultar(detailSql);
                DataTable dtAgg = Database.Consultar(aggSql);

                int count = 0;
                decimal totalPrecio = 0m;
                decimal totalBonificacion = 0m;
                decimal totalPrecioFinal = 0m;

                if (dtAgg != null && dtAgg.Rows.Count > 0)
                {
                    var a = dtAgg.Rows[0];
                    int.TryParse(a["TurnosCount"]?.ToString() ?? "0", out count);
                    decimal.TryParse(a["TotalPrecio"]?.ToString() ?? "0", System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out totalPrecio);
                    decimal.TryParse(a["TotalBonificacion"]?.ToString() ?? "0", System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out totalBonificacion);
                    decimal.TryParse(a["TotalPrecioFinal"]?.ToString() ?? "0", System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out totalPrecioFinal);
                }

                // 666 lblReportTotal.Text = $Count: {count}    Total: {totalPrecio.ToString("N2")}    Bonificación: {totalBonificacion.ToString("N2")}    PrecioFinal: {totalPrecioFinal.ToString("N2")}';);

                if (chkReportShowDetail.Checked)
                {
                    if (dtDetail == null)
                    {
                        dgvReportResults.DataSource = null;
                        return;
                    }

                    // Append totals row to detail table
                    var totalsRow = dtDetail.NewRow();

                    // Try to populate commonly present columns; fall back to first/last columns
                    if (dtDetail.Columns.Contains("MedicoNombre"))
                        totalsRow["MedicoNombre"] = "TOTAL";
                    else if (dtDetail.Columns.Count > 0)
                        totalsRow[0] = "TOTAL";

                    if (dtDetail.Columns.Contains("PrecioConsulta"))
                        totalsRow["PrecioConsulta"] = totalPrecio;
                    if (dtDetail.Columns.Contains("Bonificacion"))
                        totalsRow["Bonificacion"] = totalBonificacion;
                    if (dtDetail.Columns.Contains("PrecioFinal"))
                        totalsRow["PrecioFinal"] = totalPrecioFinal;

                    dtDetail.Rows.Add(totalsRow);

                    dgvReportResults.DataSource = dtDetail;

                    // Format numeric columns if they exist
                    if (dgvReportResults.Columns.Contains("PrecioFinal"))
                        dgvReportResults.Columns["PrecioFinal"].DefaultCellStyle.Format = "N2";
                    if (dgvReportResults.Columns.Contains("PrecioConsulta"))
                        dgvReportResults.Columns["PrecioConsulta"].DefaultCellStyle.Format = "N2";
                    if (dgvReportResults.Columns.Contains("Bonificacion"))
                        dgvReportResults.Columns["Bonificacion"].DefaultCellStyle.Format = "N2";

                    // Optionally style the last row (totals) to make it stand out
                    if (dgvReportResults.Rows.Count > 0)
                    {
                        var lastIndex = dgvReportResults.Rows.Count - 1;
                        dgvReportResults.Rows[lastIndex].DefaultCellStyle.Font = new Font(dgvReportResults.Font, FontStyle.Bold);
                    }
                }
                else
                {
                    // Show a one-row summary table when detail is hidden
                    var summary = new DataTable();
                    summary.Columns.Add("Turnos", typeof(int));
                    summary.Columns.Add("TotalPrecio", typeof(decimal));
                    summary.Columns.Add("TotalBonificacion", typeof(decimal));
                    summary.Columns.Add("TotalPrecioFinal", typeof(decimal));

                    var r = summary.NewRow();
                    r["Turnos"] = count;
                    r["TotalPrecio"] = totalPrecio;
                    r["TotalBonificacion"] = totalBonificacion;
                    r["TotalPrecioFinal"] = totalPrecioFinal;
                    summary.Rows.Add(r);

                    dgvReportResults.DataSource = summary;
                    if (dgvReportResults.Columns.Contains("TotalPrecioFinal"))
                        dgvReportResults.Columns["TotalPrecioFinal"].DefaultCellStyle.Format = "N2";
                    if (dgvReportResults.Columns.Contains("TotalPrecio"))
                        dgvReportResults.Columns["TotalPrecio"].DefaultCellStyle.Format = "N2";
                    if (dgvReportResults.Columns.Contains("TotalBonificacion"))
                        dgvReportResults.Columns["TotalBonificacion"].DefaultCellStyle.Format = "N2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

    }

}