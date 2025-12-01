using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public class frmPaciente : Form
    {
        private DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn hasTypeDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn scalarDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn objDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn arrayDataGridViewTextBoxColumn;
        private BindingSource argsBindingSource;
        private System.ComponentModel.IContainer components;
        private BindingSource stmtExecuteBindingSource;
        private DataGridView dataGridView1;

        public frmPaciente()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Paciente";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.stmtExecuteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.argsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hasTypeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.scalarDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmtExecuteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.argsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeDataGridViewTextBoxColumn,
            this.hasTypeDataGridViewCheckBoxColumn,
            this.scalarDataGridViewTextBoxColumn,
            this.objDataGridViewTextBoxColumn,
            this.arrayDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.argsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(198, 145);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // stmtExecuteBindingSource
            // 
            this.stmtExecuteBindingSource.DataSource = typeof(Mysqlx.Sql.StmtExecute);
            // 
            // argsBindingSource
            // 
            this.argsBindingSource.DataMember = "Args";
            this.argsBindingSource.DataSource = this.stmtExecuteBindingSource;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            // 
            // hasTypeDataGridViewCheckBoxColumn
            // 
            this.hasTypeDataGridViewCheckBoxColumn.DataPropertyName = "HasType";
            this.hasTypeDataGridViewCheckBoxColumn.HeaderText = "HasType";
            this.hasTypeDataGridViewCheckBoxColumn.Name = "hasTypeDataGridViewCheckBoxColumn";
            this.hasTypeDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // scalarDataGridViewTextBoxColumn
            // 
            this.scalarDataGridViewTextBoxColumn.DataPropertyName = "Scalar";
            this.scalarDataGridViewTextBoxColumn.HeaderText = "Scalar";
            this.scalarDataGridViewTextBoxColumn.Name = "scalarDataGridViewTextBoxColumn";
            // 
            // objDataGridViewTextBoxColumn
            // 
            this.objDataGridViewTextBoxColumn.DataPropertyName = "Obj";
            this.objDataGridViewTextBoxColumn.HeaderText = "Obj";
            this.objDataGridViewTextBoxColumn.Name = "objDataGridViewTextBoxColumn";
            // 
            // arrayDataGridViewTextBoxColumn
            // 
            this.arrayDataGridViewTextBoxColumn.DataPropertyName = "Array";
            this.arrayDataGridViewTextBoxColumn.HeaderText = "Array";
            this.arrayDataGridViewTextBoxColumn.Name = "arrayDataGridViewTextBoxColumn";
            // 
            // frmPaciente
            // 
            this.ClientSize = new System.Drawing.Size(712, 386);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPaciente";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stmtExecuteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.argsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}