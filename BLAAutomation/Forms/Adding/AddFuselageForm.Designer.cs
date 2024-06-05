partial class AddFuselageForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _fuselageModelField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _weightField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _totalCompartmentsField;
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
        this._fuselageModelField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._weightField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._totalCompartmentsField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._projectComboBox = new System.Windows.Forms.ComboBox();
        this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
        this.SuspendLayout();
        // 
        // _fuselageModelField
        // 
        this._fuselageModelField.Depth = 0;
        this._fuselageModelField.Hint = "Модель фюзеляжа";
        this._fuselageModelField.Location = new System.Drawing.Point(49, 95);
        this._fuselageModelField.MouseState = MaterialSkin.MouseState.HOVER;
        this._fuselageModelField.Name = "_fuselageModelField";
        this._fuselageModelField.PasswordChar = '\0';
        this._fuselageModelField.SelectedText = "";
        this._fuselageModelField.SelectionLength = 0;
        this._fuselageModelField.SelectionStart = 0;
        this._fuselageModelField.Size = new System.Drawing.Size(300, 23);
        this._fuselageModelField.TabIndex = 0;
        this._fuselageModelField.UseSystemPasswordChar = false;
        this._fuselageModelField.Click += new System.EventHandler(this._fuselageModelField_Click);
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
        // _totalCompartmentsField
        // 
        this._totalCompartmentsField.Depth = 0;
        this._totalCompartmentsField.Hint = "Общее количество отсеков";
        this._totalCompartmentsField.Location = new System.Drawing.Point(49, 195);
        this._totalCompartmentsField.MouseState = MaterialSkin.MouseState.HOVER;
        this._totalCompartmentsField.Name = "_totalCompartmentsField";
        this._totalCompartmentsField.PasswordChar = '\0';
        this._totalCompartmentsField.SelectedText = "";
        this._totalCompartmentsField.SelectionLength = 0;
        this._totalCompartmentsField.SelectionStart = 0;
        this._totalCompartmentsField.Size = new System.Drawing.Size(300, 23);
        this._totalCompartmentsField.TabIndex = 2;
        this._totalCompartmentsField.UseSystemPasswordChar = false;
        this._totalCompartmentsField.Click += new System.EventHandler(this._totalCompartmentsField_Click);
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
        this._saveButton.Click += new System.EventHandler(this.SaveFuselage);
        // 
        // AddFuselageForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 350);
        this.Controls.Add(this._fuselageModelField);
        this.Controls.Add(this._weightField);
        this.Controls.Add(this._totalCompartmentsField);
        this.Controls.Add(this._projectComboBox);
        this.Controls.Add(this._saveButton);
        this.Name = "AddFuselageForm";
        this.Text = "Добавить фюзеляж";
        this.Load += new System.EventHandler(this.AddFuselageForm_Load);
        this.ResumeLayout(false);

    }

    #endregion
}
