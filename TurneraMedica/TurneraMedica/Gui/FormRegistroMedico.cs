using System;
using System.Windows.Forms;
using TurneraMedica.Controlador;
using TurneraMedica.Modelo;

namespace TurneraMedica.Gui
{
    public partial class FormRegistroMedico : Form
    {
        public FormRegistroMedico()
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
                    Rol = "medico"
                };

                int idUsuario = UsuarioManager.CrearUsuario(nuevo);

                // 2) Crear médico
                Medico medico = new Medico()
                {
                    UsuarioId = idUsuario,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    Especialidad = txtEspecialidad.Text,
                    Precio = decimal.Parse(txtPrecio.Text)
                };

                MedicoManager.CrearMedico(medico);

                MessageBox.Show("Médico registrado correctamente.");

                new FormInicio().Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Error al registrar. Revisá los datos.");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            new FormRegistroUsuario().Show();
            this.Hide();
        }
    }
}
