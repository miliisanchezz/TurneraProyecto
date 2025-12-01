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
    public partial class Ingresar_como : Form
    {
        public Ingresar_como()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            IngresoPaciente form = new IngresoPaciente();
            form.Show();
            this.Hide();
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            IngresoMedico form = new IngresoMedico();
            form.Show();
            this.Hide();
        }

        private void Ingresar_como_Load(object sender, EventArgs e)
        {

        }
    }
}
