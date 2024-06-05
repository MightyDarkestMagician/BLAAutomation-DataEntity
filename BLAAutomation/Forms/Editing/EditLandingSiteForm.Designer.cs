partial class EditLandingSiteForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _landingSiteNumberField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _coordinateXField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _coordinateYField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _coordinateZField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _weightLimitField;
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
            this._landingSiteNumberField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._coordinateXField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._coordinateYField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._coordinateZField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._weightLimitField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._projectComboBox = new System.Windows.Forms.ComboBox();
            this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // _landingSiteNumberField
            // 
            this._landingSiteNumberField.Depth = 0;
            this._landingSiteNumberField.Hint = "Номер посадочного места";
            this._landingSiteNumberField.Location = new System.Drawing.Point(49, 82);
            this._landingSiteNumberField.MouseState = MaterialSkin.MouseState.HOVER;
            this._landingSiteNumberField.Name = "_landingSiteNumberField";
            this._landingSiteNumberField.PasswordChar = '\0';
            this._landingSiteNumberField.SelectedText = "";
            this._landingSiteNumberField.SelectionLength = 0;
            this._landingSiteNumberField.SelectionStart = 0;
            this._landingSiteNumberField.Size = new System.Drawing.Size(300, 23);
            this._landingSiteNumberField.TabIndex = 0;
            this._landingSiteNumberField.UseSystemPasswordChar = false;
            this._landingSiteNumberField.Click += new System.EventHandler(this._landingSiteNumberField_Click);
            // 
            // _coordinateXField
            // 
            this._coordinateXField.Depth = 0;
            this._coordinateXField.Hint = "Координата X";
            this._coordinateXField.Location = new System.Drawing.Point(49, 126);
            this._coordinateXField.MouseState = MaterialSkin.MouseState.HOVER;
            this._coordinateXField.Name = "_coordinateXField";
            this._coordinateXField.PasswordChar = '\0';
            this._coordinateXField.SelectedText = "";
            this._coordinateXField.SelectionLength = 0;
            this._coordinateXField.SelectionStart = 0;
            this._coordinateXField.Size = new System.Drawing.Size(300, 23);
            this._coordinateXField.TabIndex = 1;
            this._coordinateXField.UseSystemPasswordChar = false;
            this._coordinateXField.Click += new System.EventHandler(this._coordinateXField_Click);
            // 
            // _coordinateYField
            // 
            this._coordinateYField.Depth = 0;
            this._coordinateYField.Hint = "Координата Y";
            this._coordinateYField.Location = new System.Drawing.Point(49, 170);
            this._coordinateYField.MouseState = MaterialSkin.MouseState.HOVER;
            this._coordinateYField.Name = "_coordinateYField";
            this._coordinateYField.PasswordChar = '\0';
            this._coordinateYField.SelectedText = "";
            this._coordinateYField.SelectionLength = 0;
            this._coordinateYField.SelectionStart = 0;
            this._coordinateYField.Size = new System.Drawing.Size(300, 23);
            this._coordinateYField.TabIndex = 2;
            this._coordinateYField.UseSystemPasswordChar = false;
            this._coordinateYField.Click += new System.EventHandler(this._coordinateYField_Click);
            // 
            // _coordinateZField
            // 
            this._coordinateZField.Depth = 0;
            this._coordinateZField.Hint = "Координата Z";
            this._coordinateZField.Location = new System.Drawing.Point(49, 214);
            this._coordinateZField.MouseState = MaterialSkin.MouseState.HOVER;
            this._coordinateZField.Name = "_coordinateZField";
            this._coordinateZField.PasswordChar = '\0';
            this._coordinateZField.SelectedText = "";
            this._coordinateZField.SelectionLength = 0;
            this._coordinateZField.SelectionStart = 0;
            this._coordinateZField.Size = new System.Drawing.Size(300, 23);
            this._coordinateZField.TabIndex = 3;
            this._coordinateZField.UseSystemPasswordChar = false;
            this._coordinateZField.Click += new System.EventHandler(this._coordinateZField_Click);
            // 
            // _weightLimitField
            // 
            this._weightLimitField.Depth = 0;
            this._weightLimitField.Hint = "Весовое ограничение";
            this._weightLimitField.Location = new System.Drawing.Point(49, 258);
            this._weightLimitField.MouseState = MaterialSkin.MouseState.HOVER;
            this._weightLimitField.Name = "_weightLimitField";
            this._weightLimitField.PasswordChar = '\0';
            this._weightLimitField.SelectedText = "";
            this._weightLimitField.SelectionLength = 0;
            this._weightLimitField.SelectionStart = 0;
            this._weightLimitField.Size = new System.Drawing.Size(300, 23);
            this._weightLimitField.TabIndex = 4;
            this._weightLimitField.UseSystemPasswordChar = false;
            this._weightLimitField.Click += new System.EventHandler(this._weightLimitField_Click);
            // 
            // _projectComboBox
            // 
            this._projectComboBox.Location = new System.Drawing.Point(49, 302);
            this._projectComboBox.Name = "_projectComboBox";
            this._projectComboBox.Size = new System.Drawing.Size(300, 21);
            this._projectComboBox.TabIndex = 5;
            this._projectComboBox.SelectedIndexChanged += new System.EventHandler(this._projectComboBox_SelectedIndexChanged);
            // 
            // _saveButton
            // 
            this._saveButton.Depth = 0;
            this._saveButton.Location = new System.Drawing.Point(149, 344);
            this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Primary = true;
            this._saveButton.Size = new System.Drawing.Size(100, 23);
            this._saveButton.TabIndex = 6;
            this._saveButton.Text = "Сохранить";
            this._saveButton.Click += new System.EventHandler(this.SaveLandingSite);
            // 
            // EditLandingSiteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this._landingSiteNumberField);
            this.Controls.Add(this._coordinateXField);
            this.Controls.Add(this._coordinateYField);
            this.Controls.Add(this._coordinateZField);
            this.Controls.Add(this._weightLimitField);
            this.Controls.Add(this._projectComboBox);
            this.Controls.Add(this._saveButton);
            this.Name = "EditLandingSiteForm";
            this.Text = "Редактировать посадочное место";
            this.Load += new System.EventHandler(this.EditLandingSiteForm_Load);
            this.ResumeLayout(false);

    }

    #endregion
}
