using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public class MainForm : Form
    {
        private TabControl tabControl;
        private TabPage tabAdministrativo;
        private TabPage tabPaciente;
        private TabPage tabMedico;
        private Label lblBienvenida;

        private readonly string _rol;
        private readonly int _idUsuario;

        public MainForm(string rol, int idUsuario)
        {
            _rol = rol;
            _idUsuario = idUsuario;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Turnera - Principal";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(800, 600);

            lblBienvenida = new Label
            {
                Text = $"Bienvenido (UsuarioId={_idUsuario}) Rol: {_rol}",
                AutoSize = true,
                Left = 12,
                Top = 12
            };

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Top = 40
            };

            tabAdministrativo = new TabPage("Administrativo");
            tabPaciente = new TabPage("Paciente");
            tabMedico = new TabPage("Medico");

            // Contenido de ejemplo para cada pestaña (ajustá según necesidades)
            tabAdministrativo.Controls.Add(new Label { Text = "Panel Administrativo", Dock = DockStyle.Top, Padding = new Padding(8) });
            tabPaciente.Controls.Add(new Label { Text = "Panel Paciente", Dock = DockStyle.Top, Padding = new Padding(8) });
            tabMedico.Controls.Add(new Label { Text = "Panel Médico", Dock = DockStyle.Top, Padding = new Padding(8) });

            tabControl.TabPages.Add(tabAdministrativo);
            tabControl.TabPages.Add(tabPaciente);
            tabControl.TabPages.Add(tabMedico);

            Controls.Add(tabControl);
            Controls.Add(lblBienvenida);
        }
    }
}