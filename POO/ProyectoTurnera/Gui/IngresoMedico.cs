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
    public partial class IngresoMedico : Form
    {
        public IngresoMedico()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboEspecialidad.Items.Add("Pediatria");
            comboEspecialidad.Items.Add("Cardiologo");
            comboEspecialidad.Items.Add("Clínico");
            comboEspecialidad.Items.Add("Dermatologia");
            comboEspecialidad.Items.Add("Traumatologia");
            comboEspecialidad.Items.Add("Ginecologia");
        }

        private void IngresoMedico_Load(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Medicos (Nombre, Apellido, DNI, Especialidad, Matricula, PrecioConsulta, ObraSocial) " +"VALUES ('" + textNombre.Text + "', '" + textApellido.Text + "', '" + textDni.Text + "', '" + comboEspecialidad.Text + "', '" + textMatricula.Text + "', '" + textPrecio.Text + "', '" + comboObra.Text + "')";

            BD.Ejecutar(sql);

            MessageBox.Show("Médico registrado correctamente");

            PanelMedico f = new PanelMedico();
            f.Show();
            this.Hide();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboObra.Items.Add("OSDE");
            comboObra.Items.Add("PAMI");
            comboObra.Items.Add("IOMA");
            comboObra.Items.Add("Galeno");
            comboObra.Items.Add("Particular");
        }
    }
}
