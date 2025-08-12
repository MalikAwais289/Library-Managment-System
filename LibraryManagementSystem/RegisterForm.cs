using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class RegisterForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Server=.\SQLEXPRESS;Database=LibraryDB;Integrated Security=True;Connect Timeout=30");

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void signIn_btn_Click(object sender, EventArgs e)
        {
            LoginForm lForm = new LoginForm();
            lForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();

                foreach (byte t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if (register_email.Text == "" || register_username.Text == "" || register_password.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        // Check if the username already exists
                        string checkUsername = "SELECT COUNT(*) FROM users WHERE username = @username";
                        using (SqlCommand checkCMD = new SqlCommand(checkUsername, connect))
                        {
                            checkCMD.Parameters.AddWithValue("@username", register_username.Text.Trim());
                            int count = (int)checkCMD.ExecuteScalar();

                            if (count >= 1)
                            {
                                MessageBox.Show(register_username.Text.Trim() + " is already taken", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                // Hash the password before inserting into the database
                                string hashedPassword = HashPassword(register_password.Text.Trim());

                                // Insert the data into the users table
                                DateTime day = DateTime.Today;

                                string insertData = "INSERT INTO users (email, username, PasswordHash, FullName, DateCreated) " +
                     "VALUES(@email, @username, @password, @fullName, @date)";



                                using (SqlCommand insertCMD = new SqlCommand(insertData, connect))
                                {
                                    insertCMD.Parameters.AddWithValue("@email", register_email.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@username", register_username.Text.Trim()); // Assign FullName to username
                                    insertCMD.Parameters.AddWithValue("@password", hashedPassword);  // Insert the hashed password
                                    insertCMD.Parameters.AddWithValue("@fullName", register_username.Text.Trim()); // Add FullName parameter
                                    insertCMD.Parameters.AddWithValue("@date", day.ToString());

                                    insertCMD.ExecuteNonQuery();

                                    MessageBox.Show("Register successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoginForm lForm = new LoginForm();
                                    lForm.Show();
                                    this.Hide();
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        private void register_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
