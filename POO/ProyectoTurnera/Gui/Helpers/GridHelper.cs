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

                    // 5) Confirmación
                    DialogResult resultado = MessageBox.Show(
                        $"¿Eliminar este registro?\n\n{entidad}",
                        "Confirmar eliminación",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }

                    // 6) Ejecutar la eliminación (usando el Controller que pasaste)
                    accionEliminar(id);

                    MessageBox.Show("Registro eliminado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public static void ConfigurarColumnasPacientes(DataGridView gv)
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
                DataPropertyName = "PrestadorNombre", // o lo que uses para mostrar
                DisplayMember = "Nombre",
                ValueMember = "Nombre",
                Width = 100,
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

        public static void HabilitarComboConActualizacion(
            DataGridView dgv,
            string nombreColumna,
            List<object> listaOpciones,
            Action<object, object> actualizarEntidad)  // ← ESTE ES EL TRUCO
        {
            // 1. Convertir la columna en ComboBox
            if (dgv.Columns.Contains(nombreColumna))
            {
                var col = (DataGridViewComboBoxColumn) dgv.Columns[nombreColumna];
                col.DataSource = listaOpciones;
            }

            // 2. Al terminar de editar → actualizamos el objeto
            dgv.CellEndEdit += (s, e) =>
            {
                if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
                if (dgv.Columns[e.ColumnIndex].Name != nombreColumna) return;

                var valor = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (string.IsNullOrEmpty(valor)) return;

                var entidad = dgv.Rows[e.RowIndex].DataBoundItem;
                if (entidad == null) return;

                // Buscamos el objeto completo por el nombre
                var seleccionado = listaOpciones
                    .FirstOrDefault(x => x.GetType().GetProperty("Nombre")?.GetValue(x)?.ToString() == valor);

                // ¡AQUÍ USAMOS LA ACCIÓN QUE NOS PASASTE!
                actualizarEntidad(entidad, seleccionado);
            };
        }
        

    }

}

