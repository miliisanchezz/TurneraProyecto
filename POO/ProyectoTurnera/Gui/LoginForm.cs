using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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

        // New controls for role selection
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
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Ingrese usuario y contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Determine selected role and corresponding table
            string selectedTable;
            string selectedRoleKey;

            if (rbAdministrador.Checked)
            {
                selectedTable = "administradores";
                selectedRoleKey = "administrador";
            }
            else if (rbMedico.Checked)
            {
                selectedTable = "medicos";
                selectedRoleKey = "medico";
            }
            else if (rbPaciente.Checked)
            {
                selectedTable = "pacientes";
                selectedRoleKey = "paciente";
            }
            else
            {
                MessageBox.Show("Seleccione un rol para continuar", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string user = txtUser.Text.Trim();
                string pwd = txtPassword.Text.Trim();
                
                string pwdParam = pwd;

                using (var conn = new MySqlConnection(BD.cadena))
                using (var cmd = conn.CreateCommand())
                {
                    // Table name cannot be parameterized; ensure selection was validated above
                    cmd.CommandText = $"SELECT Id FROM {selectedTable} WHERE DNI = @user AND Password = @pwd LIMIT 1";
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pwd", pwdParam);

                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            int id = rdr.GetInt32("Id");

                            // Open the corresponding form based on role selection.
                            // The user requested opening: frmAdministrador, frmEdico y frmEmpleado
                            // These forms are instantiated without parameters; adjust if your constructors require arguments.
                            Form nextForm;
                            switch (selectedRoleKey)
                            {
                                case "administrador":
                                    nextForm = new frmAdministrador();
                                    break;
                                case "medico":
                                    nextForm = new frmMedico();
                                    break;
                                case "paciente":
                                    nextForm = new frmPaciente();
                                    break;
                                default:
                                    nextForm = null;
                                    break;
                            }

                            if (nextForm != null)
                            {
                                nextForm.Show();
                                Hide();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo abrir la ventana correspondiente al rol.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos", "Error de autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al autenticar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}