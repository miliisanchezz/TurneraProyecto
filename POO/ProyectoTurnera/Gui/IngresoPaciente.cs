using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string sql = "INSERT INTO Pacientes (Nombre, Apellido, DNI, ObraSocial) " + "VALUES ('" + textNombre.Text + "', '" + textApellido.Text + "', '" + textDni.Text + "', '" + comboObra.Text + "')";

            BD.Ejecutar(sql);

            if (string.IsNullOrWhiteSpace(textNombre.Text) ||string.IsNullOrWhiteSpace(textApellido.Text) || string.IsNullOrWhiteSpace(textDni.Text) || string.IsNullOrWhiteSpace(comboObra.Text))
                {
                MessageBox.Show("Los campos no pueden estar vacíos");
                return;
            }
            else
            {
                MessageBox.Show("Paciente registrado correctamente");
            }
   
            PanelPaciente f = new PanelPaciente();
            f.Show();
            this.Hide();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void IngresoPaciente_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void comboObra_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboObra.Items.Add("OSDE");
            comboObra.Items.Add("PAMI");
            comboObra.Items.Add("IOMA");
            comboObra.Items.Add("Galeno");
            comboObra.Items.Add("Particular");
        }
    }
}
