using System;
using System.Data;
using System.Drawing;
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
        private TabPage tabTarifas;
        private TabPage tabAdministradores;
        private TabPage tabReportes;

        private DataGridView dgvPrestadores;
        private DataGridView dgvPacientes;
        private DataGridView dgvMedicos;
        private DataGridView dgvEspecialidades;
        private DataGridView dgvTarifas;
        private DataGridView dgvAdministradores;
        private DataGridView dgvReportes;

        public frmAdministrador()
        {
            InitializeComponents();
            LoadEspecialidades();
        }

        private void InitializeComponents()
        {
            Text = "Administrador";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;

            // TabControl que ocupa el 100% del formulario
            tabControl1 = new TabControl
            {
                Dock = DockStyle.Fill
            };

            // Crear pestañas
            tabPrestadores = new TabPage("Prestadores");
            tabPacientes = new TabPage("Pacientes");
            tabMedicos = new TabPage("Médicos");
            tabEspecialidades = new TabPage("Especialidades");
            tabTarifas = new TabPage("Tarifas");
            tabAdministradores = new TabPage("Administradores");
            tabReportes = new TabPage("Reportes");

            // Crear grillas y hacer que ocupen 100% de cada tab
            dgvPrestadores = CreateDefaultGrid("dgvPrestadores");
            dgvPacientes = CreateDefaultGrid("dgvPacientes");
            dgvMedicos = CreateDefaultGrid("dgvMedicos");
            dgvEspecialidades = CreateDefaultGrid("dgvEspecialidades");
            dgvTarifas = CreateDefaultGrid("dgvTarifas");
            dgvAdministradores = CreateDefaultGrid("dgvAdministradores");
            dgvReportes = CreateDefaultGrid("dgvReportes");

            // Agregar las grillas a las pestañas correspondientes
            tabPrestadores.Controls.Add(dgvPrestadores);
            tabPacientes.Controls.Add(dgvPacientes);
            tabMedicos.Controls.Add(dgvMedicos);
            tabEspecialidades.Controls.Add(dgvEspecialidades); // aquí embebemos la grilla de especialidades
            tabTarifas.Controls.Add(dgvTarifas);
            tabAdministradores.Controls.Add(dgvAdministradores);
            tabReportes.Controls.Add(dgvReportes);

            // Agregar pestañas al tabcontrol
            tabControl1.TabPages.Add(tabPrestadores);
            tabControl1.TabPages.Add(tabPacientes);
            tabControl1.TabPages.Add(tabMedicos);
            tabControl1.TabPages.Add(tabEspecialidades);
            tabControl1.TabPages.Add(tabTarifas);
            tabControl1.TabPages.Add(tabAdministradores);
            tabControl1.TabPages.Add(tabReportes);

            Controls.Add(tabControl1);
        }

        private DataGridView CreateDefaultGrid(string name)
        {
            var dgv = new DataGridView
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoGenerateColumns = true
            };
            return dgv;
        }

        private void LoadEspecialidades()
        {
            try
            {
                string sql = "SELECT Id AS Id, Nombre FROM especialidad";
                DataTable dt = BD.Consultar(sql);

                // Si la consulta retorna null o vacía, dejar la grilla vacía
                if (dt == null || dt.Rows.Count == 0)
                {
                    dgvEspecialidades.DataSource = null;
                    return;
                }

                dgvEspecialidades.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar especialidades: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}