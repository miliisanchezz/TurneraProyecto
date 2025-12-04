using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurneraMedica.Gui
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void btnPaciente_Click(object sender, EventArgs e)
        {
            new FormLoginPaciente().Show();
            this.Hide();
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            new FormLoginMedico().Show();
            this.Hide();
        }
    }
}
