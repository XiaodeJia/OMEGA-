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
    public partial class MangerLogin : Form
    {
        private Form fr;
        private int userId;
        private static string userType;
        public MangerLogin()
        {
            InitializeComponent();
        }

        public MangerLogin(int id, string type)
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
                this.Hide();
                fr = new logIn();
                fr.ShowDialog();
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

        private void btnManVehilce_Click(object sender, EventArgs e)
        {
            fr = new ManageVehile(userId, userType);
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void btnManCate_Click(object sender, EventArgs e)
        {
            fr = new ManageCategory();
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }

        private void btnVeReport_Click(object sender, EventArgs e)
        {
            fr = new VehicleReport();
            this.Hide();
            fr.ShowDialog();
            this.Close();
        }
    }
}
