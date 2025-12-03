using ProyectoTurnera.Controller;
using ProyectoTurnera.Model;    
using System;
using System.Drawing;

using System.Windows.Forms;

namespace ProyectoTurnera.Gui
{
    public class LoginForm : Form
    {
        private Label lblUser;
        private Label lblPassword;
        private TextBox txtUser;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnCancel;
        private LinkLabel linkCerrar;

        private GroupBox grpRole;
        private RadioButton rbAdministrador;
        private RadioButton rbMedico;
        private RadioButton rbPaciente;

        public LoginForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Ingreso - Turnera";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(360, 260);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblUser = new Label { Text = "Usuario:", AutoSize = true, Left = 20, Top = 20 };
            txtUser = new TextBox { Left = 120, Top = 16, Width = 200 };

            lblPassword = new Label { Text = "Contraseña:", AutoSize = true, Left = 20, Top = 60 };
            txtPassword = new TextBox { Left = 120, Top = 56, Width = 200, UseSystemPasswordChar = true };

            // Role selection group (radio buttons)
            grpRole = new GroupBox { Text = "Rol", Left = 20, Top = 96, Width = 300, Height = 60 };
            rbAdministrador = new RadioButton { Text = "Administrador", Left = 10, Top = 20, AutoSize = true };
            rbMedico = new RadioButton { Text = "Médico", Left = 120, Top = 20, AutoSize = true };
            rbPaciente = new RadioButton { Text = "Paciente", Left = 200, Top = 20, AutoSize = true };

            // Default selection
            rbAdministrador.Checked = true;

            grpRole.Controls.Add(rbAdministrador);
            grpRole.Controls.Add(rbMedico);
            grpRole.Controls.Add(rbPaciente);

            btnLogin = new Button { Text = "Ingresar", Left = 120, Top = 170, Width = 90 };
            btnLogin.Click += BtnLogin_Click;

            btnCancel = new Button { Text = "Cancelar", Left = 230, Top = 170, Width = 90 };
            btnCancel.Click += (s, e) => Close();

            linkCerrar = new LinkLabel { Text = "Salir", Left = 20, Top = 210, AutoSize = true };
            linkCerrar.Click += (s, e) => Application.Exit();

            Controls.Add(lblUser);
            Controls.Add(txtUser);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(grpRole);
            Controls.Add(btnLogin);
            Controls.Add(btnCancel);
            Controls.Add(linkCerrar);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {

            int dni = Convert.ToInt32(txtUser.Text.Trim());
            string password = txtPassword.Text.Trim();

            try {

                if (rbAdministrador.Checked)
                {

                    AdministradorController administradorcontroller = new AdministradorController();

                    Administrador administrador = administradorcontroller.Login(dni, password);

                    new frmAdministrador(administrador).Show();

                }
                else if (rbMedico.Checked)
                {

                    MedicoController administradorcontroller = new MedicoController();

                    Medico medico = administradorcontroller.Login(dni, password);

                    new frmMedico(medico).Show();

                }
                else if (rbPaciente.Checked)
                {

                    PacienteController administradorcontroller = new PacienteController();

                    Paciente paciente = administradorcontroller.Login(dni, password);

                    new frmPaciente(paciente).Show();
                }
                else
                {
                    MessageBox.Show("Seleccione un rol para continuar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Hide();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("DNI o contraseña incorrectos.", "Error de inicio de sesión",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

    }
}