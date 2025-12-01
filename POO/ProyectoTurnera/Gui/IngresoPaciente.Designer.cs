namespace ProyectoTurnera.Gui
{
    partial class IngresoPaciente
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
            this.Paciente = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.textApellido = new System.Windows.Forms.TextBox();
            this.textDni = new System.Windows.Forms.TextBox();
            this.buttonRegistrar = new System.Windows.Forms.Button();
            this.comboObra = new System.Windows.Forms.ComboBox();
            this.buttonAtras = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Paciente
            // 
            this.Paciente.AutoSize = true;
            this.Paciente.Font = new System.Drawing.Font("Sitka Heading", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paciente.Location = new System.Drawing.Point(12, 9);
            this.Paciente.Name = "Paciente";
            this.Paciente.Size = new System.Drawing.Size(98, 35);
            this.Paciente.TabIndex = 0;
            this.Paciente.Text = "Paciente";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(228, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Apellido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(228, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 28);
            this.label3.TabIndex = 3;
            this.label3.Text = "Dni";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(228, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 28);
            this.label5.TabIndex = 4;
            this.label5.Text = "Obra social";
            // 
            // textNombre
            // 
            this.textNombre.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textNombre.Location = new System.Drawing.Point(375, 97);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(209, 20);
            this.textNombre.TabIndex = 5;
            // 
            // textApellido
            // 
            this.textApellido.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textApellido.Location = new System.Drawing.Point(375, 155);
            this.textApellido.Name = "textApellido";
            this.textApellido.Size = new System.Drawing.Size(209, 20);
            this.textApellido.TabIndex = 10;
            // 
            // textDni
            // 
            this.textDni.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textDni.Location = new System.Drawing.Point(375, 210);
            this.textDni.Name = "textDni";
            this.textDni.Size = new System.Drawing.Size(209, 20);
            this.textDni.TabIndex = 11;
            // 
            // buttonRegistrar
            // 
            this.buttonRegistrar.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegistrar.Location = new System.Drawing.Point(340, 326);
            this.buttonRegistrar.Name = "buttonRegistrar";
            this.buttonRegistrar.Size = new System.Drawing.Size(104, 46);
            this.buttonRegistrar.TabIndex = 32;
            this.buttonRegistrar.Text = "Registrar";
            this.buttonRegistrar.UseVisualStyleBackColor = true;
            this.buttonRegistrar.Click += new System.EventHandler(this.buttonRegistrar_Click);
            // 
            // comboObra
            // 
            this.comboObra.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.comboObra.FormattingEnabled = true;
            this.comboObra.Location = new System.Drawing.Point(375, 268);
            this.comboObra.Name = "comboObra";
            this.comboObra.Size = new System.Drawing.Size(209, 21);
            this.comboObra.TabIndex = 36;
            this.comboObra.SelectedIndexChanged += new System.EventHandler(this.comboObra_SelectedIndexChanged_1);
            // 
            // buttonAtras
            // 
            this.buttonAtras.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAtras.Location = new System.Drawing.Point(340, 378);
            this.buttonAtras.Name = "buttonAtras";
            this.buttonAtras.Size = new System.Drawing.Size(104, 46);
            this.buttonAtras.TabIndex = 39;
            this.buttonAtras.Text = "Atrás";
            this.buttonAtras.UseVisualStyleBackColor = true;
            this.buttonAtras.Click += new System.EventHandler(this.buttonAtras_Click);
            // 
            // IngresoPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonAtras);
            this.Controls.Add(this.comboObra);
            this.Controls.Add(this.buttonRegistrar);
            this.Controls.Add(this.textDni);
            this.Controls.Add(this.textApellido);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Paciente);
            this.Name = "IngresoPaciente";
            this.Text = "RegistroMedico";
            this.Load += new System.EventHandler(this.IngresoPaciente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.TextBox textApellido;
        private System.Windows.Forms.TextBox textDni;
        private System.Windows.Forms.Button buttonRegistrar;
        private System.Windows.Forms.ComboBox comboObra;
        private System.Windows.Forms.Button buttonAtras;
    }
}