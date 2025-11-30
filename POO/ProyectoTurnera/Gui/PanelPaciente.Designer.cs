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
            ((System.ComponentModel.ISupportInitialize)(this.verTurnos)).BeginInit();
            this.SuspendLayout();
            // 
            // verTurnos
            // 
            this.verTurnos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.verTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.verTurnos.Location = new System.Drawing.Point(26, 81);
            this.verTurnos.Name = "verTurnos";
            this.verTurnos.Size = new System.Drawing.Size(405, 162);
            this.verTurnos.TabIndex = 0;
            this.verTurnos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.verTurnos_CellContentClick);
            // 
            // buttonAgregarTurno
            // 
            this.buttonAgregarTurno.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgregarTurno.Location = new System.Drawing.Point(313, 350);
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
            this.lblIdPaciente.Location = new System.Drawing.Point(581, 144);
            this.lblIdPaciente.Name = "lblIdPaciente";
            this.lblIdPaciente.Size = new System.Drawing.Size(0, 13);
            this.lblIdPaciente.TabIndex = 35;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(516, 81);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 36;
            // 
            // PanelPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}