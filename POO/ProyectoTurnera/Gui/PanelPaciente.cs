using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public partial class PanelPaciente : Form
    {
        public PanelPaciente()
        {
            InitializeComponent();
        }

        private void PanelPaciente_Load(object sender, EventArgs e)
        {
            string sql = "SELECT T.IdTurno, M.Nombre AS Medico, M.Especialidad, T.Fecha, T.Hora " + "FROM Turnos T " + "JOIN Medicos M ON T.IdMedico = M.IdMedico " + "WHERE T.IdPaciente IS NULL";

            verTurnos.DataSource = BD.Consultar(sql);
        }

        private void verTurnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAgregarTurno_Click(object sender, EventArgs e)
        {
            if (verTurnos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccioná un turno");
                return;
            }

            string idTurno = verTurnos.SelectedRows[0].Cells["IdTurno"].Value.ToString();

            string sql = "UPDATE Turnos SET IdPaciente = '" + lblIdPaciente.Text +
                         "' WHERE IdTurno = " + idTurno;

            BD.Ejecutar(sql);

            MessageBox.Show("Turno reservado");

            PanelPaciente_Load(null, null);
        }
    }
}
