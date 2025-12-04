using System;

namespace TurneraMedica.Gui
{
    partial class FormRegistroPaciente
    {
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.TextBox txtDni;
        private System.Windows.Forms.TextBox txtObraSocial;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.Button btnVolver;

        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.txtDni = new System.Windows.Forms.TextBox();
            this.txtObraSocial = new System.Windows.Forms.TextBox();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // txtUsuario
            this.txtUsuario.Text = "Usuario";
            this.txtUsuario.Location = new System.Drawing.Point(40, 20);
            this.txtUsuario.Size = new System.Drawing.Size(250, 27);

            // txtPassword
            this.txtPassword.Text = "Contraseña";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Location = new System.Drawing.Point(40, 60);
            this.txtPassword.Size = new System.Drawing.Size(250, 27);

            // txtNombre
            this.txtNombre.Text = "Nombre";
            this.txtNombre.Location = new System.Drawing.Point(40, 100);
            this.txtNombre.Size = new System.Drawing.Size(250, 27);

            // txtApellido
            this.txtApellido.Text = "Apellido";
            this.txtApellido.Location = new System.Drawing.Point(40, 140);
            this.txtApellido.Size = new System.Drawing.Size(250, 27);

            // txtDdni
            this.txtDni.Text = "DNI";
            this.txtDni.Location = new System.Drawing.Point(40, 180);
            this.txtDni.Size = new System.Drawing.Size(250, 27);

            // txtObraSocial
            this.txtObraSocial.Text = "Obra Social";
            this.txtObraSocial.Location = new System.Drawing.Point(40, 220);
            this.txtObraSocial.Size = new System.Drawing.Size(250, 27);

            // btnRegistrar
            this.btnRegistrar.Text = "Registrar Paciente";
            this.btnRegistrar.Location = new System.Drawing.Point(40, 270);
            this.btnRegistrar.Size = new System.Drawing.Size(250, 40);
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);

            // btnVolver
            this.btnVolver.Text = "Volver";
            this.btnVolver.Location = new System.Drawing.Point(40, 320);
            this.btnVolver.Size = new System.Drawing.Size(250, 35);
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);

            // FormRegistroPaciente
            this.ClientSize = new System.Drawing.Size(340, 380);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.txtDni);
            this.Controls.Add(this.txtObraSocial);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.btnVolver);

            this.Text = "Registrar Paciente";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
