namespace TurneraMedica.Gui
{
    partial class FormPanelPaciente
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblBienvenida = new System.Windows.Forms.Label();
            this.btnVerTurnos = new System.Windows.Forms.Button();
            this.btnSacarTurno = new System.Windows.Forms.Button();
            this.dgvTurnos = new System.Windows.Forms.DataGridView();
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBienvenida
            // 
            this.lblBienvenida.AutoSize = true;
            this.lblBienvenida.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblBienvenida.Location = new System.Drawing.Point(25, 20);
            this.lblBienvenida.Name = "lblBienvenida";
            this.lblBienvenida.Size = new System.Drawing.Size(120, 32);
            this.lblBienvenida.TabIndex = 0;
            this.lblBienvenida.Text = "Bienvenido";
            // 
            // btnVerTurnos
            // 
            this.btnVerTurnos.Location = new System.Drawing.Point(25, 70);
            this.btnVerTurnos.Name = "btnVerTurnos";
            this.btnVerTurnos.Size = new System.Drawing.Size(120, 27);
            this.btnVerTurnos.TabIndex = 1;
            this.btnVerTurnos.Text = "Mis Turnos";
            this.btnVerTurnos.UseVisualStyleBackColor = true;
            this.btnVerTurnos.Click += new System.EventHandler(this.btnVerTurnos_Click);
            // 
            // btnSacarTurno
            // 
            this.btnSacarTurno.Location = new System.Drawing.Point(170, 70);
            this.btnSacarTurno.Name = "btnSacarTurno";
            this.btnSacarTurno.Size = new System.Drawing.Size(120, 27);
            this.btnSacarTurno.TabIndex = 2;
            this.btnSacarTurno.Text = "Sacar Turno";
            this.btnSacarTurno.UseVisualStyleBackColor = true;
            this.btnSacarTurno.Click += new System.EventHandler(this.btnSacarTurno_Click);
            // 
            // dgvTurnos
            // 
            this.dgvTurnos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTurnos.Location = new System.Drawing.Point(25, 120);
            this.dgvTurnos.Name = "dgvTurnos";
            this.dgvTurnos.RowHeadersWidth = 51;
            this.dgvTurnos.RowTemplate.Height = 29;
            this.dgvTurnos.Size = new System.Drawing.Size(600, 250);
            this.dgvTurnos.TabIndex = 3;
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Location = new System.Drawing.Point(505, 390);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(120, 27);
            this.btnCerrarSesion.TabIndex = 4;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = true;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // FormPanelPaciente
            // 
            this.ClientSize = new System.Drawing.Size(650, 450);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.dgvTurnos);
            this.Controls.Add(this.btnSacarTurno);
            this.Controls.Add(this.btnVerTurnos);
            this.Controls.Add(this.lblBienvenida);
            this.Name = "FormPanelPaciente";
            this.Text = "Panel del Paciente";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTurnos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBienvenida;
        private System.Windows.Forms.Button btnVerTurnos;
        private System.Windows.Forms.Button btnSacarTurno;
        private System.Windows.Forms.DataGridView dgvTurnos;
        private System.Windows.Forms.Button btnCerrarSesion;
    }
}
