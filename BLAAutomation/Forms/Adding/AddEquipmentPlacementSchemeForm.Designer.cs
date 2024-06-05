partial class AddEquipmentPlacementSchemeForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _schemeNumberField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _creationDateField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _schemeDescriptionField;
    private System.Windows.Forms.ComboBox _projectComboBox;
    private System.Windows.Forms.ComboBox _equipmentComboBox;
    private System.Windows.Forms.ComboBox _compartmentComboBox;
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
            this._schemeNumberField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._creationDateField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._schemeDescriptionField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._projectComboBox = new System.Windows.Forms.ComboBox();
            this._equipmentComboBox = new System.Windows.Forms.ComboBox();
            this._compartmentComboBox = new System.Windows.Forms.ComboBox();
            this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // _schemeNumberField
            // 
            this._schemeNumberField.Depth = 0;
            this._schemeNumberField.Hint = "Номер схемы";
            this._schemeNumberField.Location = new System.Drawing.Point(49, 78);
            this._schemeNumberField.MouseState = MaterialSkin.MouseState.HOVER;
            this._schemeNumberField.Name = "_schemeNumberField";
            this._schemeNumberField.PasswordChar = '\0';
            this._schemeNumberField.SelectedText = "";
            this._schemeNumberField.SelectionLength = 0;
            this._schemeNumberField.SelectionStart = 0;
            this._schemeNumberField.Size = new System.Drawing.Size(300, 23);
            this._schemeNumberField.TabIndex = 0;
            this._schemeNumberField.UseSystemPasswordChar = false;
            this._schemeNumberField.Click += new System.EventHandler(this._schemeNumberField_Click);
            // 
            // _creationDateField
            // 
            this._creationDateField.Depth = 0;
            this._creationDateField.Hint = "Дата создания (гггг-мм-дд)";
            this._creationDateField.Location = new System.Drawing.Point(49, 124);
            this._creationDateField.MouseState = MaterialSkin.MouseState.HOVER;
            this._creationDateField.Name = "_creationDateField";
            this._creationDateField.PasswordChar = '\0';
            this._creationDateField.SelectedText = "";
            this._creationDateField.SelectionLength = 0;
            this._creationDateField.SelectionStart = 0;
            this._creationDateField.Size = new System.Drawing.Size(300, 23);
            this._creationDateField.TabIndex = 1;
            this._creationDateField.UseSystemPasswordChar = false;
            this._creationDateField.Click += new System.EventHandler(this._creationDateField_Click);
            // 
            // _schemeDescriptionField
            // 
            this._schemeDescriptionField.Depth = 0;
            this._schemeDescriptionField.Hint = "Описание схемы";
            this._schemeDescriptionField.Location = new System.Drawing.Point(49, 170);
            this._schemeDescriptionField.MouseState = MaterialSkin.MouseState.HOVER;
            this._schemeDescriptionField.Name = "_schemeDescriptionField";
            this._schemeDescriptionField.PasswordChar = '\0';
            this._schemeDescriptionField.SelectedText = "";
            this._schemeDescriptionField.SelectionLength = 0;
            this._schemeDescriptionField.SelectionStart = 0;
            this._schemeDescriptionField.Size = new System.Drawing.Size(300, 23);
            this._schemeDescriptionField.TabIndex = 2;
            this._schemeDescriptionField.UseSystemPasswordChar = false;
            this._schemeDescriptionField.Click += new System.EventHandler(this._schemeDescriptionField_Click);
            // 
            // _projectComboBox
            // 
            this._projectComboBox.Location = new System.Drawing.Point(49, 216);
            this._projectComboBox.Name = "_projectComboBox";
            this._projectComboBox.Size = new System.Drawing.Size(300, 21);
            this._projectComboBox.TabIndex = 3;
            this._projectComboBox.SelectedIndexChanged += new System.EventHandler(this._projectComboBox_SelectedIndexChanged);
            // 
            // _equipmentComboBox
            // 
            this._equipmentComboBox.Location = new System.Drawing.Point(49, 260);
            this._equipmentComboBox.Name = "_equipmentComboBox";
            this._equipmentComboBox.Size = new System.Drawing.Size(300, 21);
            this._equipmentComboBox.TabIndex = 4;
            this._equipmentComboBox.SelectedIndexChanged += new System.EventHandler(this._equipmentComboBox_SelectedIndexChanged);
            // 
            // _compartmentComboBox
            // 
            this._compartmentComboBox.Location = new System.Drawing.Point(49, 304);
            this._compartmentComboBox.Name = "_compartmentComboBox";
            this._compartmentComboBox.Size = new System.Drawing.Size(300, 21);
            this._compartmentComboBox.TabIndex = 5;
            this._compartmentComboBox.SelectedIndexChanged += new System.EventHandler(this._compartmentComboBox_SelectedIndexChanged);
            // 
            // _saveButton
            // 
            this._saveButton.Depth = 0;
            this._saveButton.Location = new System.Drawing.Point(149, 345);
            this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Primary = true;
            this._saveButton.Size = new System.Drawing.Size(100, 23);
            this._saveButton.TabIndex = 6;
            this._saveButton.Text = "Сохранить";
            this._saveButton.Click += new System.EventHandler(this.SaveEquipmentPlacementScheme);
            // 
            // AddEquipmentPlacementSchemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this._schemeNumberField);
            this.Controls.Add(this._creationDateField);
            this.Controls.Add(this._schemeDescriptionField);
            this.Controls.Add(this._projectComboBox);
            this.Controls.Add(this._equipmentComboBox);
            this.Controls.Add(this._compartmentComboBox);
            this.Controls.Add(this._saveButton);
            this.Name = "AddEquipmentPlacementSchemeForm";
            this.Text = "Новая схема размещения оборудования";
            this.Load += new System.EventHandler(this.AddEquipmentPlacementSchemeForm_Load);
            this.ResumeLayout(false);

    }

    #endregion
}
