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
    public partial class VehicleReport : Form
    {
        private Controller ctr;
        public VehicleReport()
        {
            InitializeComponent();
        }

        private void VehicleReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'NOMEGADataSet1.Vehicle' table. You can move, or remove it, as needed.
            this.VehicleTableAdapter.Fill(this.NOMEGADataSet1.Vehicle);

            this.reportViewer1.RefreshReport();

            cobMaker.Items.Add("BMW");
            cobMaker.Items.Add("Benz");
            cobMaker.Items.Add("Rolls-Royce");
            cobMaker.Items.Add("Bentley");
            cobMaker.Text = "";
            ctr = new Controller();
        }

        private void btnSerch_Click(object sender, EventArgs e)
        {
            if (cobMaker.Text.Trim().Length == 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want all Vehicle report?", "ALL",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes == result)
                {
                    this.VehicleTableAdapter.Fill(this.NOMEGADataSet1.Vehicle);
                    this.reportViewer1.RefreshReport();

                    return;
                }
            }
            try
            {
                string mk = cobMaker.Text.Trim();
                Microsoft.Reporting.WinForms.ReportDataSource rVechileSource = new Microsoft.Reporting.WinForms.ReportDataSource();
                rVechileSource.Name = "VehicleDataSet1";
                rVechileSource.Value = ctr.getVehiclebyMaker(mk);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rVechileSource);
                this.reportViewer1.LocalReport.ReportPath = "C:/WinFormExcercice/NOMEGA/NOMEGA/VehileReport.rdlc";
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("report fail for the cause:" + ex.Message);
                return;
            }

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                MangerLogin me = new MangerLogin();
                me.ShowDialog();
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
    }
}
