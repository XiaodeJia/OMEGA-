namespace NOMEGA
{
    partial class Hr
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
            this.btnManEmployee = new System.Windows.Forms.Button();
            this.btnManageSalary = new System.Windows.Forms.Button();
            this.btnManTmsheet = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnManEmployee
            // 
            this.btnManEmployee.Location = new System.Drawing.Point(98, 82);
            this.btnManEmployee.Name = "btnManEmployee";
            this.btnManEmployee.Size = new System.Drawing.Size(175, 35);
            this.btnManEmployee.TabIndex = 0;
            this.btnManEmployee.Text = "Manage Employee";
            this.btnManEmployee.UseVisualStyleBackColor = true;
            this.btnManEmployee.Click += new System.EventHandler(this.btnManEmployee_Click);
            // 
            // btnManageSalary
            // 
            this.btnManageSalary.Location = new System.Drawing.Point(98, 166);
            this.btnManageSalary.Name = "btnManageSalary";
            this.btnManageSalary.Size = new System.Drawing.Size(175, 35);
            this.btnManageSalary.TabIndex = 0;
            this.btnManageSalary.Text = "Manage Salary";
            this.btnManageSalary.UseVisualStyleBackColor = true;
            this.btnManageSalary.Click += new System.EventHandler(this.btnManageSalary_Click);
            // 
            // btnManTmsheet
            // 
            this.btnManTmsheet.Location = new System.Drawing.Point(98, 258);
            this.btnManTmsheet.Name = "btnManTmsheet";
            this.btnManTmsheet.Size = new System.Drawing.Size(175, 35);
            this.btnManTmsheet.TabIndex = 0;
            this.btnManTmsheet.Text = "Manage Timesheet";
            this.btnManTmsheet.UseVisualStyleBackColor = true;
            this.btnManTmsheet.Click += new System.EventHandler(this.btnManTmsheet_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(357, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logOutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(84, 28);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.logOutToolStripMenuItem.Text = "Log out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(210, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Hr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 389);
            this.Controls.Add(this.btnManTmsheet);
            this.Controls.Add(this.btnManageSalary);
            this.Controls.Add(this.btnManEmployee);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Hr";
            this.Text = "Hr";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnManEmployee;
        private System.Windows.Forms.Button btnManageSalary;
        private System.Windows.Forms.Button btnManTmsheet;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}