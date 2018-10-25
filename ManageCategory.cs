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
    public partial class ManageCategory : Form
    {
        private Form fr;
        private OMEGADBDataContext db;
        private Controller ctr;
        private string cType;
        private string rate;

        private void setParameters()
        {
            cType = cobCate.Text.Trim();
            rate = cobRentRate.Text.Trim();
        }
        public ManageCategory()
        {
            InitializeComponent();
        }

        private bool checkInput()
        {
            if (cobCate.Text.Trim().Length == 0)
            {
                MessageBox.Show("please select category");
                return false;
            }
            else if (cobRentRate.Text.Trim().Length == 0)
            {
                MessageBox.Show("please select rent rate");
                return false;
            }
            return true;
        }
        private void clear()
        {
            txtCateId.Clear();
            cobCate.Text = "";
            cobRentRate.Text = "";

        }
        private void bindingDataView()
        {
            var categoryList = from cate in db.Categories
                               select new
                               {
                                   CategoryID = cate.categoryId,
                                   RentCategory = cate.categoryType,
                                   RentCate = cate.rentalRate

                               };
            this.dataGridView1.DataSource = categoryList;
        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == result)
            {
                this.Hide();
                fr = new MangerLogin();
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
            int cateId;
            try
            {
                cateId = int.Parse(txtCateId.Text);
                setParameters();
                ctr.addCategory(cateId, cType, rate);
                MessageBox.Show("Add successfully");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add fail for the cause:"+ex.Message);
                return ;
            }

            clear();
            this.bindingDataView();
        }

        private void ManageCategory_Load(object sender, EventArgs e)
        {
            db = new OMEGADBDataContext();
            ctr = new Controller();

            cobCate.Items.Add("Electric");
            cobCate.Items.Add("Luxury");
            cobCate.Items.Add("HighPerformance");
            cobCate.Items.Add("Wedding");
            
            cobRentRate.Items.Add("350");
            cobRentRate.Items.Add("500");
            cobRentRate.Items.Add("800");
            cobRentRate.Items.Add("1000");

            clear();
            bindingDataView();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!checkInput())
            {
                return;
            }
            int cateId;
            try
            {
                cateId = int.Parse(txtCateId.Text.Trim());
                setParameters();
                ctr.updateCategory(cateId, cType, rate);
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

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete the category?", "Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.No == result)
            {
                return;
            }
            int cateId;
            try
            {
                cateId = int.Parse(txtCateId.Text.Trim());

                ctr.deleteCategory(cateId);
                MessageBox.Show("delete successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("delete fail for the cause:" + ex.Message);
                return;
            }

            clear();
            this.bindingDataView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int cateId;
            Category cate;
            try
            {
                cateId = int.Parse(txtCateId.Text.Trim());

                cate = ctr.getCategory(cateId);
                MessageBox.Show("search successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show("search fail for the cause:" + ex.Message);
                return;
            }

            txtCateId.Text = cate.categoryId.ToString();
            cobCate.Text = cate.categoryType;
            cobRentRate.Text = cate.rentalRate;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int cateId;
            txtCateId.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();

            try
            {
                cateId = int.Parse(txtCateId.Text.Trim());
                Category cate = ctr.getCategory(cateId);
                cobCate.Text = cate.categoryType;
                cobRentRate.Text = cate.rentalRate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("search fail for the cause:" + ex.Message);
                return;
            }


        }

    }
}
