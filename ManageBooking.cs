using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOMEGA
{
    public partial class ManageBooking : Form
    {
        private Form fr;
        private OMEGADBDataContext db;
        private Controller ctr;
        private string userType;
        private int userId;
        private string vId;
        private string emId;
        private string cusId;
        private string bTime;
        private string rtime;
        private string sta;     

        private void setParameters()
        {
            vId   =  txtVehileId.Text ; 
            emId  =  txtEmployeeId.Text;
            cusId =  txtCustomerId.Text;
            bTime =  txtBookTime.Text ; 
            rtime =  txtRentTime.Text ; 
            sta   =  cobStatus.Text ;
        }
        public ManageBooking()
        {
            InitializeComponent();
        }

        public ManageBooking(int id, string type)
        {
            InitializeComponent();
            userId = id;
            userType = type;
        }
        private void clear()
        {
            txtBookId.Clear();
            txtVehileId.Clear();
            txtEmployeeId.Text = userId.ToString(); ;
            txtCustomerId.Clear();
            txtBookTime.Clear();
            txtRentTime.Clear();
            cobStatus.Text = "";
        }

        private void bindingDataView()
        {
            var bookList = from bk in db.Bookings
                           select new
                           {
                               bookId = bk.bookingId,
                               vehilcleId = bk.vehicleId,
                               employeeId = bk.employeeId,
                               customerId = bk.customerId,
                               bookTime = bk.bookTime,
                               rentTime = bk.rentTime,
                               status = bk.status
                           };
             this.dataGridView.DataSource = bookList;
        }
        private bool checkInput()
        {
            if (!ctr.idValidator(txtEmployeeId.Text))
            {
                MessageBox.Show("please input the employId");
                return false;
            }
            else if (!ctr.idValidator(txtCustomerId.Text))
            {
                MessageBox.Show("please input the custormerId");
                return false;
            }
            else if (!ctr.idValidator(txtVehileId.Text))
            {
                MessageBox.Show("please input the vehicle Id");
                return false;
            }

            DateTime bTime;
            if (!DateTime.TryParseExact(txtBookTime.Text, "MM/dd/yyyy", null, DateTimeStyles.None, out bTime))
            {   
                MessageBox.Show("Book Date format is not correct, should be MM/dd/yyyy");
                return false;
            }

            DateTime rTime;
            if (!DateTime.TryParseExact(txtRentTime.Text, "MM/dd/yyyy", null, DateTimeStyles.None, out rTime))
            {
                MessageBox.Show("Rent Date format is not correct, should be MM/dd/yyyy");
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

        private void ManageBooking_Load(object sender, EventArgs e)
        {
            db = new OMEGADBDataContext();
            ctr = new Controller();

            txtEmployeeId.Text = userId.ToString();
            txtBookTime.Text= "MM/DD/YYYY";
            txtRentTime.Text = "MM/DD/YYYY";
            cobStatus.Items.Add("Open");
            cobStatus.Items.Add("Close");

            cobStatus.SelectedIndex = 0 ;

            this.bindingDataView();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }
            
            try
            {
                setParameters();
                ctr.addBooking(vId,  emId,  cusId,  bTime,  rtime,  sta);
                MessageBox.Show("add Book info successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("add Book info fails for the cause:"+ex.Message);
                return;
            }
            clear();
            bindingDataView();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtBookId.Text))
            {
                MessageBox.Show("please input book id");
                return;
            }
            int bkId;
            try
            {
                bkId = int.Parse(txtBookId.Text.Trim());
                ctr.deleteBooking(bkId);
                MessageBox.Show("del Book info successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("del book list fail for the cause"+ex.Message);
                return;
            }

            bindingDataView();
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return; 
            }
            int bkId;
            try
            {
                bkId = int.Parse(txtBookId.Text.Trim());
                setParameters();
                ctr.updateBooking(bkId, vId, emId, cusId, bTime, rtime, sta);
                MessageBox.Show("update Book info successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("update book list fail for the cause" + ex.Message);
                return;
            }

            bindingDataView();
            clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtBookId.Text))
            {
                MessageBox.Show("please input book id");
                return;
            }
            int bkId;
            Booking bk;
            try
            {
                bkId = int.Parse(txtBookId.Text.Trim());
                bk = ctr.getBooking(bkId);
                MessageBox.Show("get Book info successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("get book list fail for the cause" + ex.Message);
                return;
            }

            txtVehileId.Text= bk.vehicleId.ToString();
            txtEmployeeId.Text = bk.employeeId.ToString(); ;
            txtCustomerId.Text = bk.customerId.ToString(); 
            txtBookTime.Text= bk.bookTime; 
            txtRentTime.Text= bk.rentTime; 
            cobStatus.Text= bk.status;
            
        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtBookId.Text = this.dataGridView.CurrentRow.Cells[0].Value.ToString();
                             
            int bkId;
            Booking bk;
            try
            {
                bkId = int.Parse(txtBookId.Text);
                bk = ctr.getBooking(bkId);
                txtVehileId.Text = bk.vehicleId.ToString();
                txtEmployeeId.Text = bk.employeeId.ToString(); ;
                txtCustomerId.Text = bk.customerId.ToString();
                txtBookTime.Text = bk.bookTime;
                txtRentTime.Text = bk.rentTime;
                cobStatus.Text = bk.status;

            }
            catch (Exception ex)
            {
                MessageBox.Show("get book list fail for the cause" + ex.Message);
                return;
            }
        }

        private void btnViewCustomer_Click(object sender, EventArgs e)
        {
            
            try
            {
                var customerList = from cus in db.Customers
                                   select new
                                    {
                                        CustomerId = cus.customerId,
                                        FirstName = cus.firstName,
                                        Mobile    = cus.mobile,
                                        Status = cus.status
                                    };
                this.dataGridView.DataSource = customerList;

            }
            catch (Exception ex)
            {
                MessageBox.Show("view customer fail for the cause" + ex.Message);
                return;
            }
        }

        private void btnViewVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                var vehicleList = from vec in db.Vehicles
                                   select new
                                   {
                                       VehicleId = vec.vehicleId,
                                       Maker = vec.maker,
                                       Mode = vec.mode,
                                       Status = vec.status
                                   };
                this.dataGridView.DataSource = vehicleList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("view customer fail for the cause" + ex.Message);
                return;
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


    }
}
