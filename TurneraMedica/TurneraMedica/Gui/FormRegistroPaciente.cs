using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;
using TurneraMedica.Modelo;

namespace TurneraMedica.Gui
{
    public partial class FormRegistroPaciente : Form
    {
        public FormRegistroPaciente()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Crear usuario
                Usuario nuevo = new Usuario()
                {
                    NombreUsuario = txtUsuario.Text,
                    Password = txtPassword.Text,
                    Rol = "paciente"
                };

                int idUsuario = UsuarioManager.CrearUsuario(nuevo);

                // 2) Crear paciente
                Paciente p = new Paciente()
                {
                    UsuarioId = idUsuario,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    DNI = txtDni.Text,
                    ObraSocial = txtObraSocial.Text
                };

                PacienteManager.CrearPaciente(p);

                MessageBox.Show("Paciente registrado correctamente.");

                new FormInicio().Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Error al registrar paciente.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormRegistroUsuario().Show();
            this.Hide();
        }
    }
}
