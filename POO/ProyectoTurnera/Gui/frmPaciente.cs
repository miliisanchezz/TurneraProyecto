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
                AllowUserToDeleteRows = false,        // deletion handled via Delete key
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvPendientes.KeyDown += DgvPendientes_KeyDown;

            tabPendientes.Controls.Add(dgvPendientes);

            // Gestionar top panel
            pnlGestionTop = new Panel { Dock = DockStyle.Top, Height = 72, Padding = new Padding(6) };

            var lblEsp = new Label { Text = "Especialidad:", Left = 8, Top = 12, AutoSize = true };
            cbEspecialidad = new ComboBox { Left = lblEsp.Right + 8, Top = lblEsp.Top - 3, Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };

            var lblMes = new Label { Text = "Mes:", Left = cbEspecialidad.Right + 12, Top = lblEsp.Top, AutoSize = true };
            dtpGestionMonth = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MMMM yyyy",
                ShowUpDown = true,
                Left = lblMes.Right + 8,
                Top = lblEsp.Top - 3,
                Width = 140,
                Value = DateTime.Today
            };

            btnBuscarDisponibles = new Button { Text = "Buscar disponibles", Left = dtpGestionMonth.Right + 12, Top = lblEsp.Top - 3, Width = 140 };
            btnBuscarDisponibles.Click += (s, e) => LoadDisponibles();

            pnlGestionTop.Controls.Add(lblEsp);
            pnlGestionTop.Controls.Add(cbEspecialidad);
            pnlGestionTop.Controls.Add(lblMes);
            pnlGestionTop.Controls.Add(dtpGestionMonth);
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
        { /*
            try
            {
                if (paciente.Id <= 0)
                {
                    dgvPendientes.DataSource = null;
                    return;
                }

                // Start = tomorrow 00:00:00 (you requested >= tomorrow)
                var start = DateTime.Today.AddDays(1);

                string sql = @"
                    SELECT
                        turnos.Id,
                        turnos.Fecha,
                        CONCAT(medicos.Apellido, ', ', medicos.Nombre) AS MedicoNombre,
                        especialidades.Nombre AS Especialidad,
                        CONCAT(consultorios.Nombre, ' ', consultorios.Direccion, ' ', consultorios.NumeroConsultorio) AS Consultorio,
                        turnos.PrecioConsulta,
                        CASE WHEN turnos.PrestadorMedico IS NOT NULL
                                  AND turnos.PrestadorPaciente IS NOT NULL
                                  AND turnos.PrestadorMedico = turnos.PrestadorPaciente
                           THEN ROUND(turnos.PrecioConsulta * 0.5, 2)
                           ELSE 0 END AS Bonificacion
                    FROM turnos
                    JOIN medicos ON medicos.Id = turnos.Medico
                    JOIN especialidades ON especialidades.Id = turnos.Especialidad
                    JOIN consultorios ON consultorios.Id = turnos.Consultorio
                    WHERE turnos.Paciente = @pacienteId
                      AND turnos.Fecha >= @start
                    ORDER BY turnos.Fecha;";

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(BD.cadena))
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                using (var da = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@pacienteId", paciente.Id);
                    cmd.Parameters.AddWithValue("@start", start);
                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvPendientes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pendientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }

        private void LoadEspecialidades()
        {
            var especialidades = new BindingList<Especialidad>(_especialidadController.ObtenerTodos());
            cbEspecialidad.DataSource = especialidades;
            cbEspecialidad.ValueMember = "Id";
            cbEspecialidad.DisplayMember = "";
        }

        private void LoadDisponibles()
        { /*
            try
            {
                if (cbEspecialidad.SelectedValue == null || cbEspecialidad.SelectedValue == DBNull.Value)
                {
                    MessageBox.Show("Seleccione una especialidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int especialidadId = Convert.ToInt32(cbEspecialidad.SelectedValue);

                // Month range (half-open): [monthStart, monthStart.AddMonths(1))
                var monthStart = new DateTime(dtpGestionMonth.Value.Year, dtpGestionMonth.Value.Month, 1);
                var monthEndExclusive = monthStart.AddMonths(1);

                // Also ensure we start at tomorrow (>= tomorrow)
                var minStart = DateTime.Today.AddDays(1);
                var start = minStart > monthStart ? minStart : monthStart;
                var end = monthEndExclusive;

                // Read patient's Prestador (to compare against turnos.PrestadorMedico)
                int? prestadorPacienteId = null;
                try
                {
                    var dtPac = BD.Consultar($"SELECT Prestador FROM pacientes WHERE Id = {paciente.Id} LIMIT 1");
                    if (dtPac != null && dtPac.Rows.Count > 0 && dtPac.Rows[0]["Prestador"] != DBNull.Value)
                    {
                        int parsed;
                        if (int.TryParse(dtPac.Rows[0]["Prestador"].ToString(), out parsed))
                            prestadorPacienteId = parsed;
                    }
                }
                catch
                {
                    // ignore lookup failure — proceed without prestador filter
                    prestadorPacienteId = null;
                }

                // Build SQL; add optional filter for PrestadorMedico only if patient has one
                var sql = @"
                    SELECT
                        turnos.Id,
                        turnos.Fecha,
                        CONCAT(medicos.Apellido, ', ', medicos.Nombre) AS MedicoNombre,
                        prestadores_medico.Nombre AS Prestador,
                        CONCAT(consultorios.Nombre, ' ', consultorios.Direccion, ' ', consultorios.NumeroConsultorio) AS Consultorio,
                        turnos.PrecioConsulta,
                        CASE WHEN  turnos.PrestadorMedico = @prestadorPacienteId
                        THEN ROUND(turnos.PrecioConsulta * 0.5, 2)
                        ELSE 0 END AS Bonificacion
                    FROM turnos
                    JOIN medicos ON medicos.Id = turnos.Medico
                    JOIN consultorios ON consultorios.Id = turnos.Consultorio
                    LEFT JOIN prestadores AS prestadores_medico ON prestadores_medico.Id = turnos.PrestadorMedico
                    WHERE turnos.Paciente IS NULL
                      AND turnos.Especialidad = @especialidadId
                      AND turnos.Fecha >= @start
                      AND turnos.Fecha < @end
                ";

                sql += " ORDER BY turnos.Fecha;";

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(BD.cadena))
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                using (var da = new MySql.Data.MySqlClient.MySqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@prestadorPacienteId", prestadorPacienteId);
                    cmd.Parameters.AddWithValue("@especialidadId", especialidadId);
                    cmd.Parameters.AddWithValue("@start", start);
                    cmd.Parameters.AddWithValue("@end", end);
                    if (prestadorPacienteId.HasValue)
                        cmd.Parameters.AddWithValue("@prestadorPaciente", prestadorPacienteId.Value);

                    var dt = new DataTable();
                    da.Fill(dt);
                    dgvDisponibles.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar turnos disponibles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }

        private void ReservarTurnoSeleccionado()
        { /*
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

                // Get PrestadorPaciente value from pacientes table (if exists)
                string prestadorPaciente = "NULL";
                DataTable dtPac = BD.Consultar($"SELECT Prestador FROM pacientes WHERE Id = {paciente.Id}");
                if (dtPac != null && dtPac.Rows.Count > 0 && dtPac.Rows[0]["Prestador"] != DBNull.Value && dtPac.Rows[0]["Prestador"] != null)
                {
                    prestadorPaciente = dtPac.Rows[0]["Prestador"].ToString();
                }

                // Update turnos to assign paciente
                string sql = $"UPDATE turnos SET Paciente = {paciente.Id}, PrestadorPaciente = {prestadorPaciente} WHERE Id = {turnoId}";
                BD.Ejecutar(sql);

                MessageBox.Show("Turno reservado correctamente.", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh both lists
                LoadDisponibles();
                LoadPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reservar turno: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }

        // Handle Delete key to remove selected turnos from DB
        private void DgvPendientes_KeyDown(object sender, KeyEventArgs e)
        { /*
            if (e.KeyCode != Keys.Delete)
                return;

            if (dgvPendientes.CurrentRow == null)
                return;

            var rows = dgvPendientes.SelectedRows.Cast<DataGridViewRow>().ToList();
            if (rows.Count == 0)
                rows = new List<DataGridViewRow> { dgvPendientes.CurrentRow };

            var count = rows.Count;
            var confirm = MessageBox.Show($"Eliminar {count} turno(s) seleccionado(s)?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                foreach (var r in rows)
                {
                    if (r.Cells["Id"] == null || r.Cells["Id"].Value == null)
                        continue;

                    int id = Convert.ToInt32(r.Cells["Id"].Value);
                    string sql = $"UPDATE turnos SET Paciente = null, PrestadorPaciente = null WHERE Id = {id}";
                    BD.Ejecutar(sql);
                }

                // Refresh grid after deletes
                LoadPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar turnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }
    }
}