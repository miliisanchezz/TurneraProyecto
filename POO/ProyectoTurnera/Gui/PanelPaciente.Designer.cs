namespace ProyectoTurnera.Gui
{
    partial class PanelPaciente
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
            this.verTurnos = new System.Windows.Forms.DataGridView();
            this.buttonAgregarTurno = new System.Windows.Forms.Button();
            this.Paciente = new System.Windows.Forms.Label();
            this.lblIdPaciente = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Especialidad = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.verTurnos)).BeginInit();
            this.SuspendLayout();
            // 
            // verTurnos
            // 
            this.verTurnos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.verTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.verTurnos.Location = new System.Drawing.Point(18, 198);
            this.verTurnos.Name = "verTurnos";
            this.verTurnos.Size = new System.Drawing.Size(405, 162);
            this.verTurnos.TabIndex = 0;
            this.verTurnos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.verTurnos_CellContentClick);
            // 
            // buttonAgregarTurno
            // 
            this.buttonAgregarTurno.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregarTurno.Location = new System.Drawing.Point(317, 392);
            this.buttonAgregarTurno.Name = "buttonAgregarTurno";
            this.buttonAgregarTurno.Size = new System.Drawing.Size(169, 46);
            this.buttonAgregarTurno.TabIndex = 33;
            this.buttonAgregarTurno.Text = "Solicitar turno";
            this.buttonAgregarTurno.UseVisualStyleBackColor = true;
            this.buttonAgregarTurno.Click += new System.EventHandler(this.buttonAgregarTurno_Click);
            // 
            // Paciente
            // 
            this.Paciente.AutoSize = true;
            this.Paciente.Font = new System.Drawing.Font("Sitka Heading", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paciente.Location = new System.Drawing.Point(12, 9);
            this.Paciente.Name = "Paciente";
            this.Paciente.Size = new System.Drawing.Size(213, 35);
            this.Paciente.TabIndex = 34;
            this.Paciente.Text = "Turnos disponibles:";
            // 
            // lblIdPaciente
            // 
            this.lblIdPaciente.AutoSize = true;
            this.lblIdPaciente.Location = new System.Drawing.Point(581, 133);
            this.lblIdPaciente.Name = "lblIdPaciente";
            this.lblIdPaciente.Size = new System.Drawing.Size(0, 13);
            this.lblIdPaciente.TabIndex = 35;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(505, 198);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 36;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 84);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(405, 21);
            this.comboBox1.TabIndex = 37;
            // 
            // Especialidad
            // 
            this.Especialidad.AutoSize = true;
            this.Especialidad.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Especialidad.Location = new System.Drawing.Point(13, 53);
            this.Especialidad.Name = "Especialidad";
            this.Especialidad.Size = new System.Drawing.Size(111, 28);
            this.Especialidad.TabIndex = 38;
            this.Especialidad.Text = "Especialidad";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 28);
            this.label1.TabIndex = 40;
            this.label1.Text = "Medico";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(18, 145);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(405, 21);
            this.comboBox2.TabIndex = 39;
            // 
            // PanelPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.Especialidad);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.lblIdPaciente);
            this.Controls.Add(this.Paciente);
            this.Controls.Add(this.buttonAgregarTurno);
            this.Controls.Add(this.verTurnos);
            this.Name = "PanelPaciente";
            this.Text = "PanelPaciente";
            this.Load += new System.EventHandler(this.PanelPaciente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.verTurnos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView verTurnos;
        private System.Windows.Forms.Button buttonAgregarTurno;
        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.Label lblIdPaciente;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label Especialidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}