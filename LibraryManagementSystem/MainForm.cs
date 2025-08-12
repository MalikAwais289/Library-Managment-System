using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Are you sure you want to logout?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                LoginForm lForm = new LoginForm();
                lForm.Show();
                this.Hide();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = true;
            addBooks1.Visible = false;
            returnBooks1.Visible = false;
            issueBooks1.Visible = false;

            Dashboard dForm = dashboard1 as Dashboard;
            if (dForm != null)
            {
                dForm.refreshData();
            }
        }

        private void addBooks_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            addBooks1.Visible = true;
            returnBooks1.Visible = false;
            issueBooks1.Visible = false;

            AddBooks aForm = addBooks1 as AddBooks;
            if (aForm != null)
            {
                aForm.refreshData();
            }
        }

        private void issueBooks_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            addBooks1.Visible = false;
            returnBooks1.Visible = false;
            issueBooks1.Visible = true;

            IssueBooks iForm = issueBooks1 as IssueBooks;
            if (iForm != null)
            {
                iForm.refreshData();
            }
        }

        private void returnBooks_btn_Click(object sender, EventArgs e)
        {
            dashboard1.Visible = false;
            addBooks1.Visible = false;
            returnBooks1.Visible = true;
            issueBooks1.Visible = false;

            ReturnBooks rForm = returnBooks1 as ReturnBooks;
            if (rForm != null)
            {
                rForm.refreshData();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Custom painting logic, if needed
        }
    }
}
