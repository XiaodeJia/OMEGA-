using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Configuration;
namespace NOMEGA
{
    public partial class ManageEmployee : Form
    {
        public ManageEmployee()
        {
            InitializeComponent();
        }

        private Controller ctr ;
        private OMEGADBDataContext db ;
        private Form nf;


        private bool checkImput()
        {
            if (!ctr.nameValidator(txtFirstName.Text.Trim())
                           || !ctr.nameValidator(txtLastName.Text.Trim()))
            {
                MessageBox.Show("Name Format is invalid", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            else if (cobUserType.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please select Job Title", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            else if (cobGender.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please select gender", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
            else if (!ctr.dateValidator(txtBob.Text.Trim()))
            {
                MessageBox.Show("Date format is not correct, should be MM/dd/yyyy");
                return false;
            }
            else if (cklWorkType.SelectedIndex == -1)
            {
                MessageBox.Show("please select an work type!");
                return false;
            }
            return true;

        }
        private void bindingDataGridView()
        {
            this.dataGridView.DataSource = from em in db.Employees
                                           select new
                                           {
                                               EmloyeeId = em.employeeId,
                                               FirtName = em.firstName,
                                               LastName = em.lastName,
                                               Gender = em.gender,
                                               Dob = em.dob,
                                               JobTitle = em.jobTitle,
                                               Password = em.password
                                           };
    
        }
        private void refresh()
        {

            cobUserType.Text = ""; 
            cobGender.Text = "";
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmployId.Clear();
            txtBob.Clear();
            txtPassword.Clear();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ctr.passwordValidator(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Password Format is invalid", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }     
            //
            string fname = txtFirstName.Text.Trim();
            string lname = txtLastName.Text.Trim();
            string sex = cobGender.Text.Trim();
            string birthday = txtBob.Text.Trim();
            string userType = cobUserType.Text.Trim();
            string password = txtPassword.Text.Trim();

            string md5Password = ctr.GetMd5Hash(password);
            try
            {
                int emId = ctr.addUser(fname, lname, sex, birthday, userType, password);
                string workTypeId = cklWorkType.SelectedIndex.ToString();
                
                refresh();
                bindingDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'nOMEGADataSet.Employee' table. You can move, or remove it, as needed.
            //this.employeeTableAdapter1.Fill(this.nOMEGADataSet.Employee);
            // TODO: This line of code loads data into the 'employeeDataSet.Employee' table. You can move, or remove it, as needed.
            
            cobUserType.Items.Add("select jobtitle");
            cobUserType.Items.Add("Manager");
            cobUserType.Items.Add("Staff");
            cobUserType.Items.Add("Hr");
            cobUserType.SelectedIndex = 0;
            cobGender.Items.Add("");
            cobGender.Items.Add("Male");
            cobGender.Items.Add("Female");
            cobGender.SelectedIndex = 0;

            txtBob.Text = "MM/DD/YYYY";

            
             ctr = new Controller();
             db = new OMEGADBDataContext();

             bindingDataGridView();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                this.Hide();
                nf = new Hr();
                nf.ShowDialog();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int emId;
            try
            {
                emId = int.Parse(txtEmployId.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("Select one book first.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return; 
            }
            

            if (!checkImput())
            {
                return;
            }
            string fname = txtFirstName.Text.Trim();
            string lname = txtLastName.Text.Trim();
            string sex = cobGender.Text.Trim();
            string birthday = txtBob.Text.Trim();
            string userType = cobUserType.Text.Trim();
            string password = txtPassword.Text.Trim();
            string md5Password;

            Employee employee = ctr.getUser(emId);
            //check password, can not use check user,checking including userType
            if (password != employee.password)
            {
                md5Password = ctr.GetMd5Hash(password);
            }
            else
            {
                md5Password = employee.password;
            }
            ctr.updateUser(emId, fname, lname, sex, birthday, userType, md5Password);
            refresh();
            bindingDataGridView();
        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int emId = int.Parse(this.dataGridView.CurrentRow.Cells[0].Value.ToString());

            Employee em = ctr.getUser(emId);

            txtEmployId.Text = emId.ToString();
            txtFirstName.Text = em.firstName;
            txtLastName.Text = em.lastName;
            cobGender.Text = em.gender;
            txtBob.Text = em.dob;
            cobUserType.Text = em.jobTitle;
            txtPassword.Text = em.password;
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int emId = 0;
            try
            {
                emId = int.Parse(txtEmployId.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            DialogResult result = MessageBox.Show(
             "All related timesheet, salary and employee work type will be deleted as well, continue?",
             "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }
            try
            {
                
                ctr.deleteUser(emId);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            bindingDataGridView();
            refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtEmployId.Text.Trim().Length == 0)
            {
                MessageBox.Show("please input Employee ID");
                return;
            }

            if (!ctr.idValidator(txtEmployId.Text.Trim()))
            {
                MessageBox.Show("please input Employee right ID");
                return;
            }
      
            int emId = int.Parse(txtEmployId.Text.Trim());
            Employee em;
            try
            {
                em = ctr.getUser(emId);
            }
            catch 
            {
                MessageBox.Show("Cannot find employee ID:" + emId);
                return;
            }

            txtFirstName.Text = em.firstName;
            txtLastName.Text = em.lastName;
            cobGender.Text = em.gender;
            txtBob.Text = em.dob;
            cobUserType.Text = em.jobTitle;
            txtPassword.Text = em.password;
        }

        private void btnViewTimeSheet_Click(object sender, EventArgs e)
        {
            if (txtEmployId.Text.Trim().Length == 0)
            {
                MessageBox.Show("please input Employee ID");
                return;
            }

            if (!ctr.idValidator(txtEmployId.Text.Trim()))
            {
                MessageBox.Show("please input Employee right ID");
                return;
            }

            int emId = Convert.ToInt32(txtEmployId.Text.Trim());
            //TimeSheet tmSheet;
            try
            {
                var tsheet = ctr.getTimeSheetByUserId(emId);
                dataGridView.DataSource = tsheet;
            }
            catch 
            {

                MessageBox.Show("Cannot find timesheet for employId" + emId);
                return; ;
            }
            //lbWorkTime.Text = tmSheet.workingHours;
            //labOverTime.Text = tmSheet.overTimeHours;
        }

        private void btnViewSalary_Click(object sender, EventArgs e)
        {
            if (txtEmployId.Text.Trim().Length == 0)
            {
                MessageBox.Show("please input Employee ID");
                return;
            }

            if (!ctr.idValidator(txtEmployId.Text.Trim()))
            {
                MessageBox.Show("please input Employee right ID");
                return;
            }

            int emId = Convert.ToInt32(txtEmployId.Text.Trim());
             
            try
            {
                this.dataGridView.DataSource = ctr.getSalaryByUserId(emId);
            }
            catch
            {

                MessageBox.Show("Can find salry,with employId" + emId);
                return; ;
            }

            //labTotalSalary.Text = sal.total;
            //labSalaryＤate.Text = sal.salaryDate;
        }

        private void btnViewEmployee_Click(object sender, EventArgs e)
        {
            bindingDataGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
