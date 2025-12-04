namespace TurneraMedica.Gui
{
    partial class FormCrearTurno
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código de diseño

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.comboMedicos = new System.Windows.Forms.ComboBox();
            this.comboPacientes = new System.Windows.Forms.ComboBox();
            this.comboConsultorios = new System.Windows.Forms.ComboBox();
            this.dateTurno = new System.Windows.Forms.DateTimePicker();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblMedico = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.lblConsultorio = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 10);
            this.lblTitulo.Size = new System.Drawing.Size(360, 40);
            this.lblTitulo.Text = "Crear Nuevo Turno";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMedico
            // 
            this.lblMedico.Location = new System.Drawing.Point(30, 70);
            this.lblMedico.Size = new System.Drawing.Size(100, 23);
            this.lblMedico.Text = "Médico:";
            // 
            // comboMedicos
            // 
            this.comboMedicos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMedicos.Location = new System.Drawing.Point(140, 70);
            this.comboMedicos.Size = new System.Drawing.Size(200, 23);
            // 
            // lblPaciente
            // 
            this.lblPaciente.Location = new System.Drawing.Point(30, 110);
            this.lblPaciente.Size = new System.Drawing.Size(100, 23);
            this.lblPaciente.Text = "Paciente:";
            // 
            // comboPacientes
            // 
            this.comboPacientes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPacientes.Location = new System.Drawing.Point(140, 110);
            this.comboPacientes.Size = new System.Drawing.Size(200, 23);
            // 
            // lblConsultorio
            // 
            this.lblConsultorio.Location = new System.Drawing.Point(30, 150);
            this.lblConsultorio.Size = new System.Drawing.Size(100, 23);
            this.lblConsultorio.Text = "Consultorio:";
            // 
            // comboConsultorios
            // 
            this.comboConsultorios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConsultorios.Location = new System.Drawing.Point(140, 150);
            this.comboConsultorios.Size = new System.Drawing.Size(200, 23);
            // 
            // lblFecha
            // 
            this.lblFecha.Location = new System.Drawing.Point(30, 190);
            this.lblFecha.Size = new System.Drawing.Size(100, 23);
            this.lblFecha.Text = "Fecha:";
            // 
            // dateTurno
            // 
            this.dateTurno.Location = new System.Drawing.Point(140, 190);
            this.dateTurno.Size = new System.Drawing.Size(200, 23);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(40, 240);
            this.btnCrear.Size = new System.Drawing.Size(120, 35);
            this.btnCrear.Text = "Crear Turno";
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(200, 240);
            this.btnCancelar.Size = new System.Drawing.Size(120, 35);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FormCrearTurno
            // 
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblMedico);
            this.Controls.Add(this.comboMedicos);
            this.Controls.Add(this.lblPaciente);
            this.Controls.Add(this.comboPacientes);
            this.Controls.Add(this.lblConsultorio);
            this.Controls.Add(this.comboConsultorios);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dateTurno);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Name = "FormCrearTurno";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ComboBox comboMedicos;
        private System.Windows.Forms.ComboBox comboPacientes;
        private System.Windows.Forms.ComboBox comboConsultorios;
        private System.Windows.Forms.DateTimePicker dateTurno;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblMedico;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label lblConsultorio;
        private System.Windows.Forms.Label lblFecha;
    }
}
