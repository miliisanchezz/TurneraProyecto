using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui.Helpers
{

    public static class GridHelper
    {
        /// <summary>
        /// Handler genérico de eliminación para C# 7.3 o inferior
        /// </summary>
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

    }

}

