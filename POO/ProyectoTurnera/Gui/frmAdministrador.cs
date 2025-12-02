using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public class frmAdministrador : Form
    {
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


        // Columns that must be forced to uppercase
        private static readonly HashSet<string> UppercaseColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Nombre",
            "Apellido",
            "Direccion"
        };

        public frmAdministrador()
        {
            InitializeComponents();
            LoadPrestadores();
            LoadPacientes();
            LoadMedicos();
            LoadEspecialidades();
            LoadConsultorios();
            //LoadTurnos();
            LoadAdministradores();
        }

        private void InitializeComponents()
        {
            Text = "Administrador";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;

            tabControl1 = new TabControl
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
            AttachCrudHandlers(dgvEspecialidades, "especialidades", LoadEspecialidades);
            AttachCrudHandlers(dgvPrestadores, "prestadores", LoadPrestadores);
            AttachCrudHandlers(dgvPacientes, "pacientes", LoadPacientes);
            AttachCrudHandlers(dgvMedicos, "medicos", LoadMedicos);
            AttachCrudHandlers(dgvConsultorios, "consultorios", LoadConsultorios);
            AttachCrudHandlers(dgvAdministradores, "administradores", LoadAdministradores);

            SetupTurnosTab();

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
                        if (string.Equals(colNameEdit, "DNI", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colNameEdit, "Matricula", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(colNameEdit, "NumeroConsultorio", StringComparison.OrdinalIgnoreCase))
                        {
                            tb.KeyPress += NumericKeyPress;
                        }
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

            // cuando termina la edición de una celda -> UPDATE columna específica
            dgv.CellEndEdit += (s, e) =>
            {
                try
                {
                    if (e.RowIndex < 0 || e.ColumnIndex < 0)
                        return;

                    var grid = (DataGridView)s;

                    // Guard against stale row indices (can happen if the grid was reloaded mid-event)
                    if (e.RowIndex >= grid.Rows.Count)
                        return;

                    var row = grid.Rows[e.RowIndex];

                    // Ensure uppercase for Nombre/Apellido columns right after editing
                    var column = grid.Columns[e.ColumnIndex];
                    var colNameForCase = column.DataPropertyName;
                    if (string.IsNullOrWhiteSpace(colNameForCase))
                        colNameForCase = column.Name;
                    if (UppercaseColumns.Contains(colNameForCase))
                    {
                        var cell = row.Cells[e.ColumnIndex];
                        if (cell.Value != null)
                        {
                            var upper = cell.Value.ToString().ToUpperInvariant();
                            if (!string.Equals(cell.Value.ToString(), upper, StringComparison.Ordinal))
                            {
                                // Update the cell value; this keeps data consistent before update/insert
                                cell.Value = upper;
                            }
                        }
                    }

                    // Buscar columna 'Id' (clave primaria)
                    if (!grid.Columns.Contains("Id"))
                        return;

                    var idObj = row.Cells["Id"].Value;
                    if (idObj == null || idObj == DBNull.Value || string.IsNullOrWhiteSpace(idObj.ToString()))
                    {
                        // fila nueva sin Id: no actualizar aquí (se insertará en RowValidated)
                        return;
                    }

                    var id = idObj.ToString();
                    var col = column;
                    var colName = col.DataPropertyName;
                    if (string.IsNullOrWhiteSpace(colName))
                        colName = col.Name;

                    if (string.Equals(colName, "Id", StringComparison.OrdinalIgnoreCase))
                        return;

                    var cellValue = row.Cells[e.ColumnIndex].Value;
                    string sqlValue = cellValue == null || cellValue == DBNull.Value ? "NULL" : $"'{EscapeSql(cellValue.ToString())}'";

                    string sql = $"UPDATE {tableName} SET {colName} = {sqlValue} WHERE Id = {id}";
                    BD.Ejecutar(sql);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // cuando se valida la fila (se completó una nueva fila) -> INSERT si no tiene Id
            dgv.RowValidated += (s, e) =>
            {
                try
                {
                    var grid = (DataGridView)s;
                    if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count)
                        return;

                    var row = grid.Rows[e.RowIndex];

                    // Ignorar la fila nueva en edición (IsNewRow)
                    if (row.IsNewRow)
                        return;

                    // Si hay columna Id y está vacía -> INSERT
                    if (grid.Columns.Contains("Id"))
                    {
                        var idObj = row.Cells["Id"].Value;
                        if (idObj == null || idObj == DBNull.Value || string.IsNullOrWhiteSpace(idObj.ToString()))
                        {
                            // Construir columnas/valores para INSERT (excluir Id)
                            var cols = "";
                            var vals = "";
                            foreach (DataGridViewColumn col in grid.Columns)
                            {
                                var colName = col.DataPropertyName;
                                if (string.IsNullOrWhiteSpace(colName))
                                    colName = col.Name;

                                if (string.Equals(colName, "Id", StringComparison.OrdinalIgnoreCase))
                                    continue;

                                // Omitir columnas virtuales
                                if (col.Visible == false)
                                    continue;

                                // Guard against invalid column index
                                if (col.Index < 0 || col.Index >= grid.Columns.Count)
                                    continue;

                                var cell = row.Cells[col.Index];
                                var valueObj = cell.Value;

                                // Force uppercase on configured columns prior to building INSERT
                                if (valueObj != null && UppercaseColumns.Contains(colName))
                                {
                                    var upper = valueObj.ToString().ToUpperInvariant();
                                    if (!string.Equals(valueObj.ToString(), upper, StringComparison.Ordinal))
                                    {
                                        cell.Value = upper;
                                        valueObj = upper;
                                    }
                                }

                                var value = valueObj;
                                if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                                {
                                    // omitir columnas vacías en INSERT para permitir valores por defecto en BD
                                    continue;
                                }

                                if (!string.IsNullOrEmpty(cols))
                                {
                                    cols += ", ";
                                    vals += ", ";
                                }

                                cols += colName;
                                vals += $"'{EscapeSql(value.ToString())}'";
                            }

                            if (!string.IsNullOrWhiteSpace(cols))
                            {
                                string sql = $"INSERT INTO {tableName} ({cols}) VALUES ({vals})";
                                BD.Ejecutar(sql);

                                // recargar para obtener el Id asignado y sincronizar la grilla
                                // defer reload to avoid modifying the grid while DataGridView is processing events
                                grid.BeginInvoke((Action)(() => reloadAction?.Invoke()));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // cuando se borra una fila desde la grilla -> DELETE
            dgv.UserDeletingRow += (s, e) =>
            {
                try
                {
                    var grid = (DataGridView)s;
                    var row = e.Row;
                    if (!grid.Columns.Contains("Id"))
                        return;

                    var idObj = row.Cells["Id"].Value;
                    if (idObj == null || idObj == DBNull.Value || string.IsNullOrWhiteSpace(idObj.ToString()))
                    {
                        // fila sin Id, nada que borrar en BD
                        return;
                    }

                    var id = idObj.ToString();

                    var confirm = MessageBox.Show("¿Eliminar registro seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }

                    string sql = $"DELETE FROM {tableName} WHERE Id = {id}";
                    BD.Ejecutar(sql);

                    // defer reload to avoid modifying the grid while DataGridView is processing events
                    grid.BeginInvoke((Action)(() => reloadAction?.Invoke()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            };
        }

        private string EscapeSql(string value)
        {
            return value?.Replace("'", "''") ?? "";
        }

        /// <summary>
        /// Asegura que la columna "Id" sea visible y tenga propiedades adecuadas tras bindear el DataTable.
        /// Por seguridad, la columna Id se expone pero queda en modo ReadOnly (no editable por el usuario).
        /// </summary>
        private void ConfigureIdColumn(DataGridView dgv, bool visible = true, bool allowEdit = false)
        {
            if (dgv == null)
                return;

            // Si el DataSource es un DataTable o BindingSource, después del bind se crean las columnas automáticas.
            // Asegurarnos de que la columna exista y ajustar propiedades.
            if (!dgv.Columns.Contains("Id"))
                return;

            var col = dgv.Columns["Id"];
            col.Visible = visible;
            col.ReadOnly = !allowEdit; // por defecto no editable (Id generalmente es PK/autoincrement)
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.Width = Math.Max(50, col.Width);
            // evitar que la columna se oculte por error por AutoSize si no se necesita
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }

        private void LoadEspecialidades()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre FROM especialidades";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null)
                {
                    dgvEspecialidades.DataSource = null;
                    return;
                }

                dgvEspecialidades.DataSource = dt;

                // Asegurar que la columna Id sea visible (pero no editable)
                ConfigureIdColumn(dgvEspecialidades, visible: true, allowEdit: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar especialidades: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadPrestadores()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre FROM prestadores";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null)
                {
                    dgvPrestadores.DataSource = null;
                    return;
                }

                // Force Nombre/Apellido columns in the returned table to uppercase (if present)
                UppercaseColumns.Intersect(dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
                    .ToList()
                    .ForEach(col => {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[col] != DBNull.Value && r[col] != null)
                                r[col] = r[col].ToString().ToUpperInvariant();
                        }
                    });

                dgvPrestadores.DataSource = dt;

                // Mostrar Id para referencia, mantenerlo ReadOnly
                ConfigureIdColumn(dgvPrestadores, visible: true, allowEdit: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar prestadores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPacientes()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre, Apellido, DNI, Password, Prestador, Telefono FROM pacientes";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null)
                {
                    dgvPacientes.DataSource = null;
                    return;
                }

                // Force Nombre/Apellido uppercase in loaded data
                UppercaseColumns.Intersect(dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
                    .ToList()
                    .ForEach(col => {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[col] != DBNull.Value && r[col] != null)
                                r[col] = r[col].ToString().ToUpperInvariant();
                        }
                    });

                dgvPacientes.DataSource = dt;

                SetupPrestadorCombo(dgvPacientes);


                // Mostrar Id para referencia, mantenerlo ReadOnly
                ConfigureIdColumn(dgvPacientes, visible: true, allowEdit: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pacientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMedicos()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre, Apellido, DNI, Password, Especialidad, Matricula, Prestador, PrecioConsulta FROM medicos";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null)
                {
                    dgvMedicos.DataSource = null;
                    return;
                }

                // Force Nombre/Apellido uppercase in loaded data
                UppercaseColumns.Intersect(dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
                    .ToList()
                    .ForEach(col => {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[col] != DBNull.Value && r[col] != null)
                                r[col] = r[col].ToString().ToUpperInvariant();
                        }
                    });

                dgvMedicos.DataSource = dt;

                SetupEspecialidadCombo(dgvMedicos);
                SetupPrestadorCombo(dgvMedicos);

                // Mostrar Id para referencia, mantenerlo ReadOnly
                ConfigureIdColumn(dgvMedicos, visible: true, allowEdit: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar pacientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadConsultorios()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre, Direccion, NumeroConsultorio FROM consultorios";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null)
                {
                    dgvConsultorios.DataSource = null;
                    return;
                }

                // Force Nombre/Direccion uppercase in loaded data
                UppercaseColumns.Intersect(dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
                    .ToList()
                    .ForEach(col => {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[col] != DBNull.Value && r[col] != null)
                                r[col] = r[col].ToString().ToUpperInvariant();
                        }
                    });

                dgvConsultorios.DataSource = dt;

                // Mostrar Id para referencia, mantenerlo ReadOnly
                ConfigureIdColumn(dgvConsultorios, visible: true, allowEdit: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar consultorios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAdministradores()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre, Apellido, DNI, Password FROM administradores";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null)
                {
                    dgvAdministradores.DataSource = null;
                    return;
                }

                // Force Nombre/Apellido uppercase in loaded data
                UppercaseColumns.Intersect(dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName))
                    .ToList()
                    .ForEach(col => {
                        foreach (DataRow r in dt.Rows)
                        {
                            if (r[col] != DBNull.Value && r[col] != null)
                                r[col] = r[col].ToString().ToUpperInvariant();
                        }
                    });

                dgvAdministradores.DataSource = dt;

                // Mostrar Id para referencia, mantenerlo ReadOnly
                ConfigureIdColumn(dgvPacientes, visible: true, allowEdit: false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar administradores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add this helper method inside the frmAdministrador class (for example, place it just after ConfigureIdColumn):
        private void SetupPrestadorCombo(DataGridView grid)
        {
            try
            {
                // Load prestadores (Id, Nombre)
                DataTable dtPrestadores = BD.Consultar("SELECT Id, Nombre FROM prestadores");
                if (dtPrestadores == null)
                    return;

                // Find existing Prestador column and record its index
                int insertIndex = -1;
                var existing = grid.Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => string.Equals(
                        string.IsNullOrWhiteSpace(c.DataPropertyName) ? c.Name : c.DataPropertyName,
                        "Prestador", StringComparison.OrdinalIgnoreCase));

                if (existing != null)
                {
                    insertIndex = existing.Index;
                    grid.Columns.Remove(existing);
                }

                // Create combo column bound to the numeric Id but showing Nombre
                var combo = new DataGridViewComboBoxColumn
                {
                    Name = "Prestador",
                    HeaderText = "Prestador",
                    DataPropertyName = "Prestador", // binds to the Id value in the data source
                    DataSource = dtPrestadores,
                    DisplayMember = "Nombre",
                    ValueMember = "Id",
                    FlatStyle = FlatStyle.Flat,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };
                
                // Insert at the original column index if known; otherwise append
                if (insertIndex >= 0 && insertIndex <= grid.Columns.Count)
                    grid.Columns.Insert(insertIndex, combo);
                else
                    grid.Columns.Add(combo);
            }
            catch (Exception)
            {
                // noncritical for UI; keep original column if something fails
            }
        }

        private void SetupEspecialidadCombo(DataGridView grid)
        {
            try
            {
                // Load prestadores (Id, Nombre)
                DataTable dtEspecialidades = BD.Consultar("SELECT Id, Nombre FROM especialidades");
                if (dtEspecialidades == null)
                    return;

                // Find existing Prestador column and record its index
                int insertIndex = -1;
                var existing = grid.Columns.Cast<DataGridViewColumn>()
                    .FirstOrDefault(c => string.Equals(
                        string.IsNullOrWhiteSpace(c.DataPropertyName) ? c.Name : c.DataPropertyName,
                        "Especialidad", StringComparison.OrdinalIgnoreCase));

                if (existing != null)
                {
                    insertIndex = existing.Index;
                    grid.Columns.Remove(existing);
                }

                // Create combo column bound to the numeric Id but showing Nombre
                var combo = new DataGridViewComboBoxColumn
                {
                    Name = "Especialidad",
                    HeaderText = "Especialidad",
                    DataPropertyName = "Especialidad", // binds to the Id value in the data source
                    DataSource = dtEspecialidades,
                    DisplayMember = "Nombre",
                    ValueMember = "Id",
                    FlatStyle = FlatStyle.Flat,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton
                };

                // Insert at the original column index if known; otherwise append
                if (insertIndex >= 0 && insertIndex <= grid.Columns.Count)
                    grid.Columns.Insert(insertIndex, combo);
                else
                    grid.Columns.Add(combo);
            }
            catch (Exception)
            {
                // noncritical for UI; keep original column if something fails
            }
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

        private void LoadMedicosForTurnos()
        {
            try
            {
                DataTable dt = BD.Consultar("SELECT Id, CONCAT(Nombre, ' ', Apellido) AS DisplayName FROM medicos");
                if (dt == null)
                    return;
                cbMedicoTurnos.DataSource = dt;
                cbMedicoTurnos.ValueMember = "Id";
                cbMedicoTurnos.DisplayMember = "DisplayName";
            }
            catch { }
        }

        private void LoadConsultoriosForTurnos()
        {
            try
            {
                DataTable dt = BD.Consultar("SELECT Id, CONCAT(consultorios.Nombre, ' ', consultorios.Direccion, ' No. ', CAST(consultorios.NumeroConsultorio AS CHAR)) AS Nombre FROM consultorios");
                if (dt == null)
                    return;
                cbConsultorioTurnos.DataSource = dt;
                cbConsultorioTurnos.ValueMember = "Id";
                cbConsultorioTurnos.DisplayMember = "Nombre";
            }
            catch { }
        }

        private void LoadTurnos()
        {
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
                DataTable dt = BD.Consultar(sql);
                dgvTurnos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar turnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateTurnos()
        {
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
                DataTable dtMed = BD.Consultar($"SELECT Especialidad, PrecioConsulta, Prestador FROM medicos WHERE Id = {medicoId} LIMIT 1");
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
                    BD.Ejecutar(ins);
                }

                MessageBox.Show($"Generados {inserts.Count} turnos.", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload grid
                LoadTurnos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar turnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}