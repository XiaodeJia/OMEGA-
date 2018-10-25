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
    public partial class ManageTimeSheet : Form
    {

        public delegate void IndentityHandler(object sender, TimeSheetIdentityEvent e);
        public TimeSheetIdentityEvent TimeSheetEvent;
        public event IndentityHandler IdentityUpdated;

        private Controller ctr;
        private OMEGADBDataContext db;
        private int timesheetId;
        private int userId;
        private string userType;
        private Form fm;

        public ManageTimeSheet()
        {
            InitializeComponent();
        }
        //type not for employee,for to go back the form of type(hr/salary)
        public ManageTimeSheet(int id,string type)
        {
            InitializeComponent();
            userId = id;
            userType = type;
        }

        public ManageTimeSheet(int id, int tsId, string type)
        {
            InitializeComponent();
            userId = id;
            userType = type;
            timesheetId = tsId; 
        }
        private void bindingDateGridView()
        {
            var ts = from t in db.TimeSheets
                        select new
                        {
                            timesheetId = t.timeSheetId,
                            empployeeId = t.employeeId,
                            workinghours = t.workingHours,
                            overTimeHours = t.overTimeHours
                        };

            dataGridViewTimeSheet.DataSource = ts;
        }
        private void refresh()
        {
            txtSheetId.Clear();
            txtSheetId.Clear();
            txtSheetId.Clear();
            txtSheetId.Clear();
        }
        //add
        private void button1_Click(object sender, EventArgs e)
        {
            int wkHour;
            int ovHour;
            try
            {
                wkHour = int.Parse(txtWkHour.Text.Trim());
                ovHour = int.Parse(txtOvertimeHur.Text.Trim());
            }
            catch 
            {
                
                MessageBox.Show("please input correct time");
                return;
            }
            string emId = txtEmId.Text.Trim();
            string ws = txtWkHour.Text.Trim();
            string os = txtOvertimeHur.Text.Trim();
            int tmId;
            try
            {
                tmId = ctr.addTimeSheet(emId, ws, os);
                MessageBox.Show("add successful");
            }
            catch
            {
                MessageBox.Show("add fail");
                return;
            }
            finally
            {
                
            }
            refresh();
            bindingDateGridView();
            // publish the timesheetId to salaryS
            if (userType.Equals("Salary"))
            {
                TimeSheetEvent = new TimeSheetIdentityEvent(tmId, ws, os);
                IdentityUpdated(this, TimeSheetEvent);
                MessageBox.Show("add timesheet of salary for emloyee ID:"+userId+" successfully");
                this.Dispose();
                
            }

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                this.Hide();
                if (userType.Equals("Hr"))
                {
                    fm = new Hr();
                }
                else 
                {
                    this.Close();
                    return;
                }
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

        private void ManageTimeSheet_Load(object sender, EventArgs e)
        {
            ctr = new Controller();
            db = new OMEGADBDataContext();
            if (userType.Equals("Salary"))
            {
                txtEmId.Text = userId.ToString();
                txtSheetId.Text = timesheetId.ToString();
            }

            this.bindingDateGridView();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int emId;

            DialogResult result = MessageBox.Show("Are you sure you want to delete all employee timesheet?", "Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            try
            {
                emId = int.Parse(txtEmId.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("del fail");
                return;
            }

            if (DialogResult.Yes == result)
            {
                ctr.delTimeSheetByUserId(emId);
            }

            bindingDateGridView();
            refresh();
            return;     
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int tmId;
            int emId;
            try
            {
                tmId = int.Parse(txtSheetId.Text.Trim());
                emId = int.Parse(txtEmId.Text.Trim());
            }
            catch
            {
                MessageBox.Show("please input timesheetID/employeeID");
                return;
            }
            ctr.updateTimeSheet(tmId, txtEmId.Text.Trim(), txtWkHour.Text.Trim(), txtOvertimeHur.Text.Trim());
           
            if (userType.Equals("Salary"))
            {
                TimeSheetEvent = new TimeSheetIdentityEvent(tmId, txtWkHour.Text.Trim(), txtOvertimeHur.Text.Trim());
                IdentityUpdated(this, TimeSheetEvent);
                MessageBox.Show("update timesheet of salary for emloyee ID:"+userId+" successfully");
                this.Dispose();
            }
            refresh();
            bindingDateGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //search by timesheetID;
            if (ctr.idValidator(txtSheetId.Text))
            {
                try
                {
                    int tsheetId = int.Parse(txtSheetId.Text.Trim());
                    TimeSheet tsheet = ctr.getTimeSheet(tsheetId);
                    txtEmId.Text = tsheet.employeeId.ToString();
                    txtWkHour.Text = tsheet.workingHours;
                    txtOvertimeHur.Text = tsheet.overTimeHours;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("no timesheet for the ID:" + txtSheetId.Text + "for the cause:"+ex.Message);
                }
           
                return;
            }
            //search by employeeId
            int emId;
            try
            {
                emId = int.Parse(txtEmId.Text.Trim());
            }
            catch
            {
                MessageBox.Show("employee ID wrong ");
                return;
            }
            try
            {
                var ts = from t in db.TimeSheets
                         where t.employeeId == emId
                         select new
                         {
                             timesheetId = t.timeSheetId,
                             empployeeId = t.employeeId,
                             workinghours = t.workingHours,
                             overTimeHours = t.overTimeHours
                         };

                dataGridViewTimeSheet.DataSource = ts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("no timesheet for employee ID:" + emId +"for the cause:"
                                 + ex.Message);
                return;
            }
            
        }

        private void dataGridViewTimeSheet_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int tsheetId;
            txtSheetId.Text = this.dataGridViewTimeSheet.CurrentRow.Cells[0].Value.ToString();

            tsheetId = int.Parse(txtSheetId.Text.Trim());
            TimeSheet tsheet = ctr.getTimeSheet(tsheetId);

            txtEmId.Text = tsheet.employeeId.ToString();
            txtWkHour.Text = tsheet.workingHours;
            txtOvertimeHur.Text = tsheet.overTimeHours;
        }

    
    }
}
