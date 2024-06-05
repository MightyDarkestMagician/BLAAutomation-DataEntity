partial class AddDeviceForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _deviceModelField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _weightField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _noiseImmunityField;
    private System.Windows.Forms.ComboBox _projectComboBox;
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
        this._deviceModelField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._weightField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._noiseImmunityField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._projectComboBox = new System.Windows.Forms.ComboBox();
        this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
        this.SuspendLayout();
        // 
        // _deviceModelField
        // 
        this._deviceModelField.Depth = 0;
        this._deviceModelField.Hint = "Модель устройства";
        this._deviceModelField.Location = new System.Drawing.Point(49, 95);
        this._deviceModelField.MouseState = MaterialSkin.MouseState.HOVER;
        this._deviceModelField.Name = "_deviceModelField";
        this._deviceModelField.PasswordChar = '\0';
        this._deviceModelField.SelectedText = "";
        this._deviceModelField.SelectionLength = 0;
        this._deviceModelField.SelectionStart = 0;
        this._deviceModelField.Size = new System.Drawing.Size(300, 23);
        this._deviceModelField.TabIndex = 0;
        this._deviceModelField.UseSystemPasswordChar = false;
        this._deviceModelField.Click += new System.EventHandler(this._deviceModelField_Click);
        // 
        // _weightField
        // 
        this._weightField.Depth = 0;
        this._weightField.Hint = "Вес";
        this._weightField.Location = new System.Drawing.Point(49, 145);
        this._weightField.MouseState = MaterialSkin.MouseState.HOVER;
        this._weightField.Name = "_weightField";
        this._weightField.PasswordChar = '\0';
        this._weightField.SelectedText = "";
        this._weightField.SelectionLength = 0;
        this._weightField.SelectionStart = 0;
        this._weightField.Size = new System.Drawing.Size(300, 23);
        this._weightField.TabIndex = 1;
        this._weightField.UseSystemPasswordChar = false;
        this._weightField.Click += new System.EventHandler(this._weightField_Click);
        // 
        // _noiseImmunityField
        // 
        this._noiseImmunityField.Depth = 0;
        this._noiseImmunityField.Hint = "Восприимчивость";
        this._noiseImmunityField.Location = new System.Drawing.Point(49, 195);
        this._noiseImmunityField.MouseState = MaterialSkin.MouseState.HOVER;
        this._noiseImmunityField.Name = "_noiseImmunityField";
        this._noiseImmunityField.PasswordChar = '\0';
        this._noiseImmunityField.SelectedText = "";
        this._noiseImmunityField.SelectionLength = 0;
        this._noiseImmunityField.SelectionStart = 0;
        this._noiseImmunityField.Size = new System.Drawing.Size(300, 23);
        this._noiseImmunityField.TabIndex = 2;
        this._noiseImmunityField.UseSystemPasswordChar = false;
        this._noiseImmunityField.Click += new System.EventHandler(this._noiseImmunityField_Click);
        // 
        // _projectComboBox
        // 
        this._projectComboBox.Location = new System.Drawing.Point(49, 245);
        this._projectComboBox.Name = "_projectComboBox";
        this._projectComboBox.Size = new System.Drawing.Size(300, 21);
        this._projectComboBox.TabIndex = 3;
        this._projectComboBox.SelectedIndexChanged += new System.EventHandler(this._projectComboBox_SelectedIndexChanged);
        // 
        // _saveButton
        // 
        this._saveButton.Depth = 0;
        this._saveButton.Location = new System.Drawing.Point(149, 295);
        this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
        this._saveButton.Name = "_saveButton";
        this._saveButton.Primary = true;
        this._saveButton.Size = new System.Drawing.Size(100, 23);
        this._saveButton.TabIndex = 4;
        this._saveButton.Text = "Сохранить";
        this._saveButton.Click += new System.EventHandler(this.SaveDevice);
        // 
        // AddDeviceForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 350);
        this.Controls.Add(this._deviceModelField);
        this.Controls.Add(this._weightField);
        this.Controls.Add(this._noiseImmunityField);
        this.Controls.Add(this._projectComboBox);
        this.Controls.Add(this._saveButton);
        this.Name = "AddDeviceForm";
        this.Text = "Новое устройство";
        this.Load += new System.EventHandler(this.AddDeviceForm_Load);
        this.ResumeLayout(false);

    }

    #endregion
}
