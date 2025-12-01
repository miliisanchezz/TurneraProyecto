namespace ProyectoTurnera.Gui
{
    partial class Ingresar_como
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPaciente = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMedico = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPaciente
            // 
            this.btnPaciente.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPaciente.Font = new System.Drawing.Font("Sitka Heading", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaciente.Location = new System.Drawing.Point(522, 248);
            this.btnPaciente.Name = "btnPaciente";
            this.btnPaciente.Size = new System.Drawing.Size(123, 55);
            this.btnPaciente.TabIndex = 13;
            this.btnPaciente.Text = "Ingresar como Paciente";
            this.btnPaciente.UseVisualStyleBackColor = false;
            this.btnPaciente.Click += new System.EventHandler(this.btnPaciente_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(332, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "Bienvenido a...";
            // 
            // btnMedico
            // 
            this.btnMedico.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnMedico.Font = new System.Drawing.Font("Sitka Heading", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedico.Location = new System.Drawing.Point(153, 248);
            this.btnMedico.Name = "btnMedico";
            this.btnMedico.Size = new System.Drawing.Size(123, 55);
            this.btnMedico.TabIndex = 14;
            this.btnMedico.Text = "Ingresar como Medico";
            this.btnMedico.UseVisualStyleBackColor = false;
            this.btnMedico.Click += new System.EventHandler(this.btnMedico_Click);
            // 
            // Ingresar_como
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnMedico);
            this.Controls.Add(this.btnPaciente);
            this.Controls.Add(this.label1);
            this.Name = "Ingresar_como";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Ingresar_como_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPaciente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMedico;
    }
}