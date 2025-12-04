using System;

namespace TurneraMedica.Gui
{
    partial class FormLoginMedico
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Button btnVolver;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnVolver = new System.Windows.Forms.Button();

            this.SuspendLayout();

            // txtUsuario
            this.txtUsuario.Location = new System.Drawing.Point(40, 30);
            this.txtUsuario.Size = new System.Drawing.Size(250, 27);
            this.txtUsuario.Text = "Usuario";

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(40, 70);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(250, 27);
            this.txtPassword.Text = "Contraseña";

            // btnIngresar
            this.btnIngresar.Text = "Ingresar";
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnIngresar.Location = new System.Drawing.Point(40, 120);
            this.btnIngresar.Size = new System.Drawing.Size(250, 40);
            this.btnIngresar.Click += new EventHandler(this.btnIngresar_Click);

            // btnVolver
            this.btnVolver.Text = "Volver";
            this.btnVolver.Location = new System.Drawing.Point(40, 170);
            this.btnVolver.Size = new System.Drawing.Size(250, 40);
            this.btnVolver.Click += new EventHandler(this.btnVolver_Click);

            // FormLoginMedico
            this.ClientSize = new System.Drawing.Size(340, 240);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnIngresar);
            this.Controls.Add(this.btnVolver);
            this.Text = "FormLoginMedico";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
