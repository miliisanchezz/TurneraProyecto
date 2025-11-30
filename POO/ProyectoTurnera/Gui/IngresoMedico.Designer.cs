namespace ProyectoTurnera.Gui
{
    partial class IngresoMedico
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
            this.textDni = new System.Windows.Forms.TextBox();
            this.textApellido = new System.Windows.Forms.TextBox();
            this.textNombre = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Paciente = new System.Windows.Forms.Label();
            this.textPrecio = new System.Windows.Forms.TextBox();
            this.textMatricula = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboObra = new System.Windows.Forms.ComboBox();
            this.buttonRegistrar = new System.Windows.Forms.Button();
            this.comboEspecialidad = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // textDni
            // 
            this.textDni.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textDni.Location = new System.Drawing.Point(403, 135);
            this.textDni.Name = "textDni";
            this.textDni.Size = new System.Drawing.Size(209, 20);
            this.textDni.TabIndex = 21;
            this.textDni.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textApellido
            // 
            this.textApellido.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textApellido.Location = new System.Drawing.Point(403, 93);
            this.textApellido.Name = "textApellido";
            this.textApellido.Size = new System.Drawing.Size(209, 20);
            this.textApellido.TabIndex = 20;
            // 
            // textNombre
            // 
            this.textNombre.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textNombre.Location = new System.Drawing.Point(403, 52);
            this.textNombre.Name = "textNombre";
            this.textNombre.Size = new System.Drawing.Size(209, 20);
            this.textNombre.TabIndex = 18;
            this.textNombre.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(206, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 28);
            this.label5.TabIndex = 17;
            this.label5.Text = "Especialidad";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(206, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 28);
            this.label3.TabIndex = 16;
            this.label3.Text = "Dni";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(206, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 28);
            this.label2.TabIndex = 15;
            this.label2.Text = "Apellido";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(206, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 28);
            this.label1.TabIndex = 14;
            this.label1.Text = "Nombre";
            // 
            // Paciente
            // 
            this.Paciente.AutoSize = true;
            this.Paciente.Font = new System.Drawing.Font("Sitka Heading", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paciente.Location = new System.Drawing.Point(13, 11);
            this.Paciente.Name = "Paciente";
            this.Paciente.Size = new System.Drawing.Size(98, 35);
            this.Paciente.TabIndex = 13;
            this.Paciente.Text = "Paciente";
            // 
            // textPrecio
            // 
            this.textPrecio.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textPrecio.Location = new System.Drawing.Point(403, 261);
            this.textPrecio.Name = "textPrecio";
            this.textPrecio.Size = new System.Drawing.Size(209, 20);
            this.textPrecio.TabIndex = 27;
            // 
            // textMatricula
            // 
            this.textMatricula.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.textMatricula.Location = new System.Drawing.Point(403, 219);
            this.textMatricula.Name = "textMatricula";
            this.textMatricula.Size = new System.Drawing.Size(209, 20);
            this.textMatricula.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(206, 296);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 28);
            this.label4.TabIndex = 25;
            this.label4.Text = "Obra social";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(206, 253);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 28);
            this.label6.TabIndex = 24;
            this.label6.Text = "Precio de la consulta";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(206, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 28);
            this.label7.TabIndex = 23;
            this.label7.Text = "Matricula";
            // 
            // comboObra
            // 
            this.comboObra.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.comboObra.FormattingEnabled = true;
            this.comboObra.Location = new System.Drawing.Point(403, 304);
            this.comboObra.Name = "comboObra";
            this.comboObra.Size = new System.Drawing.Size(209, 21);
            this.comboObra.TabIndex = 30;
            this.comboObra.SelectedIndexChanged += new System.EventHandler(this.comboObra_SelectedIndexChanged);
            // 
            // buttonRegistrar
            // 
            this.buttonRegistrar.Font = new System.Drawing.Font("Sitka Heading", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRegistrar.Location = new System.Drawing.Point(350, 352);
            this.buttonRegistrar.Name = "buttonRegistrar";
            this.buttonRegistrar.Size = new System.Drawing.Size(104, 46);
            this.buttonRegistrar.TabIndex = 31;
            this.buttonRegistrar.Text = "Registrar";
            this.buttonRegistrar.UseVisualStyleBackColor = true;
            this.buttonRegistrar.Click += new System.EventHandler(this.buttonRegistrar_Click);
            // 
            // comboEspecialidad
            // 
            this.comboEspecialidad.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.comboEspecialidad.FormattingEnabled = true;
            this.comboEspecialidad.Location = new System.Drawing.Point(403, 177);
            this.comboEspecialidad.Name = "comboEspecialidad";
            this.comboEspecialidad.Size = new System.Drawing.Size(209, 21);
            this.comboEspecialidad.TabIndex = 32;
            this.comboEspecialidad.SelectedIndexChanged += new System.EventHandler(this.comboEspecialidad_SelectedIndexChanged);
            // 
            // IngresoMedico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.comboEspecialidad);
            this.Controls.Add(this.buttonRegistrar);
            this.Controls.Add(this.comboObra);
            this.Controls.Add(this.textPrecio);
            this.Controls.Add(this.textMatricula);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textDni);
            this.Controls.Add(this.textApellido);
            this.Controls.Add(this.textNombre);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Paciente);
            this.Name = "IngresoMedico";
            this.Text = "IngresoMedico";
            this.Load += new System.EventHandler(this.IngresoMedico_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textDni;
        private System.Windows.Forms.TextBox textApellido;
        private System.Windows.Forms.TextBox textNombre;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Paciente;
        private System.Windows.Forms.TextBox textPrecio;
        private System.Windows.Forms.TextBox textMatricula;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboObra;
        private System.Windows.Forms.Button buttonRegistrar;
        private System.Windows.Forms.ComboBox comboEspecialidad;
    }
}