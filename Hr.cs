using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOMEGA
{
    public partial class Hr : Form
    {
        public Hr()
        {
            InitializeComponent();
        }
        Form fr;
        private void btnManEmployee_Click(object sender, EventArgs e)
        {
            this.Hide();
            fr = new ManageEmployee();
            fr.ShowDialog();
            this.Close();
            
        }

        private void btnManageSalary_Click(object sender, EventArgs e)
        {
            this.Hide();
            fr = new ManageSalary();
            fr.ShowDialog();
            this.Close();
        }

        private void btnManTmsheet_Click(object sender, EventArgs e)
        {
            this.Hide();
            fr = new ManageTimeSheet(1,"Hr");//only hr manage timesheet
            fr.ShowDialog();
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.No == result)
            {
                return;
            }
            this.Hide();
            Form fs = new logIn();
            fs.ShowDialog();
            this.Close();
        }
    }
}
