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
        private TabPage tabReportes;

        private DataGridView dgvPrestadores;
        private DataGridView dgvPacientes;
        private DataGridView dgvMedicos;
        private DataGridView dgvEspecialidades;
        private DataGridView dgvConsultorios;
        private DataGridView dgvAdministradores;
        private DataGridView dgvReportes;

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
                        // enable password char only for Password column
                        tb.UseSystemPasswordChar = string.Equals(colNameEdit, "Password", StringComparison.OrdinalIgnoreCase);
                    }
                }
                catch
                {
                    // ignore
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
                string sql = "SELECT Id AS Id, Nombre, Direccion FROM consultorios";
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


    }
}