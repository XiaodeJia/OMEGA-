using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NOMEGA
{
    public partial class ManageExpenditure : Form
    {
        private Form fr;
        private OMEGADBDataContext db;
        private Controller ctr;
        private static string userType;
        private int expId;
        private string vId;
        private string fcost   ;
        private string pcost   ;
        private string toffence; 
        private string dep     ;
        private string rt;      
        private string tal;     
        private string sta ;
        private string staDate;
        private string endDate;
     
        public ManageExpenditure()
        {
            InitializeComponent();
        }
        //type for logout back to form
        public ManageExpenditure(int id, string veId, string type)
        {
            InitializeComponent();
            expId = id;
            vId = veId;
            userType = type;
        }
        public ManageExpenditure(int id)
        {
            InitializeComponent();
            expId = id;
        }

        private void bindingDataView()
        {
            var expList = from exp in db.Expenditures
                          select new
                          {
                              ExpenditureId = exp.expenditureId,
                              VehicleId = exp.vehicleId,
                              FuelCost = exp.fuelCost,
                              ParkOffence = exp.parkOffence,
                              TrafficeOffence = exp.trafficeOffence,
                              Deposit = exp.deposit,
                              RentCost = exp.rental,
                              TotalCost = exp.total,
                              Status = exp.status,
                              StartTime = exp.StartDate,
                              EndTime = exp.EndDate

                          };
            this.dataGridView.DataSource = expList;
        }
        private void setParameteres()
        {
            vId = txtVehileId.Text.Trim();
            fcost = txtFuelCost.Text.Trim();
            pcost = txtParkOffence.Text.Trim();
            toffence = txtTrafficOffence.Text.Trim();
            dep = txtDeposit.Text.Trim();
            rt = txtRentCost.Text.Trim();
            tal = txtTotal.Text.Trim();
            sta = cobStatus.Text.Trim();
            staDate = txtStartDate.Text.Trim();
            endDate = txtEndDate.Text.Trim();
        }

        private void clear()
        {
            txtExpenId.Text = "";
            txtFuelCost.Text = "" + 0;
            txtParkOffence.Text= ""+0;
            txtTrafficOffence.Text = "" + 0;

            txtDeposit.Text= ""+0;
            txtRentCost.Text= ""+0;
            txtTotal.Text= ""+0;
            cobStatus.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.No == result)
            {
                return;
            }
            if (userType.Equals("rent"))
	        {
		        this.Dispose();
                return;
	        }
            else
	        {
                fr = new StaffLogin();
	        }
            this.Hide();
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

        private void ManageExpenditure_Load(object sender, EventArgs e)
        {
            
            cobStatus.Items.Add("Open");
            cobStatus.Items.Add("Close");
            
            //clear();
            db = new OMEGADBDataContext();
            ctr = new Controller();
            this.bindingDataView();

            if (userType.Equals("rent"))
            {
                txtExpenId.Text = expId.ToString();
                txtVehileId.Text = vId;
                txtExpenId.ReadOnly = true;
                txtVehileId.ReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int total;
            int exId;
            
            try
            {
                setParameteres();

                exId = ctr.addExpenditure(vId,fcost, pcost, toffence, dep, rt, tal, sta, staDate,endDate);
                MessageBox.Show("Add successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add expenditure fail for the cause:"+ex.Message);
                return;
            }
            clear();
            this.bindingDataView();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(!ctr.idValidator(txtExpenId.Text))
            {
                MessageBox.Show("Please input expenditure ID");
                return;
            }
            int exId;
            try
            {
                exId = int.Parse(txtExpenId.Text);
                ctr.deleteExpenditure(exId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("del expenditure for the cause:"+ex.Message);
                return;
            }
            clear();
            this.bindingDataView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int exId;
            try
            {
                exId = int.Parse(txtExpenId.Text);
                setParameteres();
                ctr.updateExpenditure(exId, vId, fcost, pcost, toffence, dep, rt, tal, sta, staDate, endDate);
                MessageBox.Show("update successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("update fail for the cause:" + ex.Message);
                return;
            }

            clear();
            this.bindingDataView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtExpenId.Text))
            {
                MessageBox.Show("Please input expenditure ID");
                return;
            }
            int exId;
            Expenditure exp;
            try
            {
                exId = int.Parse(txtExpenId.Text);
                exp = ctr.getExpenditure(exId);
                MessageBox.Show("search successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("del expenditure for the cause:" + ex.Message);
                return;
            }
            txtVehileId.Text = "" + exp.vehicleId;
            txtFuelCost.Text = "" + exp.fuelCost;
            txtParkOffence.Text = "" + exp.parkOffence;
            txtTrafficOffence.Text = "" + exp.trafficeOffence;
            txtDeposit.Text = "" + exp.deposit;
            txtRentCost.Text = "" + exp.rental;
            txtTotal.Text = "" + exp.total;
            cobStatus.Text = ""+exp.status;
            txtStartDate.Text = exp.StartDate;
            txtEndDate.Text = exp.EndDate;
            
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            //get rent fee
            if (!ctr.idValidator(txtVehileId.Text))
            {
                MessageBox.Show("please input vehicle Id" );
                return;
            }
 
            DateTime startDate;
            DateTime endDate;
            if (!DateTime.TryParseExact(txtStartDate.Text.Trim(), "MM/dd/yyyy", null, DateTimeStyles.None, out startDate))
            {
                MessageBox.Show("Date format is not correct, should be MM/dd/yyyy");
                return;
            }
            if (!DateTime.TryParseExact(txtEndDate.Text.Trim(), "MM/dd/yyyy", null, DateTimeStyles.None, out endDate))
            {
                MessageBox.Show("Date format is not correct, should be MM/dd/yyyy");
                return;
            }
            //get the basic rent fee
            int vId;
            double rentFee;
            Vehicle vehicle;
            Category cate;
            TimeSpan dateInterval = endDate - startDate;
        
            try
            {
                vId = int.Parse(txtVehileId.Text.Trim());
                vehicle = ctr.getVehiclebyId(vId);
                cate = ctr.getCategorybyType(vehicle.categoryType);        
            }
            catch (Exception ex)
            {
                MessageBox.Show("cannot find category for the cause:"+ex.Message);
                return;
            }

            rentFee = (dateInterval.TotalDays) * (int.Parse(cate.rentalRate.Trim()));

            try
            {
                setParameteres();
                int total = int.Parse(fcost) + int.Parse(pcost)
                       + int.Parse(toffence) - int.Parse(dep)
                       + (int)rentFee;
                txtTotal.Text = total.ToString();
                MessageBox.Show("calculate successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("calculate fail for the cause:" + ex.Message);
                return;
            }
            bindingDataView();
        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtExpenId.Text = this.dataGridView.CurrentRow.Cells[0].Value.ToString();
            
            int expId;
            Expenditure exp;

            try 
	        {
                expId = int.Parse(txtExpenId.Text.Trim());
                exp = ctr.getExpenditure(expId);

                txtVehileId.Text = exp.vehicleId.ToString();
                txtFuelCost.Text = exp.fuelCost.ToString();
                txtParkOffence.Text = exp.parkOffence.ToString();
                txtTrafficOffence.Text = exp.trafficeOffence.ToString();
                txtDeposit.Text = exp.deposit.ToString();
                txtRentCost.Text = exp.rental.ToString();
                txtTotal.Text = exp.total.ToString();
                cobStatus.Text = exp.status;
                txtStartDate.Text = exp.StartDate;
                txtEndDate.Text = exp.EndDate;
	        }
	        catch (Exception ex)
	        {
                MessageBox.Show("get expenditure fail for the cause:" + ex.Message);
                return;
	        }
            
        }

        private void btnReport_Click(object sender, EventArgs e)
        {       
            fr = new RentalReport();
            fr.ShowDialog();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
