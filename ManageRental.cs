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
    public partial class ManageRental : Form
    {
        private Form fr;
        private OMEGADBDataContext db;
        private Controller ctr;

        private int userId;

        string emId ; 
        string cId ; 
        string vId ; 
        string exId ; 
        string bId ; 
        string sta ;


        private void setParameters()
        {
            emId= userId.ToString(); 
            cId= txtCustomeId.Text.Trim(); 
            vId= txtVehileId.Text.Trim(); 
            exId= txtExpenditureId.Text.Trim(); 
            bId= ""+0; 
            sta= cobStatus.Text.Trim();
        }
        private bool checkInput()
        {
            if (cobStatus.Text.Trim().Length == 0)
            {
                MessageBox.Show("please select status");
                return false;
            }

            if (!ctr.idValidator(txtEmployId.Text))
            {
                MessageBox.Show("please input employid");
                return false;
            }

            if (!ctr.idValidator(txtCustomeId.Text))
            {
                MessageBox.Show("please input employid");
                return false;
            }
            if (!ctr.idValidator(txtVehileId.Text))
            {
                MessageBox.Show("please input employid");
                return false;
            }
            if (!ctr.idValidator(txtExpenditureId.Text))
            {
                MessageBox.Show("please input employid");
                return false;
            }

            return true;
        }
        public ManageRental()
        {
            InitializeComponent();
        }
        public ManageRental(int id)
        {
            InitializeComponent();
            userId = id;
        }

        private void clear()
        {
            txtRentId.Clear();
            txtEmployId.Text = userId.ToString();
            txtCustomeId.Clear();
            txtVehileId.Clear();
            txtExpenditureId.Clear();
            txtBookId.Clear();
            cobStatus.Text = "";
        }
        private void bindingDataView()
        {
            var rentList = from rent in db.Rentals
                           select new
                           {
                               RentId = rent.RentalId,
                               EmployeeId = rent.employeeId,
                               CustomerId = rent.customerId,
                               VehicleId = rent.vehicleId,
                               ExpenditureId = rent.expenditureId,
                               BookId = rent.bookingId,
                               Status = rent.status

                           };
            this.dataGridView.DataSource = rentList;
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
                setParameters();
                ctr.addRental( emId,  cId,  vId,  exId,  bId,  sta);
                MessageBox.Show("add rent successful" );
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("add fail for the cause" + ex.Message);
                return;
            }

            this.bindingDataView();
        }

        private void ManageRental_Load(object sender, EventArgs e)
        {
            cobStatus.Items.Add("Open");
            cobStatus.Items.Add("Close");
            cobStatus.SelectedIndex = 0;

            txtEmployId.Text = userId.ToString();

            db = new OMEGADBDataContext();
            ctr = new Controller();

            this.bindingDataView();

        }

        //view customer
        private void button4_Click(object sender, EventArgs e)
        {
            try
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
                    this.dataGridView.DataSource = customer;
      
            }
            catch (Exception ex)
            {
                MessageBox.Show("new customer" + ex.Message);
                return;
            }
        }

        private void btnViewVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                 var vehicle = from veh in db.Vehicles
                          select new
                              {
                                  VehicleId = veh.vehicleId,
                                  Maker = veh.maker,
                                  Mode = veh.mode,
                                  MakeTime = veh.makeTime,
                                  VehileColor = veh.color,
                                  odemeter = veh.odometer,
                                  Desciption = veh.description,
                                  Category = veh.categoryType,
                                  Status = veh.status
                              };
                    this.dataGridView.DataSource = vehicle;
            }
            catch (Exception ex)
            {
                MessageBox.Show("no vehicle is available for the cause:"+ex.Message);
                return;
            }
            
        }

        private void btnManageCost_Click(object sender, EventArgs e)
        {
            int expId;

            string fcost = "" + 0;
            string pcost = "" + 0;
            string toffence = "" + 0;
            string dep = "" + 0;
            string rt = "" + 0;
            string tal = "" + 0;
            string sta = "" + 0;
            string staDate = "11/11/1111";
            string endDate = "11/11/1111";

            if(!ctr.idValidator(txtVehileId.Text.Trim()))
            {
                MessageBox.Show("please input vehicle ID");
                return;
            }
            
            if (!ctr.idValidator(txtExpenditureId.Text))
            {
                try
                {

                    expId = ctr.addExpenditure(txtVehileId.Text.Trim(), fcost, pcost, toffence, dep, rt, tal, sta, staDate, endDate);
                    txtExpenditureId.Text = expId.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("add expenditure fail for the cause:" + ex.Message);
                    return;
                }
      
            }
            else
            {
                try
                {
                    expId = int.Parse(txtExpenditureId.Text);

                    this.dataGridView.DataSource = from exp in db.Expenditures where expId == exp.expenditureId select exp;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("get expenditure fail for the id:" + txtExpenditureId.Text+ ex.Message);
                    return;
                }
                
            }
            
        }
        //close book 
        private void btnManageBook_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtBookId.Text))
            {
                    MessageBox.Show("the customer did not book vehicle before" );
                    return;
            }
            int bkId;
            try
            {
                bkId = int.Parse(txtBookId.Text);
                if (bkId == 0)
                {
                    MessageBox.Show("the customer did not book vehicle before" );
                    return;
                }
                ctr.closeBooking(bkId);
                MessageBox.Show("close book list");
            }
            catch (Exception ex)
            {
                MessageBox.Show("close book list for the cause:"+ex.Message);
                return;
            }

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(!ctr.idValidator(txtRentId.Text.Trim()))
            {
                MessageBox.Show("please input Rental Id");
                return;
            }

            int rentId;
            try
            {
                rentId = int.Parse(txtRentId.Text.Trim());
                ctr.deleteRental(rentId);
                MessageBox.Show("Del rent successfully");
   
            }
            catch (Exception ex)
            {
                MessageBox.Show("del rent fail for the cause:" + ex.Message);
                return;
            }

            this.bindingDataView();
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }
            if (!ctr.idValidator(txtRentId.Text.Trim()))
            {
                MessageBox.Show("please input Rental Id");
                return;
            }
            int rentId;
            try
            {
                rentId = int.Parse(txtRentId.Text.Trim());
                setParameters();
                ctr.updateRental(rentId, emId, cId, vId, exId, bId, sta);
                MessageBox.Show("update rent successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("update rent fail for the cause:" + ex.Message);
                return;
            }
            this.bindingDataView();
            clear();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtRentId.Text.Trim()))
            {
                MessageBox.Show("please input Rental Id");
                return;
            }

            int rentId;

            try
            {
                rentId = int.Parse(txtRentId.Text.Trim());
                Rental rent = ctr.getRental(rentId);
                      
                txtEmployId.Text= rent.employeeId.ToString();      
                txtCustomeId.Text= rent.customerId.ToString();     
                txtVehileId.Text= rent.vehicleId.ToString();      
                txtExpenditureId.Text= rent.expenditureId.ToString(); 
                txtBookId.Text= rent.bookingId.ToString();        
                cobStatus.Text= rent.status;   
                MessageBox.Show("search rent successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("search fail for the cause:"+ex.Message);
                return;
            }

     

        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtRentId.Text = this.dataGridView.CurrentRow.Cells[0].Value.ToString();

            try
            {
                int rentId = int.Parse(txtRentId.Text.Trim());
                Rental rent = ctr.getRental(rentId);
                      
                txtEmployId.Text= rent.employeeId.ToString();      
                txtCustomeId.Text= rent.customerId.ToString();     
                txtVehileId.Text= rent.vehicleId.ToString();      
                txtExpenditureId.Text= rent.expenditureId.ToString(); 
                txtBookId.Text= rent.bookingId.ToString();        
                cobStatus.Text= rent.status;   
     
            }
            catch (Exception ex)
            {
                MessageBox.Show("search fail for the cause:"+ex.Message);
                return;
            }
        }

        private void btnViewBook_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtCustomeId.Text.Trim()))
            {
                MessageBox.Show("please input right customer Id");
                return;
            }
            int cusId;
            try
            {
                cusId = int.Parse(txtCustomeId.Text.Trim());
                
                this.dataGridView.DataSource = ctr.getBookListByCustomeId(cusId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("view book list fail for the cause:"+ ex.Message);
                return;
            }
        }
        //update expenditure
        private void button2_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtExpenditureId.Text.Trim()))
            {
                MessageBox.Show("please input expenditure Id");
                return;
            }
            if (!ctr.idValidator(txtVehileId.Text.Trim()))
            {
                MessageBox.Show("please input vehicle Id");
                return;
            }
            int expId = int.Parse(txtExpenditureId.Text.Trim());
          
            fr = new ManageExpenditure(expId, txtVehileId.Text.Trim(), "rent");
            fr.ShowDialog();
          

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cusId = int.Parse(txtCustomeId.Text.Trim());
            this.dataGridView.DataSource = ctr.getExpenditureByCustomerId(cusId);
        }

        private void btnViewRental_Click(object sender, EventArgs e)
        {
            this.bindingDataView();
        }

        private void btnRentReport_Click(object sender, EventArgs e)
        {
            fr = new RentReport();
            fr.ShowDialog();
        }
    }
}
