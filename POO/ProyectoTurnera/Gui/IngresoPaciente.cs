using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProyectoTurnera.Gui
{
    public partial class IngresoPaciente : Form
    {
        public IngresoPaciente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {

            var obraIdObj = comboObra.SelectedValue;
            int obraId = Convert.ToInt32(obraIdObj);

            string sql = "INSERT INTO Pacientes (Nombre, Apellido, DNI, ObraSocial) " +
                 "VALUES ('" + textNombre.Text + "', '" + textApellido.Text + "', '" + textDni.Text + "', " + obraId + ")";

            BD.Ejecutar(sql);

            if (string.IsNullOrWhiteSpace(textNombre.Text) || string.IsNullOrWhiteSpace(textApellido.Text) || string.IsNullOrWhiteSpace(textDni.Text) || string.IsNullOrWhiteSpace(comboObra.Text))
            {
                MessageBox.Show("Los campos no pueden estar vacíos");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show("Paciente registrado con éxito. ¿Desea registrar otro paciente?", "Registro Exitoso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                   
                    textNombre.Text = "";
                    textApellido.Text = "";
                    textDni.Text = "";
                    comboObra.SelectedIndex = -1;
                }
                else
                {
                    
                    this.Close();
                }

                PanelPaciente f = new PanelPaciente();
                f.Show();
                this.Hide();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboObra_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IngresoPaciente_Load(object sender, EventArgs e)
        {
            try
            {
                // Ajusta los nombres de columnas si en tu BD son diferentes (por ejemplo IdPrestador)
                string sql = "SELECT Id AS Id, Nombre FROM prestador";
                DataTable dt = BD.Consultar(sql);

                // Si no existe filas, dejar vacío
                if (dt == null || dt.Rows.Count == 0)
                {
                    comboObra.DataSource = null;
                    return;
                }

                comboObra.DisplayMember = "Nombre"; // lo que se muestra
                comboObra.ValueMember = "Id";       // el valor asociado
                comboObra.DataSource = dt;
                comboObra.SelectedIndex = -1; // sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los pacientes: " + ex.Message);
            }
        }

        private void comboObra_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Ejemplo de cómo obtener el Id y el Nombre seleccionados
            if (comboObra.DataSource == null || comboObra.SelectedIndex < 0)
                return;

            var selectedId = comboObra.SelectedValue; // corresponde a la columna 'Id'
            var selectedName = comboObra.Text;         // corresponde a la columna 'Nombre' / DisplayMember

            }

        private void buttonAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ingresar_como volver = new Ingresar_como();
            volver.Show();
        }
    }

}