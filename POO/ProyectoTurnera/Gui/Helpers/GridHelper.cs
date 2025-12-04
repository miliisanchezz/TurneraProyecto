using ProyectoTurnera.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui.Helpers
{

    public static class GridHelper
    {

        public static void ConfigureIdColumn(DataGridView dgv, bool visible = true, bool allowEdit = false)
        {

            if (dgv == null)
                return;

            if (!dgv.Columns.Contains("Id"))
                return;

            var col = dgv.Columns["Id"];
            col.Visible = visible;
            col.ReadOnly = !allowEdit;
            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.Width = Math.Max(50, col.Width);
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

        }

        public static void HabilitarEdicionInline<T>(
        DataGridView dgv,
        BindingList<T> lista,
        IEntityController<T> controller,
        params string[] columnasMayusculas) where T : class, new()
        {
            var uppercaseColumns = new HashSet<string>(columnasMayusculas, StringComparer.OrdinalIgnoreCase);

            // Aseguramos que el DataSource sea la lista directamente (para ordenamiento)
            dgv.DataSource = lista;

            // Permitir agregar nuevas filas
            dgv.AllowUserToAddRows = true;

            // Eventos principales
            dgv.CellEndEdit += (s, e) => OnCellEndEdit(dgv, lista, controller, e, uppercaseColumns);
            dgv.UserAddedRow += (s, e) => OnUserAddedRow(dgv, lista, controller, e);
            dgv.RowValidating += (s, e) => OnRowValidating(dgv, lista, controller, e);
        }

        private static void OnCellEndEdit<T>(
                DataGridView dgv,
                BindingList<T> lista,
                IEntityController<T> controller,
                DataGridViewCellEventArgs e,
                HashSet<string> uppercaseColumns) where T : class, new()
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgv.Rows[e.RowIndex];
            if (row.IsNewRow) return; // Se maneja en UserAddedRow o RowValidating

            var columna = dgv.Columns[e.ColumnIndex];
            var propiedad = columna.DataPropertyName;
            if (string.IsNullOrEmpty(propiedad)) return;

            // Forzar mayúsculas si corresponde
            if (uppercaseColumns.Contains(propiedad))
            {
                var cell = row.Cells[e.ColumnIndex];
                if (cell.Value is string str && !string.IsNullOrWhiteSpace(str))
                {
                    cell.Value = str.ToUpperInvariant();
                }
            }

            if (dgv.Columns[e.ColumnIndex].Name == "Prestador")
            {
                var nombreSeleccionado = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrEmpty(nombreSeleccionado))
                {
                    var prestador = Paciente.PrestadoresDisponibles
                        .FirstOrDefault(p => p.Nombre == nombreSeleccionado);

                    var paciente = dgv.Rows[e.RowIndex].DataBoundItem as Paciente;
                    if (paciente != null)
                    {
                        paciente.Prestador = prestador; // ← se actualiza el objeto completo
                    }
                }
            }

            // Actualizar entidad existente
            var entidad = row.DataBoundItem as T;
            if (entidad == null) return;

            var idProp = entidad.GetType().GetProperty("Id");
            if (idProp == null) return;

            var idValue = idProp.GetValue(entidad);
            int id = idValue is int i ? i : Convert.ToInt32(idValue);

            if (id > 0)
            {
                try
                {
                    controller.Actualizar(entidad);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dgv.CancelEdit();
                }
            }
            // Si id <= 0 → es nuevo → se crea en RowValidating o UserAddedRow
        }

        private static void OnUserAddedRow<T>(
            DataGridView dgv,
            BindingList<T> lista,
            IEntityController<T> controller,
            DataGridViewRowEventArgs e) where T : class, new()
        {
            var entidad = e.Row.DataBoundItem as T;
            if (entidad == null) return;

            try
            {
                int nuevoId = controller.Crear(entidad);
                var idProp = entidad.GetType().GetProperty("Id");
                if (idProp != null && idProp.CanWrite)
                {
                    idProp.SetValue(entidad, nuevoId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lista.Remove(entidad);
            }
        }

        private static void OnRowValidating<T>(
            DataGridView dgv,
            BindingList<T> lista,
            IEntityController<T> controller,
            DataGridViewCellCancelEventArgs e) where T : class, new()
        {
            if (dgv.IsCurrentRowDirty && !dgv.Rows[e.RowIndex].IsNewRow)
            {
                var entidad = dgv.Rows[e.RowIndex].DataBoundItem as T;
                if (entidad == null) return;

                var idProp = entidad.GetType().GetProperty("Id");
                if (idProp == null) return;

                var idValue = idProp.GetValue(entidad);
                int id = idValue is int i ? i : Convert.ToInt32(idValue);

                if (id <= 0)
                {
                    // Es una fila nueva que se completó manualmente (sin pasar por UserAddedRow)
                    try
                    {
                        int nuevoId = controller.Crear(entidad);
                        if (idProp.CanWrite)
                            idProp.SetValue(entidad, nuevoId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                    }
                }
            }
        }

        public static void HabilitarEliminacionConConfirmacion<T>(
            DataGridView dgv,
            BindingList<T> lista,
            Action<int> accionEliminar) where T : class
        {
            dgv.UserDeletingRow += (s, e) =>
            {
                try
                {
                    // 1) Obtener el objeto completo de la fila
                    object rowObj = e.Row.DataBoundItem;
                    if (rowObj == null)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // 2) Castear a T (compatible con C# 7.3)
                    T entidad = rowObj as T;
                    if (entidad == null)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // 3) Obtener la propiedad Id por reflexión (funciona en todas las versiones)
                    PropertyInfo propId = typeof(T).GetProperty("Id");
                    if (propId == null)
                    {
                        MessageBox.Show("La entidad no tiene propiedad Id", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;
                    }

                    // 4) Leer el valor del Id
                    object idObj = propId.GetValue(entidad);
                    if (!(idObj is int id) || id <= 0)
                    {
                        MessageBox.Show("El registro no tiene Id válido o es nuevo.", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                        return;
                    }

                    accionEliminar(id);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            };
        }

        public static void ConfigurarColumnasConsultorios(DataGridView gv)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 150
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Direccion",
                HeaderText = "Dirección",
                DataPropertyName = "Direccion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NumeroConsultorio",
                HeaderText = "Nº Consultorio",
                DataPropertyName = "NumeroConsultorio",
                Width = 100
            });
        }

        public static void ConfigurarColumnasPrestadores(DataGridView gv)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60,
                ReadOnly = true
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        public static void ConfigurarColumnasEspecialidades(DataGridView gv)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60,
                ReadOnly = true
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

        }

        public static void ConfigurarColumnasAdministradores(DataGridView gv)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 150
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Apellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI",
                HeaderText = "DNI",
                DataPropertyName = "DNI",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Password",
                HeaderText = "Password",
                DataPropertyName = "Password",
                Width = 100
            });

        }

        public static void ConfigurarColumnasPacientes(DataGridView gv, List<Prestador> prestadores)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 150
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Apellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI",
                HeaderText = "DNI",
                DataPropertyName = "DNI",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Password",
                HeaderText = "Password",
                DataPropertyName = "Password",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Telefono",
                HeaderText = "Telefono",
                DataPropertyName = "Telefono",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Prestador",
                HeaderText = "Prestador",
                DataPropertyName = "PrestadorId",    // ← BINDEA AL ID!
                DisplayMember = "Nombre",
                ValueMember = "Id",                  // ← Compara por Id
                Width = 150,
                FlatStyle = FlatStyle.Flat
            });

        }

        public static void ConfigurarColumnasMedicos(DataGridView gv)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 150
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Apellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DNI",
                HeaderText = "DNI",
                DataPropertyName = "DNI",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Password",
                HeaderText = "Password",
                DataPropertyName = "Password",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Matricula",
                HeaderText = "Metricula",
                DataPropertyName = "Matricula",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Prestador",
                HeaderText = "Prestador",
                DataPropertyName = "PrestadorNombre", // o lo que uses para mostrar
                DisplayMember = "Nombre",
                ValueMember = "Nombre",
                Width = 100,
                FlatStyle = FlatStyle.Flat
            });

            gv.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "Espcialidad",
                HeaderText = "Especialidad",
                DataPropertyName = "EspecialidadNombre", // o lo que uses para mostrar
                DisplayMember = "Nombre",
                ValueMember = "Nombre",
                Width = 100,
                FlatStyle = FlatStyle.Flat
            });

        }

        public static void ConfigurarColumnasTurnos(DataGridView gv)
        {
            gv.AutoGenerateColumns = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 60
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fecha",
                HeaderText = "Fecha",
                DataPropertyName = "Fecha",
                Width = 150
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Medico",
                HeaderText = "Medico",
                DataPropertyName = "Medico",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Especialidad",
                HeaderText = "Especialidad",
                DataPropertyName = "Especialidad",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Consultorio",
                HeaderText = "COnsultorio",
                DataPropertyName = "Consultorio",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Matricula",
                HeaderText = "Metricula",
                DataPropertyName = "Matricula",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrestadorMedico",
                HeaderText = "PrestadorMedico",
                DataPropertyName = "PrestadorMedico",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioConsulta",
                HeaderText = "PrecioConsulta",
                DataPropertyName = "PrecioConsulta",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Paciente",
                HeaderText = "Paciente",
                DataPropertyName = "Paciente",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrestadorPaciente",
                HeaderText = "PrestadorPaciente",
                DataPropertyName = "PrestadorPciente",
                Width = 100
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Estado",
                HeaderText = "Estado",
                DataPropertyName = "Estado",
                Width = 100
            });

        }

        public static void AttachCrudHandlers(DataGridView dgv, string tableName, Action reloadAction)
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

        public static void HabilitarComboConActualizacion<T>(
            DataGridView dgv,
            string nombreColumna,
            List<T> listaOpciones)
        {
            if (dgv.Columns[nombreColumna] is DataGridViewComboBoxColumn col)
            {
                col.DataSource = listaOpciones;
                col.ValueMember = "Id";
                col.DisplayMember = "Nombre";
                col.ReadOnly = false;
            }
        }

    }

}

