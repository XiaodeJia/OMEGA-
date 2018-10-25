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
    public partial class StaffLogin : Form
    {
        private Form fm;
        private static int userId;
        private static string userType;
        public StaffLogin()
        {
            InitializeComponent();
        }

        public StaffLogin(int id, string type)
        {
            InitializeComponent();
            userId = id;
            userType = type;
        }
        
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (DialogResult.Yes == result)
            {
                fm = new logIn();
                this.Hide();
                fm.ShowDialog();
                this.Close();
            }
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

        private void btnManageVehile_Click(object sender, EventArgs e)
        {
            this.Hide();
            //add userType later
            fm = new ManageVehile(userId,userType);
            fm.ShowDialog();
            this.Close();
        }

        private void btnManageCustomer_Click(object sender, EventArgs e)
        {
            this.Hide();
            fm = new ManageCustomer();
            fm.ShowDialog();
            this.Close();
        }
        //rental management
        private void btnManageExp_Click(object sender, EventArgs e)
        {
            this.Hide();
            fm = new ManageRental(userId);
            fm.ShowDialog();
            this.Close();
        }

        private void btnManBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            fm = new ManageBooking(userId, userType);
            fm.ShowDialog();
            this.Close();
        }

    }
}
