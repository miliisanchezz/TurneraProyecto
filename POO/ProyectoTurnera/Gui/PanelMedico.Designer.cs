namespace ProyectoTurnera.Gui
{
    partial class PanelMedico
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
            this.lblIdMedico = new System.Windows.Forms.Label();
            this.Paciente = new System.Windows.Forms.Label();
            this.verMedico = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.verMedico)).BeginInit();
            this.SuspendLayout();
            // 
            // lblIdMedico
            // 
            this.lblIdMedico.AutoSize = true;
            this.lblIdMedico.Location = new System.Drawing.Point(599, 162);
            this.lblIdMedico.Name = "lblIdMedico";
            this.lblIdMedico.Size = new System.Drawing.Size(0, 13);
            this.lblIdMedico.TabIndex = 39;
            // 
            // Paciente
            // 
            this.Paciente.AutoSize = true;
            this.Paciente.Font = new System.Drawing.Font("Sitka Heading", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paciente.Location = new System.Drawing.Point(30, 27);
            this.Paciente.Name = "Paciente";
            this.Paciente.Size = new System.Drawing.Size(248, 35);
            this.Paciente.TabIndex = 38;
            this.Paciente.Text = "Ver turnos disponibles:";
            // 
            // verMedico
            // 
            this.verMedico.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.verMedico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.verMedico.Location = new System.Drawing.Point(44, 99);
            this.verMedico.Name = "verMedico";
            this.verMedico.Size = new System.Drawing.Size(503, 324);
            this.verMedico.TabIndex = 36;
            this.verMedico.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.verTurnos_CellContentClick);
            // 
            // PanelMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblIdMedico);
            this.Controls.Add(this.Paciente);
            this.Controls.Add(this.verMedico);
            this.Name = "PanelMedico";
            this.Text = "PanelMedico";
            this.Load += new System.EventHandler(this.PanelMedico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.verMedico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdMedico;
        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.DataGridView verMedico;
    }
}