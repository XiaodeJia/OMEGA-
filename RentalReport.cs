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
    public partial class RentalReport : Form
    {
        private OMEGADBDataContext db;
        private Controller ctr;
        public RentalReport()
        {
            InitializeComponent();
        }

        private void RentalReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ExpendDataSet.Expenditure' table. You can move, or remove it, as needed.
            this.ExpenditureTableAdapter.Fill(this.ExpendDataSet.Expenditure);

            this.reportViewer1.RefreshReport();

            ctr = new Controller();
            db = new OMEGADBDataContext();
        }


        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (!ctr.idValidator(txtCusId.Text.Trim()))
            {
                DialogResult result = MessageBox.Show("Are you sure you want all report?", "ALL",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes == result)
                {
                    this.ExpenditureTableAdapter.Fill(this.ExpendDataSet.Expenditure);
                    this.reportViewer1.RefreshReport();

                    return;
                }
            }

            int expId;

            try
            {
                expId = int.Parse(txtCusId.Text.Trim());
                Microsoft.Reporting.WinForms.ReportDataSource rSource = new Microsoft.Reporting.WinForms.ReportDataSource();
                rSource.Name = "RentReport";

                rSource.Value = ctr.getExpenditureByCustomerId(expId);
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rSource);
                this.reportViewer1.LocalReport.ReportPath = "C:/WinFormExcercice/NOMEGA/NOMEGA/RentalReport.rdlc";
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Report fail for the cause:" + ex.Message);
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
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
       
    }
}
