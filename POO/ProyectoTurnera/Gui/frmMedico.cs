using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public class frmMedico : Form
    {
        public frmMedico()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Médico";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // frmMedico
            // 
            this.ClientSize = new System.Drawing.Size(759, 440);
            this.Name = "frmMedico";
            this.ResumeLayout(false);

        }
    }
}