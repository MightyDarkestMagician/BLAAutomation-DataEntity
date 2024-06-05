partial class CreateProjectForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _projectNameField;
    private System.Windows.Forms.ComboBox _uavModelComboBox;
    private MaterialSkin.Controls.MaterialRaisedButton _saveButton;

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
        this._projectNameField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._uavModelComboBox = new System.Windows.Forms.ComboBox();
        this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
        this.SuspendLayout();
        // 
        // _projectNameField
        // 
        this._projectNameField.Depth = 0;
        this._projectNameField.Hint = "Название проекта";
        this._projectNameField.Location = new System.Drawing.Point(49, 95);
        this._projectNameField.MouseState = MaterialSkin.MouseState.HOVER;
        this._projectNameField.Name = "_projectNameField";
        this._projectNameField.PasswordChar = '\0';
        this._projectNameField.SelectedText = "";
        this._projectNameField.SelectionLength = 0;
        this._projectNameField.SelectionStart = 0;
        this._projectNameField.Size = new System.Drawing.Size(300, 23);
        this._projectNameField.TabIndex = 0;
        this._projectNameField.UseSystemPasswordChar = false;
        this._projectNameField.Click += new System.EventHandler(this._projectNameField_Click);
        // 
        // _uavModelComboBox
        // 
        this._uavModelComboBox.Location = new System.Drawing.Point(49, 145);
        this._uavModelComboBox.Name = "_uavModelComboBox";
        this._uavModelComboBox.Size = new System.Drawing.Size(300, 21);
        this._uavModelComboBox.TabIndex = 1;
        this._uavModelComboBox.SelectedIndexChanged += new System.EventHandler(this._uavModelComboBox_SelectedIndexChanged);
        // 
        // _saveButton
        // 
        this._saveButton.Depth = 0;
        this._saveButton.Location = new System.Drawing.Point(149, 195);
        this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
        this._saveButton.Name = "_saveButton";
        this._saveButton.Primary = true;
        this._saveButton.Size = new System.Drawing.Size(100, 23);
        this._saveButton.TabIndex = 2;
        this._saveButton.Text = "Сохранить";
        this._saveButton.Click += new System.EventHandler(this.SaveProject);
        // 
        // CreateProjectForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 250);
        this.Controls.Add(this._projectNameField);
        this.Controls.Add(this._uavModelComboBox);
        this.Controls.Add(this._saveButton);
        this.Name = "CreateProjectForm";
        this.Text = "Создать проект";
        this.Load += new System.EventHandler(this.CreateProjectForm_Load);
        this.ResumeLayout(false);

    }

    #endregion
}
