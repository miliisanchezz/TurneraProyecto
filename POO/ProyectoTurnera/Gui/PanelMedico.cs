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
    public partial class PanelMedico : Form
    {
        public PanelMedico()
        {
            InitializeComponent();
        }

        private void PanelMedico_Load(object sender, EventArgs e)
        {
            string sql = "SELECT Fecha, Hora, P.Nombre AS Paciente, C.Nombre AS Consultorio " +
                 "FROM Turnos T " +
                 "JOIN Pacientes P ON T.IdPaciente = P.IdPaciente " +
                 "JOIN Consultorios C ON T.IdConsultorio = C.IdConsultorio " +
                 "WHERE T.IdMedico = " + lblIdMedico.Text;

            verMedico.DataSource = BD.Consultar(sql);
        }

        private void verTurnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
