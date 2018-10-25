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
    public partial class ManageVehile : Form
    {
        private Form fm;
        private Controller ctr;
        private OMEGADBDataContext db;
        private Employees employee;
        private int userId;
        private string userType;
 
        public ManageVehile()
        {
            InitializeComponent();
        }

        public ManageVehile(int id, string type)
        {
            InitializeComponent();
            userId = id;
            userType = type;
        }
        private void setVehileItemsByManager(Manager man)
        {
            //man.VehicleId = int.Parse(txtVeId.Text.Trim());
            man.Maker = cobMaker.Text.Trim();
            man.Mode = cobMode.Text.Trim();
            man.MakeTime = txtMakeTime.Text.Trim();
            man.Colour = cobColor.Text.Trim();
            man.Odometer = txtOdemeter.Text.Trim();
            man.Discription = txtDescription.Text.Trim();
            man.Categery = cobCategory.Text.Trim();
            man.Status = cobStatus.Text.Trim();
        }
        private void setVehileItemsByStaff(staff staf)
        {
            //staf.VehicleId = int.Parse(txtVeId.Text.Trim());
            staf.Odometer = txtOdemeter.Text.Trim();
            staf.Status = cobStatus.Text.Trim();
        }
        private void setVehileIdByManager(Manager man, int id)
        {
            man.VehicleId = id;
        }
        private void setVehileIdByStaff(staff staf, int id)
        {
            staf.VehicleId = id;
        }
        private Vehicle showSearchVehileResultByManager(Manager man)
        {
            return man.Vehicle;
        }
        private Vehicle showSearchVehileResultByStaff(staff staf)
        {
            return staf.Vehicle;
        }

        
        private bool checkInput()
        {
            if (!ctr.idValidator(txtVeId.Text))
            {
                MessageBox.Show("please select a vehicle");
                return false;
            }
            DateTime parseDate;
            if (!DateTime.TryParseExact(txtMakeTime.Text, "MM/dd/yyyy", null, DateTimeStyles.None, out parseDate))
            {
                MessageBox.Show("Date format is not correct, should be MM/dd/yyyy");
                return false;
            }
            return true;
        }
        private void bindDataView()
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
        private void clear()
        {
            txtVeId.Clear();
    
            txtMakeTime.Clear();

            txtOdemeter.Clear();
            txtDescription.Clear();

            cobMaker.Text = "";
            cobMode.Text = "";
            cobColor.Text = "";
            cobCategory.Text = "";
            cobStatus.Text = "";

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }

            try
            {
                //vId = ctr.addVehicle(mk, md, mt, color, odo, ds, sta);
                if (userType.Equals("Manager"))
                {
                    setVehileItemsByManager((Manager)employee);
                }
                else
                {
                    setVehileItemsByStaff((staff)employee);
                }
                
                employee.addVehicle();
                if (userType.Equals("Manager"))
                {
                    MessageBox.Show("Add Vehile successful ");
                }
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add Vehile fail, cause: " + ex.Message);
                return;
            }

            this.bindDataView();
            this.clear();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (DialogResult.Yes == result)
            {
                if (userType.Equals("Manager"))
                {
                    fm = new MangerLogin();
                }
                else
                {
                    fm = new StaffLogin();
                }
                
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

        private void ManageVehile_Load(object sender, EventArgs e)
        {
            txtMakeTime.Text = "MM/DD/YYYY";
            
            cobColor.Items.Add("Red");
            cobColor.Items.Add("Blue");
            cobColor.Items.Add("Green");
            cobColor.Items.Add("Yellow");
            cobColor.Items.Add("Black");
            cobColor.Text = "";

            cobMaker.Items.Add("BMW");
            cobMaker.Items.Add("Benz");
            cobMaker.Items.Add("Rolls-Royce");
            cobMaker.Items.Add("Bentley");
            cobMaker.Text = "";

            cobMode.Items.Add("MPV");
            cobMode.Items.Add("SUV");
            cobMode.Items.Add("SPORTS");
            cobMode.Select();

            cobStatus.Items.Add("AVAILABLE");
            cobStatus.Items.Add("UNAVAILABLE");
            cobStatus.Text = "";

            cobCategory.Items.Add("Electric");
            cobCategory.Items.Add("Luxury");
            cobCategory.Items.Add("HighPerformance");
            cobCategory.Items.Add("Wedding");
            cobCategory.Text = "";

            ctr = new Controller();
            db = new OMEGADBDataContext();
            this.bindDataView();

            //specify the employee type
            if (userType.Equals("Manager"))
            {
                employee = new Manager();
            }
            else if (userType.Equals("Staff"))
            {
                employee = new staff();
            }
  
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            DialogResult result = MessageBox.Show("Are you sure you to delete the car?", "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.No == result)
            {
                return;
            }
            if (!ctr.idValidator(txtVeId.Text.Trim()))
            {
                MessageBox.Show("Vehicle Id is wrong ");
                return;
            }

            if (userType.Equals("Staff"))
            {
                MessageBox.Show("you have no right to delete vehile ");
                return;
            }
            int vId = int.Parse(txtVeId.Text.Trim());
            try
            {
                setVehileIdByManager((Manager)employee, vId);
                employee.delVehicle();
                //ctr.deleteVehicle(vId);
                MessageBox.Show("Del vehile successful ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Del vehile fail the cause: "+ex.Message);
                return;
            }

            this.bindDataView();
            this.clear();

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }

            if (userType.Equals("Manager"))
            {
                setVehileItemsByManager((Manager)employee);
                setVehileIdByManager((Manager)employee, int.Parse(txtVeId.Text.Trim()));
            }
            else
            {
                setVehileItemsByStaff((staff)employee);
                setVehileIdByStaff((staff)employee, int.Parse(txtVeId.Text.Trim()));
            }
            try
            {
                employee.updateVehicle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update vehile fail the cause: " + ex.Message);
                return;
            }
            
            this.bindDataView();
            clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int vId;
            try
            {
                vId = int.Parse(txtVeId.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("no vehile Id " );
                return;
            }
            if (userType.Equals("Manager"))
            {
                setVehileIdByManager((Manager)employee, vId);
            }
            else
            {
                setVehileIdByStaff((staff)employee, vId);
            }
            
            try
            {
                employee.searchVehicle();
                MessageBox.Show("Sear Vehile successfully " );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sear Vehile fail cause: "+ex.Message);
                return;
            }
            Vehicle vec;
            if (userType.Equals("Manager"))
            {
                vec = showSearchVehileResultByManager((Manager)employee);
            }
            else
            {
                vec = showSearchVehileResultByStaff((staff)employee);
            }

            cobMaker.Text= vec.maker;
            cobMode.Text= vec.mode;
            txtMakeTime.Text= vec.makeTime;
            cobColor.Text=vec.color;
            txtOdemeter.Text= vec.odometer;
            txtDescription.Text= vec.description;
            cobCategory.Text = vec.categoryType;
            cobStatus.Text= vec.status;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int vecId = int.Parse(this.dataGridView.CurrentRow.Cells[0].Value.ToString());
            Vehicle vec = ctr.getVehiclebyId(vecId);

            txtVeId.Text = this.dataGridView.CurrentRow.Cells[0].Value.ToString();
            cobMaker.Text = vec.maker;
            cobMode.Text = vec.mode;
            txtMakeTime.Text = vec.makeTime.Trim();
            cobColor.Text = vec.color;
            txtOdemeter.Text = vec.odometer;
            txtDescription.Text = vec.description;
            cobCategory.Text = vec.categoryType;
            cobStatus.Text = vec.status;

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }





    
    }
}
