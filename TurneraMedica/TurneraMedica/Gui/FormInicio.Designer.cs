namespace TurneraMedica.Gui
{
    partial class FormInicio
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnMedico;
        private System.Windows.Forms.Button btnPaciente;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnMedico = new System.Windows.Forms.Button();
            this.btnPaciente = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // btnMedico
            this.btnMedico.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMedico.Location = new System.Drawing.Point(50, 40);
            this.btnMedico.Size = new System.Drawing.Size(250, 50);
            this.btnMedico.Text = "Ingresar como Médico";
            this.btnMedico.Click += new System.EventHandler(this.btnMedico_Click);

            // btnPaciente
            this.btnPaciente.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnPaciente.Location = new System.Drawing.Point(50, 110);
            this.btnPaciente.Size = new System.Drawing.Size(250, 50);
            this.btnPaciente.Text = "Ingresar como Paciente";
            this.btnPaciente.Click += new System.EventHandler(this.btnPaciente_Click);

            // FormInicio
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.ClientSize = new System.Drawing.Size(350, 200);
            this.Controls.Add(this.btnMedico);
            this.Controls.Add(this.btnPaciente);
            this.Text = "FormInicio";
            this.ResumeLayout(false);
        }
    }
}
