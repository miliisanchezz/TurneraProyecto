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
    public class frmMedico : Form
    {
        private readonly Medico medico;

        private readonly AdministradorController _administradorController = new AdministradorController();
        private readonly PacienteController _pacienteController = new PacienteController();
        private readonly MedicoController _medicoController = new MedicoController();
        private readonly PrestadorController _prestadorController = new PrestadorController();
        private readonly EspecialidadController _especialidadController = new EspecialidadController();
        private readonly ConsultorioController _consultorioController = new ConsultorioController();
        private readonly TurnoController _turnoController = new TurnoController();

        private TabControl tabControl;
        private TabPage tabPendientes;
        private TabPage tabGestionar;
        private TabPage tabSemana;

        private DataGridView dgvPendientes;

        private Panel pnlGestionTop;
        private ComboBox cbPaciente;
        private DateTimePicker dtpGestionMonth;
        private Button btnBuscarPorPaciente;
        private DataGridView dgvPorPaciente;

        private Label lblEstaSemana;
        private DataGridView dgvSemana;

        private Panel pnlPendientesBottom;
        private Button btnMarcarPresente;
        private Button btnMarcarAusente;

        private Panel pnlSemanaTop;
        private Button btnPrevWeek;
        private Button btnNextWeek;
        private Label lblWeekRange;

        private DateTime currentWeekStart;

        public frmMedico(Medico medicologueado)
        {
            this.medico = medicologueado;
            InitializeComponents();
            LoadPendientes();
            LoadTurnos();
            currentWeekStart = ComputeWeekStart(DateTime.Today);
            LoadEstaSemana();
        }


        private void InitializeComponents()
        {
            this.Text = "Médico :: " + this.medico.ToString();

            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(900, 600);

            tabControl = new TabControl { Dock = DockStyle.Fill };

            tabPendientes = new TabPage("Pendientes");
            tabGestionar = new TabPage("Turnos");
            tabSemana = new TabPage("ESTA SEMANA");

            dgvPendientes = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvPendientes.KeyDown += DgvPendientes_KeyDown;

            pnlPendientesBottom = new Panel { Dock = DockStyle.Bottom, Height = 40, Padding = new Padding(6) };
            btnMarcarPresente = new Button { Text = "Marcar presente", Width = 140, Left = 8, Top = 6 };
            btnMarcarPresente.Click += (s, e) => MarkPendientesEstado(1);
            btnMarcarAusente = new Button { Text = "Marcar ausente", Width = 140, Left = btnMarcarPresente.Right + 8, Top = 6 };
            btnMarcarAusente.Click += (s, e) => MarkPendientesEstado(2);
            pnlPendientesBottom.Controls.Add(btnMarcarPresente);
            pnlPendientesBottom.Controls.Add(btnMarcarAusente);

            tabPendientes.Controls.Add(dgvPendientes);
            tabPendientes.Controls.Add(pnlPendientesBottom);

            dgvPorPaciente = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            tabGestionar.Controls.Add(dgvPorPaciente);

            pnlSemanaTop = new Panel { Dock = DockStyle.Top, Height = 36, Padding = new Padding(6) };
            btnPrevWeek = new Button { Text = "< Anterior", Left = 8, Width = 90, Top = 6 };
            btnPrevWeek.Click += (s, e) => { currentWeekStart = currentWeekStart.AddDays(-7); LoadEstaSemana(); };
            btnNextWeek = new Button { Text = "Siguiente >", Left = btnPrevWeek.Right + 8, Width = 90, Top = 6 };
            btnNextWeek.Click += (s, e) => { currentWeekStart = currentWeekStart.AddDays(7); LoadEstaSemana(); };
            lblWeekRange = new Label { Text = "", Left = btnNextWeek.Right + 12, Top = 8, AutoSize = true };
            pnlSemanaTop.Controls.Add(btnPrevWeek);
            pnlSemanaTop.Controls.Add(btnNextWeek);
            pnlSemanaTop.Controls.Add(lblWeekRange);

            lblEstaSemana = new Label { Text = "Turnos — Esta semana", Dock = DockStyle.Top, Height = 24, TextAlign = ContentAlignment.MiddleLeft, Padding = new Padding(8, 0, 0, 0) };
            dgvSemana = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            tabSemana.Controls.Add(dgvSemana);
            tabSemana.Controls.Add(lblEstaSemana);
            tabSemana.Controls.Add(pnlSemanaTop);

            tabControl.TabPages.Add(tabPendientes);
            tabControl.TabPages.Add(tabGestionar);
            tabControl.TabPages.Add(tabSemana);

            Controls.Add(tabControl);

            this.FormClosed += FrmMedico_FormClosed;
        }

        private void FrmMedico_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LoadPendientes()
        {

            var cut = DateTime.Today.AddDays(1);

            BindingList<Turno> pendientes = new BindingList<Turno>(_turnoController.ObtenerFiltrando(null, cut, null, this.medico));

            dgvPendientes.DataSource = pendientes;

            GridHelper.AttachCrudHandlers(dgvPendientes, "pendientes", LoadPendientes);

            GridHelper.ConfigurarColumnasTurnos(dgvPendientes);
            GridHelper.ConfigureIdColumn(dgvPendientes, visible: true, allowEdit: false);

        }

        private void LoadTurnos()
        {

            var cut = DateTime.Today.AddDays(1);

            BindingList<Turno> turnos = new BindingList<Turno>(_turnoController.ObtenerFiltrando(cut, null, null, this.medico));

            dgvPorPaciente.DataSource = turnos;

            GridHelper.AttachCrudHandlers(dgvPorPaciente, "turnos", LoadTurnos);

            GridHelper.ConfigurarColumnasTurnos(dgvPorPaciente);
            GridHelper.ConfigureIdColumn(dgvPorPaciente, visible: true, allowEdit: false);

        }

        private DateTime ComputeWeekStart(DateTime date)
        {
            int diff = (7 + (int)date.DayOfWeek - (int)DayOfWeek.Monday) % 7;
            return date.AddDays(-diff).Date;
        }

        private void LoadEstaSemana()
        { /*
            try
            {
                var weekStart = currentWeekStart;
                var weekEndExclusive = weekStart.AddDays(7);

                lblWeekRange.Text = $"{weekStart:yyyy-MM-dd} → {weekEndExclusive.AddDays(-1):yyyy-MM-dd}";

                TimeSpan? minTime = null;
                TimeSpan? maxTime = null;
                string timeBoundsSql = @"
                    SELECT MIN(TIME(Fecha)) AS MinTime, MAX(TIME(Fecha)) AS MaxTime
                    FROM turnos
                    WHERE Medico = @medicoId
                      AND Fecha >= @start
                      AND Fecha < @end;";

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(BD.cadena))
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(timeBoundsSql, conn))
                {
                    cmd.Parameters.AddWithValue("@medicoId", medico.Id);
                    cmd.Parameters.AddWithValue("@start", weekStart);
                    cmd.Parameters.AddWithValue("@end", weekEndExclusive);
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            if (!rdr.IsDBNull(0))
                                minTime = rdr.GetTimeSpan(0);
                            if (!rdr.IsDBNull(1))
                                maxTime = rdr.GetTimeSpan(1);
                        }
                    }
                }

                if (!minTime.HasValue || !maxTime.HasValue)
                {
                    dgvSemana.DataSource = null;
                    return;
                }

                var step = TimeSpan.FromMinutes(20);
                var slots = new List<TimeSpan>();
                for (var ts = minTime.Value; ts <= maxTime.Value; ts = ts.Add(step))
                    slots.Add(ts);

                var dt = new DataTable();
                dt.Columns.Add("Hora", typeof(string));
                dt.Columns.Add("Lun", typeof(string));
                dt.Columns.Add("Mar", typeof(string));
                dt.Columns.Add("Mié", typeof(string));
                dt.Columns.Add("Jue", typeof(string));
                dt.Columns.Add("Vie", typeof(string));

                foreach (var slot in slots)
                {
                    var r = dt.NewRow();
                    r["Hora"] = slot.ToString(@"hh\:mm");
                    r["Lun"] = "";
                    r["Mar"] = "";
                    r["Mié"] = "";
                    r["Jue"] = "";
                    r["Vie"] = "";
                    dt.Rows.Add(r);
                }

                string sql = @"
                    SELECT Fecha,
                           pacientes.Id AS PacienteId,
                           CONCAT(pacientes.Apellido, ', ', pacientes.Nombre) AS PacienteNombre,
                           CONCAT(consultorios.Nombre, ' ', consultorios.Direccion, ' ', consultorios.NumeroConsultorio) AS Consultorio
                    FROM turnos
                    LEFT JOIN pacientes ON pacientes.Id = turnos.Paciente
                    LEFT JOIN consultorios ON consultorios.Id = turnos.Consultorio
                    WHERE turnos.Medico = @medicoId
                      AND turnos.Fecha >= @start
                      AND turnos.Fecha < @end
                    ORDER BY turnos.Fecha;";

                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(BD.cadena))
                using (var cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@medicoId", medico.Id);
                    cmd.Parameters.AddWithValue("@start", weekStart);
                    cmd.Parameters.AddWithValue("@end", weekEndExclusive);

                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var fecha = rdr.GetDateTime("Fecha");
                            var time = fecha.TimeOfDay;
                            var day = fecha.DayOfWeek;
                            string patientName = rdr.IsDBNull(rdr.GetOrdinal("PacienteNombre")) ? "" : rdr.GetString("PacienteNombre");
                            string consultorio = rdr.IsDBNull(rdr.GetOrdinal("Consultorio")) ? "" : rdr.GetString("Consultorio");

                            int colIndex = -1;
                            switch (day)
                            {
                                case DayOfWeek.Monday: colIndex = 1; break;
                                case DayOfWeek.Tuesday: colIndex = 2; break;
                                case DayOfWeek.Wednesday: colIndex = 3; break;
                                case DayOfWeek.Thursday: colIndex = 4; break;
                                case DayOfWeek.Friday: colIndex = 5; break;
                                default: continue;
                            }

                            var timeKey = time.ToString(@"hh\:mm");
                            var rows = dt.Select($"Hora = '{timeKey}'");
                            string cellText = string.IsNullOrWhiteSpace(patientName) ? "" : (string.IsNullOrWhiteSpace(consultorio) ? patientName : $"{patientName} ({consultorio})");

                            if (rows.Length == 1)
                            {
                                rows[0][colIndex] = cellText;
                            }
                            else
                            {
                                var matchRow = dt.Rows.Cast<DataRow>().FirstOrDefault(r => TimeSpan.TryParseExact(r["Hora"].ToString(), @"hh\:mm", null, out var t) && t == time);
                                if (matchRow != null)
                                    matchRow[colIndex] = cellText;
                            }
                        }
                    }
                }

                dgvSemana.DataSource = dt;
                // make Hora column fixed width and read-only
                if (dgvSemana.Columns.Contains("Hora"))
                {
                    dgvSemana.Columns["Hora"].ReadOnly = true;
                    dgvSemana.Columns["Hora"].Width = 70;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar esta semana: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }

        private void MarkPendientesEstado(int estado)
        { /*
            try
            {
                var ids = new List<int>();
                foreach (DataGridViewRow r in dgvPendientes.Rows)
                {
                    if (r.Cells["Id"] == null || r.Cells["Id"].Value == null) continue;
                    ids.Add(Convert.ToInt32(r.Cells["Id"].Value));
                }

                if (ids.Count == 0) return;

                var confirm = MessageBox.Show($"Asignar estado {estado} a {ids.Count} turno(s)?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                foreach (var id in ids)
                {
                    BD.Ejecutar($"UPDATE turnos SET Estado = {estado} WHERE Id = {id}");
                }

                LoadPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al asignar estado: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }

        // Handle Delete key to cancel (clear paciente) on Pendientes tab
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
            var confirm = MessageBox.Show($"Cancelar {count} turno(s) seleccionado(s)?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                foreach (var r in rows)
                {
                    if (r.Cells["Id"] == null || r.Cells["Id"].Value == null)
                        continue;

                    int id = Convert.ToInt32(r.Cells["Id"].Value);
                    // cancel reservation: clear Paciente and PrestadorPaciente
                    string sql = $"UPDATE turnos SET Paciente = NULL, PrestadorPaciente = NULL WHERE Id = {id}";
                    BD.Ejecutar(sql);
                }

                LoadPendientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cancelar turnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } */
        }
    }
}