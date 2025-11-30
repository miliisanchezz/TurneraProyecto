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
    public partial class AgregarTurno : Form
    {
        public AgregarTurno()
        {
            InitializeComponent();
        }

        private void buttonRegistrar_Click(object sender, EventArgs e)
        {

        }

        private void buttonReservar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "Desea agregar el turno? ",
                "Confirmación",
                MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
            {
                MessageBox.Show("Tu turno a sido agregado");
            }
            
        }

        private void comboEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboEspecialidad.Items.Add("Pediatria");
            comboEspecialidad.Items.Add("Cardiologo");
            comboEspecialidad.Items.Add("Clínico");
            comboEspecialidad.Items.Add("Dermatologia");
            comboEspecialidad.Items.Add("Traumatologia");
            comboEspecialidad.Items.Add("Ginecologia");
        }
    }
}
