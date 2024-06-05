partial class EditFuselageForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _fuselageNameField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _totalCompartmentsField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _weightField;
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
        this._fuselageNameField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._totalCompartmentsField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._weightField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
        this.SuspendLayout();
        // 
        // _fuselageNameField
        // 
        this._fuselageNameField.Depth = 0;
        this._fuselageNameField.Hint = "Название фюзеляжа";
        this._fuselageNameField.Location = new System.Drawing.Point(49, 95);
        this._fuselageNameField.MouseState = MaterialSkin.MouseState.HOVER;
        this._fuselageNameField.Name = "_fuselageNameField";
        this._fuselageNameField.PasswordChar = '\0';
        this._fuselageNameField.SelectedText = "";
        this._fuselageNameField.SelectionLength = 0;
        this._fuselageNameField.SelectionStart = 0;
        this._fuselageNameField.Size = new System.Drawing.Size(300, 23);
        this._fuselageNameField.TabIndex = 0;
        this._fuselageNameField.UseSystemPasswordChar = false;
        this._fuselageNameField.Click += new System.EventHandler(this._fuselageNameField_Click);
        // 
        // _totalCompartmentsField
        // 
        this._totalCompartmentsField.Depth = 0;
        this._totalCompartmentsField.Hint = "Общее количество отсеков";
        this._totalCompartmentsField.Location = new System.Drawing.Point(49, 145);
        this._totalCompartmentsField.MouseState = MaterialSkin.MouseState.HOVER;
        this._totalCompartmentsField.Name = "_totalCompartmentsField";
        this._totalCompartmentsField.PasswordChar = '\0';
        this._totalCompartmentsField.SelectedText = "";
        this._totalCompartmentsField.SelectionLength = 0;
        this._totalCompartmentsField.SelectionStart = 0;
        this._totalCompartmentsField.Size = new System.Drawing.Size(300, 23);
        this._totalCompartmentsField.TabIndex = 1;
        this._totalCompartmentsField.UseSystemPasswordChar = false;
        this._totalCompartmentsField.Click += new System.EventHandler(this._totalCompartmentsField_Click);
        // 
        // _weightField
        // 
        this._weightField.Depth = 0;
        this._weightField.Hint = "Вес";
        this._weightField.Location = new System.Drawing.Point(49, 195);
        this._weightField.MouseState = MaterialSkin.MouseState.HOVER;
        this._weightField.Name = "_weightField";
        this._weightField.PasswordChar = '\0';
        this._weightField.SelectedText = "";
        this._weightField.SelectionLength = 0;
        this._weightField.SelectionStart = 0;
        this._weightField.Size = new System.Drawing.Size(300, 23);
        this._weightField.TabIndex = 2;
        this._weightField.UseSystemPasswordChar = false;
        this._weightField.Click += new System.EventHandler(this._weightField_Click);
        // 
        // _saveButton
        // 
        this._saveButton.Depth = 0;
        this._saveButton.Location = new System.Drawing.Point(149, 245);
        this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
        this._saveButton.Name = "_saveButton";
        this._saveButton.Primary = true;
        this._saveButton.Size = new System.Drawing.Size(100, 23);
        this._saveButton.TabIndex = 3;
        this._saveButton.Text = "Сохранить";
        this._saveButton.Click += new System.EventHandler(this.SaveFuselage);
        // 
        // EditFuselageForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 300);
        this.Controls.Add(this._fuselageNameField);
        this.Controls.Add(this._totalCompartmentsField);
        this.Controls.Add(this._weightField);
        this.Controls.Add(this._saveButton);
        this.Name = "EditFuselageForm";
        this.Text = "Редактировать фюзеляж";
        this.Load += new System.EventHandler(this.EditFuselageForm_Load);
        this.ResumeLayout(false);

    }

    #endregion
}
