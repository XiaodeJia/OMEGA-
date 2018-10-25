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
    public partial class logIn : Form
    {
        Form lf;
        //static string eMan = "Manager";
        //static string eEm = "Staff";
        //static string mHr = "Hr";
        
        Controller ctr ;
        public logIn()
        {
            InitializeComponent();
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

        private void logIn_Load(object sender, EventArgs e)
        {
            cobJobTitle.Items.Add("Please select your job title");
            cobJobTitle.Items.Add("Manager");
            cobJobTitle.Items.Add("Staff");
            cobJobTitle.Items.Add("Hr");
            cobJobTitle.SelectedIndex = 0;

            ctr = new Controller();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           

            if (!ctr.idValidator(txtId.Text.Trim())
                || !ctr.passwordValidator(txtPassword.Text.Trim()))
            {
                MessageBox.Show("please input the right Id and password");
                return;
            }
            if (cobJobTitle.SelectedIndex == 0)
            {
                MessageBox.Show("please select you job title");
                return;
            }
            int id = Convert.ToInt32(txtId.Text.Trim());

            string psw = txtPassword.Text.Trim();
            string type = cobJobTitle.Text.Trim();
            if (!ctr.checkUser(id, psw, type))
            {
                MessageBox.Show("password/jobtitle is wrong");
                return;
            }

            if (type.Equals("Manager"))
            {
                lf = new MangerLogin(id, type);
            }
            else if (type.Equals("Staff"))
            {
                lf = new StaffLogin(id,type);
            }
            else if (type.Equals("Hr"))
            {
                lf = new Hr();
            }

            this.Hide();
            lf.ShowDialog();
            this.Close();
        }


    }
}
