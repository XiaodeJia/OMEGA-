using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOMEGA
{
    public partial class ManageCustomer : Form
    {
        public ManageCustomer()
        {
            InitializeComponent();
        }
        private Form fr;
        private Controller ctr;

        OMEGADBDataContext db;
        string fname;
        string lname;
        string birth;
        string mb;
        string eM;
        string addr;
        string sta;

        private void bindDataView()
        {
            var customer = from cus in db.Customers
                           select new
                           {
                               CustomerId = cus.customerId,
                               FirstName = cus.firstName,
                               LastName = cus.lastName,
                               Birthday = cus.dob,
                               Mobile = cus.mobile,
                               Email = cus.email,
                               Address = cus.address,
                               Status = cus.status
                           };
            this.dataGridView1.DataSource = customer;
        }
        private void clear()
        {
            txtCustomeId.Clear();
            txtFname.Clear();
            txtLname.Clear();
            txtDob.Clear();
            txtMobile.Clear();
            txtMail.Clear();
            txtAddr.Clear();
            cobStatus.Text = "";  
        }
        private void getFormValueExceptId()
        {
            fname= txtFname.Text.Trim();
            lname = txtLname.Text.Trim();
            birth = txtDob.Text.Trim();
            mb = txtMobile.Text.Trim();
            eM = txtMail.Text.Trim();
            addr = txtAddr.Text.Trim();
            sta = cobStatus.Text.Trim();       
        }
        private bool checkInput()
        {
            if (!ctr.nameValidator(txtFname.Text.Trim()))
            {
                MessageBox.Show("please input right First name");
                return false;
            }
            else if (!ctr.emailValidator(txtMail.Text.Trim()))
            {
                MessageBox.Show("please input right email");
                return false;
            }
            else if (!ctr.phoneValidator(txtMobile.Text.Trim()))
            {
                MessageBox.Show("please input right phone");
                return false;
            }
            DateTime parseDate;
            if (!DateTime.TryParseExact(txtDob.Text.Trim(), "MM/dd/yyyy", null, DateTimeStyles.None, out parseDate))
            {
                MessageBox.Show("Date format is not correct, should be MM/dd/yyyy");
                return false;
            }
            return true;
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                this.Hide();
                fr = new StaffLogin();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (!checkInput())
            {
                return;
            }

            try
            {
                getFormValueExceptId();
                ctr.addCustomer(fname, lname, birth, mb, eM, addr, sta);
                MessageBox.Show("add successfully");
                bindDataView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("add fail the cause"+ ex.Message);
            }

            clear();
        }

        private void ManageCustomer_Load(object sender, EventArgs e)
        {
            cobStatus.Items.Add("Active");
            cobStatus.Items.Add("Unactive");
            cobStatus.SelectedIndex = 0;

            txtDob.Text = "MM/dd/yyyy";
            ctr = new Controller();
            db = new OMEGADBDataContext();

            bindDataView();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(!ctr.idValidator(txtCustomeId.Text))
            {
                MessageBox.Show("please input Customer ID");
                return;
            }
            int cusId; 
            try
            {
                cusId = int.Parse(txtCustomeId.Text);
                ctr.deleteCustomer(cusId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("del customer fail for the cause:" + ex.Message);
                return;
            }

            bindDataView();
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtCustomeId.Text))
            {
                MessageBox.Show("please input Customer ID");
                return;
            }
            int cusId; 
            try
            {
                cusId = int.Parse(txtCustomeId.Text);
                getFormValueExceptId();
                ctr.updateCustomer(cusId, fname, lname, birth, mb, eM, addr, sta);
                this.bindDataView();
            }
            catch (Exception ex)
            {

                MessageBox.Show("update customer fail for the cause:"+ ex.Message);
                return;
            }
            clear();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtCustomeId.Text))
            {
                MessageBox.Show("please input Customer ID");
                return;
            }
            int cusId;
            try
            {
                cusId = int.Parse(txtCustomeId.Text);
                Customer cus = ctr.getCustomer(cusId);
                txtFname.Text= cus.firstName;   
                txtLname.Text= cus.lastName; 
                txtDob.Text= cus.dob;   
                txtMobile.Text= cus.mobile;   
                txtMail.Text= cus.email;     
                txtAddr.Text=cus.address;   
                cobStatus.Text=cus.status;  
            }
            catch (Exception ex)
            {
                MessageBox.Show("search customer fail for the cause:" + ex.Message);
                return;
            }
            
        }

        private void btnViewRent_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtCustomeId.Text))
            {
                MessageBox.Show("please input Customer ID");
                return;
            }
            int cusId;

            try
            {
                cusId = int.Parse(txtCustomeId.Text);
                var rental = from ren in db.Rentals where ren.customerId == cusId select ren;

                this.dataGridView1.DataSource = rental;
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("View rental fail for the cause" + ex.Message);
                return;
            }
        }

        private void btnViewBook_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtCustomeId.Text))
            {
                MessageBox.Show("please input Customer ID");
                return;
            }
            int cusId;

            try
            {
                cusId = int.Parse(txtCustomeId.Text);
                var book = from bk in db.Bookings where bk.employeeId == cusId select bk;

                this.dataGridView1.DataSource = book;
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("View rental fail for the cause" + ex.Message);
                return;
            }
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtCustomeId.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

            try
            {
                int cusId = int.Parse(txtCustomeId.Text);
                Customer cus = ctr.getCustomer(cusId);
                txtFname.Text = cus.firstName;
                txtLname.Text = cus.lastName;
                txtDob.Text = cus.dob;
                txtMobile.Text = cus.mobile;
                txtMail.Text = cus.email;
                txtAddr.Text = cus.address;
                cobStatus.Text = cus.status;  
            }
            catch (Exception ex)
            {

                MessageBox.Show("double click fail for " + ex.Message);
                return;
            }
           

        }

        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            this.bindDataView();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}
