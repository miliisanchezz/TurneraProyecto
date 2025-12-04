namespace TurneraMedica.Gui
{
    partial class FormObrasSociales
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador

        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.listaObras = new System.Windows.Forms.ListBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Size = new System.Drawing.Size(360, 40);
            this.lblTitulo.Text = "Obras Sociales";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listaObras
            // 
            this.listaObras.Location = new System.Drawing.Point(30, 70);
            this.listaObras.Size = new System.Drawing.Size(320, 150);
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(30, 240);
            this.lblNombre.Text = "Nombre nuevo:";
            this.lblNombre.Size = new System.Drawing.Size(140, 23);
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(150, 240);
            this.txtNombre.Size = new System.Drawing.Size(200, 23);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(30, 280);
            this.btnAgregar.Size = new System.Drawing.Size(130, 35);
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(220, 280);
            this.btnEliminar.Size = new System.Drawing.Size(130, 35);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // FormObrasSociales
            // 
            this.ClientSize = new System.Drawing.Size(380, 340);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.listaObras);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnEliminar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormObrasSociales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ListBox listaObras;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
