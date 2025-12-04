namespace TurneraMedica.Gui
{
    partial class FormTurnos
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
            this.listaTurnos = new System.Windows.Forms.ListBox();
            this.btnCrear = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 10);
            this.lblTitulo.Size = new System.Drawing.Size(360, 40);
            this.lblTitulo.Text = "Turnos";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listaTurnos
            // 
            this.listaTurnos.Location = new System.Drawing.Point(30, 70);
            this.listaTurnos.Size = new System.Drawing.Size(320, 180);
            // 
            // btnCrear
            // 
            this.btnCrear.Location = new System.Drawing.Point(30, 270);
            this.btnCrear.Size = new System.Drawing.Size(130, 35);
            this.btnCrear.Text = "Crear Turno";
            this.btnCrear.Click += new System.EventHandler(this.btnCrear_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(220, 270);
            this.btnEliminar.Size = new System.Drawing.Size(130, 35);
            this.btnEliminar.Text = "Eliminar Turno";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // FormTurnos
            // 
            this.ClientSize = new System.Drawing.Size(380, 330);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.listaTurnos);
            this.Controls.Add(this.btnCrear);
            this.Controls.Add(this.btnEliminar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormTurnos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.ListBox listaTurnos;
        private System.Windows.Forms.Button btnCrear;
        private System.Windows.Forms.Button btnEliminar;
    }
}
