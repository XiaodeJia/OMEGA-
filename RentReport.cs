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
    public partial class RentReport : Form
    {
        private Controller ctr;
        
        public RentReport()
        {
            InitializeComponent();
        }

        private void RentReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'NOMEGADataSet2.Rental' table. You can move, or remove it, as needed.
            this.RentalTableAdapter.Fill(this.NOMEGADataSet2.Rental);

            this.reportViewer1.RefreshReport();
            ctr = new Controller();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                this.Dispose();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (ctr.idValidator(txtCustomerId.Text))
            {
                int cusId = int.Parse(txtCustomerId.Text.Trim());
                Microsoft.Reporting.WinForms.ReportDataSource r = new Microsoft.Reporting.WinForms.ReportDataSource();
                r.Name = "DataSet1";
                r.Value = ctr.getRentalByCustomerID(cusId);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(r);
                this.reportViewer1.LocalReport.ReportPath = "C:/WinFormExcercice/NOMEGA/NOMEGA/rentReport.rdlc";
                this.reportViewer1.RefreshReport();
                return;
            }
            else
            {
                this.RentalTableAdapter.Fill(this.NOMEGADataSet2.Rental);

                this.reportViewer1.RefreshReport();
            }
        }

        private void txtCustomerId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
