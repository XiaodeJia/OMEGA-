namespace NOMEGA
{
    partial class MangerLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnManVehilce = new System.Windows.Forms.Button();
            this.btnManCate = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnVeReport = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnManVehilce
            // 
            this.btnManVehilce.Location = new System.Drawing.Point(25, 27);
            this.btnManVehilce.Name = "btnManVehilce";
            this.btnManVehilce.Size = new System.Drawing.Size(175, 43);
            this.btnManVehilce.TabIndex = 0;
            this.btnManVehilce.Text = "Manage Vehicle";
            this.btnManVehilce.UseVisualStyleBackColor = true;
            this.btnManVehilce.Click += new System.EventHandler(this.btnManVehilce_Click);
            // 
            // btnManCate
            // 
            this.btnManCate.Location = new System.Drawing.Point(25, 112);
            this.btnManCate.Name = "btnManCate";
            this.btnManCate.Size = new System.Drawing.Size(175, 39);
            this.btnManCate.TabIndex = 1;
            this.btnManCate.Text = "Manage Category";
            this.btnManCate.UseVisualStyleBackColor = true;
            this.btnManCate.Click += new System.EventHandler(this.btnManCate_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systmeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(342, 32);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systmeToolStripMenuItem
            // 
            this.systmeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systmeToolStripMenuItem.Name = "systmeToolStripMenuItem";
            this.systmeToolStripMenuItem.Size = new System.Drawing.Size(84, 28);
            this.systmeToolStripMenuItem.Text = "Systme";
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(162, 30);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(162, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnManVehilce);
            this.groupBox1.Controls.Add(this.btnManCate);
            this.groupBox1.Controls.Add(this.btnVeReport);
            this.groupBox1.Location = new System.Drawing.Point(54, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 251);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            // 
            // btnVeReport
            // 
            this.btnVeReport.Location = new System.Drawing.Point(25, 191);
            this.btnVeReport.Name = "btnVeReport";
            this.btnVeReport.Size = new System.Drawing.Size(175, 37);
            this.btnVeReport.TabIndex = 0;
            this.btnVeReport.Text = "Vehicle Report";
            this.btnVeReport.UseVisualStyleBackColor = true;
            this.btnVeReport.Click += new System.EventHandler(this.btnVeReport_Click);
            // 
            // MangerLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 447);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MangerLogin";
            this.Text = "MangerLogin";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnManVehilce;
        private System.Windows.Forms.Button btnManCate;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnVeReport;
    }
}