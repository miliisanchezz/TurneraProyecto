namespace ProyectoTurnera.Gui
{
    partial class AgregarTurno
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
            this.comboHora = new System.Windows.Forms.ComboBox();
            this.comboMedico = new System.Windows.Forms.ComboBox();
            this.comboEspecialidad = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Paciente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonReservar = new System.Windows.Forms.Button();
            this.lblIdPaciente = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboHora
            // 
            this.comboHora.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.comboHora.FormattingEnabled = true;
            this.comboHora.Location = new System.Drawing.Point(447, 195);
            this.comboHora.Name = "comboHora";
            this.comboHora.Size = new System.Drawing.Size(214, 21);
            this.comboHora.TabIndex = 0;
            // 
            // comboMedico
            // 
            this.comboMedico.FormattingEnabled = true;
            this.comboMedico.Location = new System.Drawing.Point(447, 302);
            this.comboMedico.Name = "comboMedico";
            this.comboMedico.Size = new System.Drawing.Size(214, 21);
            this.comboMedico.TabIndex = 2;
            // 
            // comboEspecialidad
            // 
            this.comboEspecialidad.FormattingEnabled = true;
            this.comboEspecialidad.Location = new System.Drawing.Point(447, 247);
            this.comboEspecialidad.Name = "comboEspecialidad";
            this.comboEspecialidad.Size = new System.Drawing.Size(214, 21);
            this.comboEspecialidad.TabIndex = 3;
            this.comboEspecialidad.SelectedIndexChanged += new System.EventHandler(this.comboEspecialidades_SelectedIndexChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(447, 147);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(214, 20);
            this.dateTimePicker1.TabIndex = 33;
            // 
            // Paciente
            // 
            this.Paciente.AutoSize = true;
            this.Paciente.Font = new System.Drawing.Font("Sitka Heading", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paciente.Location = new System.Drawing.Point(12, 9);
            this.Paciente.Name = "Paciente";
            this.Paciente.Size = new System.Drawing.Size(196, 35);
            this.Paciente.TabIndex = 35;
            this.Paciente.Text = "Agregar un turno:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 28);
            this.label1.TabIndex = 36;
            this.label1.Text = "Complete los siguientes campos";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(218, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 28);
            this.label5.TabIndex = 37;
            this.label5.Text = "Fecha";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(218, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 28);
            this.label2.TabIndex = 38;
            this.label2.Text = "Horario";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(220, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(198, 28);
            this.label3.TabIndex = 39;
            this.label3.Text = "Especialidades médicas";
            // 
            // buttonReservar
            // 
            this.buttonReservar.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReservar.Location = new System.Drawing.Point(346, 382);
            this.buttonReservar.Name = "buttonReservar";
            this.buttonReservar.Size = new System.Drawing.Size(104, 46);
            this.buttonReservar.TabIndex = 40;
            this.buttonReservar.Text = "Reservar";
            this.buttonReservar.UseVisualStyleBackColor = true;
            this.buttonReservar.Click += new System.EventHandler(this.buttonReservar_Click);
            // 
            // lblIdPaciente
            // 
            this.lblIdPaciente.AutoSize = true;
            this.lblIdPaciente.Location = new System.Drawing.Point(788, 9);
            this.lblIdPaciente.Name = "lblIdPaciente";
            this.lblIdPaciente.Size = new System.Drawing.Size(0, 13);
            this.lblIdPaciente.TabIndex = 41;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(220, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 28);
            this.label4.TabIndex = 42;
            this.label4.Text = "Medico";
            // 
            // AgregarTurno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblIdPaciente);
            this.Controls.Add(this.buttonReservar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Paciente);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.comboEspecialidad);
            this.Controls.Add(this.comboMedico);
            this.Controls.Add(this.comboHora);
            this.Name = "AgregarTurno";
            this.Text = "AgregarTurno";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboHora;
        private System.Windows.Forms.ComboBox comboMedico;
        private System.Windows.Forms.ComboBox comboEspecialidad;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonReservar;
        private System.Windows.Forms.Label lblIdPaciente;
        private System.Windows.Forms.Label label4;
    }
}