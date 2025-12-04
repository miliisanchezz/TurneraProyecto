using System;

namespace TurneraMedica.Gui
{
    partial class FormRegistroUsuario
    {
        private System.Windows.Forms.Button btnMedico;
        private System.Windows.Forms.Button btnPaciente;
        private System.Windows.Forms.Button btnVolver;

        private void InitializeComponent()
        {
            this.btnMedico = new System.Windows.Forms.Button();
            this.btnPaciente = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMedico
            // 
            this.btnMedico.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMedico.Location = new System.Drawing.Point(50, 40);
            this.btnMedico.Name = "btnMedico";
            this.btnMedico.Size = new System.Drawing.Size(250, 50);
            this.btnMedico.TabIndex = 0;
            this.btnMedico.Text = "Registrar Médico";
            this.btnMedico.Click += new System.EventHandler(this.btnMedico_Click);
            // 
            // btnPaciente
            // 
            this.btnPaciente.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnPaciente.Location = new System.Drawing.Point(50, 110);
            this.btnPaciente.Name = "btnPaciente";
            this.btnPaciente.Size = new System.Drawing.Size(250, 50);
            this.btnPaciente.TabIndex = 1;
            this.btnPaciente.Text = "Registrar Paciente";
            this.btnPaciente.Click += new System.EventHandler(this.btnPaciente_Click);
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(50, 170);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(250, 40);
            this.btnVolver.TabIndex = 2;
            this.btnVolver.Text = "Volver";
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // FormRegistroUsuario
            // 
            this.ClientSize = new System.Drawing.Size(350, 250);
            this.Controls.Add(this.btnMedico);
            this.Controls.Add(this.btnPaciente);
            this.Controls.Add(this.btnVolver);
            this.Name = "FormRegistroUsuario";
            this.Text = "Registrar Usuario";
            this.Load += new System.EventHandler(this.FormRegistroUsuario_Load);
            this.ResumeLayout(false);

        }
    }
}
