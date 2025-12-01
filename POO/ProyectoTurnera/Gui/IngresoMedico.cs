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
            
        }

        private void IngresoMedico_Load(object sender, EventArgs e)
        {
            try
            {
               
                string sql = "SELECT Id AS Id, Nombre FROM especialidad";
                DataTable dt = BD.Consultar(sql);

                // Si no existe filas, dejar vacío
                if (dt == null || dt.Rows.Count == 0)
                {
                    comboEspecialidad.DataSource = null;
                    return;
                }

                comboEspecialidad.DisplayMember = "Nombre"; // lo que se muestra
                comboEspecialidad.ValueMember = "Id";       // el valor asociado
                comboEspecialidad.DataSource = dt;
                comboEspecialidad.SelectedIndex = -1; // sin selección inicial
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los medicos: " + ex.Message);
            }

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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {
            var especialidadIdObj = comboEspecialidad.SelectedValue;
            int especialidadId = Convert.ToInt32(especialidadIdObj);

            var obraIdObj = comboObra.SelectedValue;
            int obraId = Convert.ToInt32(obraIdObj);


            string sql = "INSERT INTO Medicos (Nombre, Apellido, DNI, Especialidad, Matricula, PrecioConsulta, ObraSocial) " +"VALUES ('" + textNombre.Text + "', '" + textApellido.Text + "', '" + textDni.Text + "', '" + especialidadId + "', '" + textMatricula.Text + "', '" + textPrecio.Text + "', '" + obraId + "')";

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
            if (comboObra.DataSource == null || comboObra.SelectedIndex < 0)
                return;

            var selectedId = comboObra.SelectedValue; // corresponde a la columna 'Id'
            var selectedName = comboObra.Text;         // corresponde a la columna 'Nombre' / DisplayMember

        
        }

        private void comboEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboEspecialidad.DataSource == null || comboEspecialidad.SelectedIndex < 0)
                return;

            var selectedId= comboEspecialidad.SelectedValue; // corresponde a la columna 'Id'
            var selectedName = comboEspecialidad.Text;         // corresponde a la columna 'Nombre' / DisplayMember

        }

        

        private void Paciente_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void buttonAtras_Click(object sender, EventArgs e)
        {
            this.Hide();
            Ingresar_como volver = new Ingresar_como();
            volver.Show();
        }
    }
}
