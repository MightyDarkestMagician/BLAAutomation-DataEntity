partial class ViewAntennaForm
{
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField filterField;
    private MaterialSkin.Controls.MaterialRaisedButton filterButton;
    private MaterialSkin.Controls.MaterialRaisedButton editButton;
    private MaterialSkin.Controls.MaterialRaisedButton deleteButton;
    private MaterialSkin.Controls.MaterialRaisedButton exportButton;
    private MaterialSkin.Controls.MaterialRaisedButton importButton;
    private System.Windows.Forms.DataGridView antennaDataGridView;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.filterField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.filterButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.editButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.deleteButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.exportButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.importButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.antennaDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.antennaDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // filterField
            // 
            this.filterField.Depth = 0;
            this.filterField.Hint = "Маркировка антенны";
            this.filterField.Location = new System.Drawing.Point(50, 20);
            this.filterField.MouseState = MaterialSkin.MouseState.HOVER;
            this.filterField.Name = "filterField";
            this.filterField.PasswordChar = '\0';
            this.filterField.SelectedText = "";
            this.filterField.SelectionLength = 0;
            this.filterField.SelectionStart = 0;
            this.filterField.Size = new System.Drawing.Size(300, 23);
            this.filterField.TabIndex = 0;
            this.filterField.UseSystemPasswordChar = false;
            // 
            // filterButton
            // 
            this.filterButton.Depth = 0;
            this.filterButton.Location = new System.Drawing.Point(370, 20);
            this.filterButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.filterButton.Name = "filterButton";
            this.filterButton.Primary = true;
            this.filterButton.Size = new System.Drawing.Size(100, 23);
            this.filterButton.TabIndex = 1;
            this.filterButton.Text = "Фильтр";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // editButton
            // 
            this.editButton.Depth = 0;
            this.editButton.Location = new System.Drawing.Point(49, 375);
            this.editButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.editButton.Name = "editButton";
            this.editButton.Primary = true;
            this.editButton.Size = new System.Drawing.Size(130, 36);
            this.editButton.TabIndex = 2;
            this.editButton.Text = "Редактировать";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Depth = 0;
            this.deleteButton.Location = new System.Drawing.Point(199, 375);
            this.deleteButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Primary = true;
            this.deleteButton.Size = new System.Drawing.Size(130, 36);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Depth = 0;
            this.exportButton.Location = new System.Drawing.Point(349, 375);
            this.exportButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.exportButton.Name = "exportButton";
            this.exportButton.Primary = true;
            this.exportButton.Size = new System.Drawing.Size(180, 36);
            this.exportButton.TabIndex = 4;
            this.exportButton.Text = "Экспорт в CSV";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // importButton
            // 
            this.importButton.Depth = 0;
            this.importButton.Location = new System.Drawing.Point(554, 375);
            this.importButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.importButton.Name = "importButton";
            this.importButton.Primary = true;
            this.importButton.Size = new System.Drawing.Size(180, 36);
            this.importButton.TabIndex = 5;
            this.importButton.Text = "Импорт из CSV";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // antennaDataGridView
            // 
            this.antennaDataGridView.AllowUserToAddRows = false;
            this.antennaDataGridView.AllowUserToDeleteRows = false;
            this.antennaDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.antennaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.antennaDataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.antennaDataGridView.Location = new System.Drawing.Point(0, 0);
            this.antennaDataGridView.Name = "antennaDataGridView";
            this.antennaDataGridView.ReadOnly = true;
            this.antennaDataGridView.Size = new System.Drawing.Size(800, 300);
            this.antennaDataGridView.TabIndex = 6;
            this.antennaDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.antennaDataGridView_CellContentClick);
            // 
            // ViewAntennaForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.antennaDataGridView);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.filterField);
            this.Name = "ViewAntennaForm";
            this.Load += new System.EventHandler(this.ViewAntennaForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.antennaDataGridView)).EndInit();
            this.ResumeLayout(false);

    }
}