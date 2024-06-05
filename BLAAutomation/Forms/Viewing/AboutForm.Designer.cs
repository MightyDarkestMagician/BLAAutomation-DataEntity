partial class AboutForm
{
    private MaterialSkin.Controls.MaterialLabel labelAppName;
    private MaterialSkin.Controls.MaterialLabel labelVersion;
    private MaterialSkin.Controls.MaterialLabel labelDevelopers;
    private MaterialSkin.Controls.MaterialLabel labelAppNameValue;
    private MaterialSkin.Controls.MaterialLabel labelVersionValue;
    private MaterialSkin.Controls.MaterialLabel labelDevelopersValue;

    private void InitializeComponent()
    {
        this.labelAppName = new MaterialSkin.Controls.MaterialLabel();
        this.labelVersion = new MaterialSkin.Controls.MaterialLabel();
        this.labelDevelopers = new MaterialSkin.Controls.MaterialLabel();
        this.labelAppNameValue = new MaterialSkin.Controls.MaterialLabel();
        this.labelVersionValue = new MaterialSkin.Controls.MaterialLabel();
        this.labelDevelopersValue = new MaterialSkin.Controls.MaterialLabel();
        this.SuspendLayout();
        // 
        // labelAppName
        // 
        this.labelAppName.AutoSize = true;
        this.labelAppName.Depth = 0;
        this.labelAppName.Font = new System.Drawing.Font("Roboto", 11F);
        this.labelAppName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labelAppName.Location = new System.Drawing.Point(24, 101);
        this.labelAppName.MouseState = MaterialSkin.MouseState.HOVER;
        this.labelAppName.Name = "labelAppName";
        this.labelAppName.Size = new System.Drawing.Size(133, 19);
        this.labelAppName.TabIndex = 0;
        this.labelAppName.Text = "Application Name:";
        // 
        // labelVersion
        // 
        this.labelVersion.AutoSize = true;
        this.labelVersion.Depth = 0;
        this.labelVersion.Font = new System.Drawing.Font("Roboto", 11F);
        this.labelVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labelVersion.Location = new System.Drawing.Point(24, 141);
        this.labelVersion.MouseState = MaterialSkin.MouseState.HOVER;
        this.labelVersion.Name = "labelVersion";
        this.labelVersion.Size = new System.Drawing.Size(65, 19);
        this.labelVersion.TabIndex = 1;
        this.labelVersion.Text = "Version:";
        // 
        // labelDevelopers
        // 
        this.labelDevelopers.AutoSize = true;
        this.labelDevelopers.Depth = 0;
        this.labelDevelopers.Font = new System.Drawing.Font("Roboto", 11F);
        this.labelDevelopers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labelDevelopers.Location = new System.Drawing.Point(24, 181);
        this.labelDevelopers.MouseState = MaterialSkin.MouseState.HOVER;
        this.labelDevelopers.Name = "labelDevelopers";
        this.labelDevelopers.Size = new System.Drawing.Size(88, 19);
        this.labelDevelopers.TabIndex = 2;
        this.labelDevelopers.Text = "Developers:";
        // 
        // labelAppNameValue
        // 
        this.labelAppNameValue.AutoSize = true;
        this.labelAppNameValue.Depth = 0;
        this.labelAppNameValue.Font = new System.Drawing.Font("Roboto", 11F);
        this.labelAppNameValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labelAppNameValue.Location = new System.Drawing.Point(172, 101);
        this.labelAppNameValue.MouseState = MaterialSkin.MouseState.HOVER;
        this.labelAppNameValue.Name = "labelAppNameValue";
        this.labelAppNameValue.Size = new System.Drawing.Size(115, 19);
        this.labelAppNameValue.TabIndex = 3;
        this.labelAppNameValue.Text = "BLAAutomation";
        this.labelAppNameValue.Click += new System.EventHandler(this.labelAppNameValue_Click);
        // 
        // labelVersionValue
        // 
        this.labelVersionValue.AutoSize = true;
        this.labelVersionValue.Depth = 0;
        this.labelVersionValue.Font = new System.Drawing.Font("Roboto", 11F);
        this.labelVersionValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labelVersionValue.Location = new System.Drawing.Point(172, 141);
        this.labelVersionValue.MouseState = MaterialSkin.MouseState.HOVER;
        this.labelVersionValue.Name = "labelVersionValue";
        this.labelVersionValue.Size = new System.Drawing.Size(41, 19);
        this.labelVersionValue.TabIndex = 4;
        this.labelVersionValue.Text = "1.0.0";
        // 
        // labelDevelopersValue
        // 
        this.labelDevelopersValue.AutoSize = true;
        this.labelDevelopersValue.Depth = 0;
        this.labelDevelopersValue.Font = new System.Drawing.Font("Roboto", 11F);
        this.labelDevelopersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
        this.labelDevelopersValue.Location = new System.Drawing.Point(172, 181);
        this.labelDevelopersValue.MouseState = MaterialSkin.MouseState.HOVER;
        this.labelDevelopersValue.Name = "labelDevelopersValue";
        this.labelDevelopersValue.Size = new System.Drawing.Size(117, 19);
        this.labelDevelopersValue.TabIndex = 5;
        this.labelDevelopersValue.Text = "Тимур Гилязов";
        this.labelDevelopersValue.Click += new System.EventHandler(this.labelDevelopersValue_Click);
        // 
        // AboutForm
        // 
        this.ClientSize = new System.Drawing.Size(469, 250);
        this.Controls.Add(this.labelDevelopersValue);
        this.Controls.Add(this.labelVersionValue);
        this.Controls.Add(this.labelAppNameValue);
        this.Controls.Add(this.labelDevelopers);
        this.Controls.Add(this.labelVersion);
        this.Controls.Add(this.labelAppName);
        this.Name = "AboutForm";
        this.Text = "About BLAAutomation";
        this.Load += new System.EventHandler(this.AboutForm_Load);
        this.ResumeLayout(false);
        this.PerformLayout();

    }
}