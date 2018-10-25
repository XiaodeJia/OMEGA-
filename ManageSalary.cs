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
    public partial class ManageSalary : Form
    {
        public ManageSalary()
        {
            InitializeComponent();
        }
        Controller ctr;
        OMEGADBDataContext db;
        private int tSheetId;
        private string whour;
        private string ohour;


        private void bindingDateGridView()
        {
            var sal = from s in db.Salaries
                        select new
                        {
                            SalaryId = s.salaryId,
                            EmployeeId = s.employeeId,
                            TimesheetId = s.timeSheetId,
                            WorkSalary = s.workSalary,
                            OvertimeSalary = s.overtimeSalary,
                            Bonus = s.bonus,
                            SlaryDate = s.salaryDate
                        };

            dataGridView.DataSource = sal;
        }
        private void refresh()
        {
            TxtWs.Clear();
            txtOs.Clear();
            txtBo.Clear();
            txtTotal.Clear();
            txtDate.Clear();
            cobWt.SelectedIndex = 0;
            txtDate.Text = "MM/dd/yyyy";
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
            Form fs = new Hr();
            fs.ShowDialog();
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
        public void getWorkTime(object sender, TimeSheetIdentityEvent e)
        {
            tSheetId = e.TimeSheetId;
            whour = e.WorkTime;
            ohour = e.OverTime;
            txtTsid.Text = e.TimeSheetId.ToString();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int tsId;
            
            string ws = TxtWs.Text.Trim();
            string os = txtOs.Text.Trim();
            string bs = txtBo.Text.Trim();
            string tal= txtTotal.Text.Trim();
            string wt = txtDate.Text.Trim();
            try
            {
                tsId = int.Parse(txtTsid.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("no time sheet, please add one");
                return;
            }
            if (!ctr.dateValidator(txtDate.Text.Trim()))
            {
                MessageBox.Show("Date format is not correct, should be MM/dd/yyyy");
                return;
            }
            //calc salary
            int sId = ctr.addSalary(txtEmid.Text.Trim(), txtTsid.Text.Trim(), ws, os, bs, tal, wt);
           bindingDateGridView();
           refresh();
        }

        private void ManageSalary_Load(object sender, EventArgs e)
        {
            ctr = new Controller();
            db = new OMEGADBDataContext();

            cobWt.Items.Add("FullTime");
            cobWt.Items.Add("PartTime");
            cobWt.SelectedIndex = 0;

            txtDate.Text = "MM/dd/yyyy";
            bindingDateGridView();

        }
        //view timesheet
        private void btnManT_Click(object sender, EventArgs e)
        {
            int emId;
            try
            {
                emId = int.Parse(txtEmid.Text.Trim()); 
            }
            catch 
            {
                MessageBox.Show("please input employee id");
                return;
            }
           

            try
            { 
                var tsList = from ts in db.TimeSheets where ts.employeeId == emId select ts;
                this.dataGridView.DataSource = tsList;
                MessageBox.Show("Timesheet of the Employee show success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Timesheet of the Employee show fail for the cause:"+ex.Message);
                return;
            }

        }
        //calculate salary
        private void button1_Click(object sender, EventArgs e)
        {

            //part time
            //full time
            double wBasicSal;
            double overTimeSal;
            double bonousRate;
          
            double wSalary;
            double oSalary;
            double bonus;
            double totalSalary;

            if (cobWt.SelectedIndex == 0)//full time
	        {
		        wBasicSal = 85.55;
                overTimeSal = 32.25;
                bonousRate = 0.1;
                
	        }
            else if (cobWt.SelectedIndex == 1)//part time
	        {
		        wBasicSal = 42.3;
                overTimeSal = 0;
                bonousRate = 0;
               
	        }
            else
	        {
                MessageBox.Show("please select work type");
                return;
	        }
            //get work Time
            if (!ctr.idValidator(txtTsid.Text))
            {
                MessageBox.Show("please select Timesheet");
                return;
            }
            int tsId;
            string whour;
            string ohour;
            try
            {
                tsId = int.Parse(txtTsid.Text.Trim());
                TimeSheet tSheet = ctr.getTimeSheet(tsId);
                whour = tSheet.workingHours;
                ohour = tSheet.overTimeHours;

            }
            catch (Exception ex)
            {
                MessageBox.Show("no timesheet for the employee for the cause:"+ex.Message);
                return;
            }
            wSalary= int.Parse(whour) * wBasicSal;
            oSalary = int.Parse(ohour) * overTimeSal ;
            bonus = wSalary * 52 * bonousRate;
            totalSalary = wSalary + oSalary + bonus;

            TxtWs.Text = "" + wSalary;
            txtOs.Text = "" + oSalary;
            txtBo.Text = "" + bonus;
            txtTotal.Text = "" + totalSalary;
        }

        private void btnSe_Click(object sender, EventArgs e)
        {
            int emId;
            try
            {
                emId = int.Parse(txtEmid.Text.Trim());
            }
            catch
            {
                MessageBox.Show("please select an empoyeeId");
                return;
            }

            dataGridView.DataSource = ctr.getSalaryByUserId(emId);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int emId;
            try
            {
                emId = int.Parse(txtEmid.Text.Trim());
            }
            catch
            {
                MessageBox.Show("please select an empoyeeId");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to del the salary list?", "Logout",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.No == result)
            {
                return;
            }

            try
            {
                ctr.deleteSalaryByUserID(emId);
                MessageBox.Show("delete success");
            }
            catch (Exception ex)
            {
                MessageBox.Show("delete fail:" + ex.Message);
                return;
            }

            bindingDateGridView();
            refresh();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int salarId;
            int timesheetId;
            string emId = txtEmid.Text.Trim();
            
            string ws = TxtWs.Text.Trim();
            string os = txtOs.Text.Trim();
            string bs = txtBo.Text.Trim();
            string tal = txtTotal.Text.Trim();
            string sDate = txtDate.Text.Trim();

            try
            {
                salarId = int.Parse(txtSid.Text.Trim());
                timesheetId = int.Parse(txtTsid.Text.Trim());
                ctr.updateSalary(salarId,emId, txtTsid.Text.Trim(), ws, os, bs, tal, sDate);
                MessageBox.Show("update success");
            }
            catch 
            {
                MessageBox.Show("update fail");
            }

            bindingDateGridView();
            refresh();

        }

        private void btnAddTimeSheet_Click(object sender, EventArgs e)
        {
            int emId;
            try
            {
                emId = int.Parse(txtEmid.Text.Trim());
            }
            catch
            {
                MessageBox.Show("please select an empoyeeId");
                return;
            }

            try
            {
                ManageTimeSheet ts = new ManageTimeSheet(emId,"Salary");
                ts.IdentityUpdated += new ManageTimeSheet.IndentityHandler(getWorkTime);
                ts.ShowDialog();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("add new timesheet fail for the cause:"+ex.Message);
                return;
            }
        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtSid.Text = this.dataGridView.CurrentRow.Cells[0].Value.ToString();

            int salaryId;
            try
            {
                salaryId = int.Parse(txtSid.Text.Trim());
                Salary sal = ctr.getSalary(salaryId);
                txtEmid.Text = sal.employeeId.ToString();
                txtTsid.Text = sal.timeSheetId.ToString();
                TxtWs.Text = sal.workSalary;
                txtOs.Text = sal.overtimeSalary;
                txtBo.Text = sal.bonus;
                txtTotal.Text = sal.total;
                txtDate.Text = sal.salaryDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("fail for the cause :"+ex.Message);
                return;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int emId;
    
            int timesheetId;
            try
            {
                emId = int.Parse(txtEmid.Text.Trim());
            
                timesheetId = int.Parse(txtTsid.Text.Trim());

            }
            catch
            {
                MessageBox.Show("please select an salaryID/empoyeeID/TimesheetID");
                return;
            }

   
            try
            {
                ManageTimeSheet ts = new ManageTimeSheet(emId, timesheetId, "Salary");
                ts.IdentityUpdated += new ManageTimeSheet.IndentityHandler(getWorkTime);
                ts.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("add new timesheet fail for the cause:" + ex.Message);
                return;
            }
        }



    }
}
