partial class AddEquipmentParametersForm
{
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _equipmentModelField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _powerConsumptionField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _noiseImmunityField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _weightField;
    private MaterialSkin.Controls.MaterialRaisedButton _saveButton;

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
        this._equipmentModelField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._powerConsumptionField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._noiseImmunityField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._weightField = new MaterialSkin.Controls.MaterialSingleLineTextField();
        this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
        this.SuspendLayout();

        // 
        // _equipmentModelField
        // 
        this._equipmentModelField.Depth = 0;
        this._equipmentModelField.Hint = "Модель оборудования";
        this._equipmentModelField.Location = new System.Drawing.Point(50, 95);
        this._equipmentModelField.MouseState = MaterialSkin.MouseState.HOVER;
        this._equipmentModelField.Name = "_equipmentModelField";
        this._equipmentModelField.PasswordChar = '\0';
        this._equipmentModelField.SelectedText = "";
        this._equipmentModelField.SelectionLength = 0;
        this._equipmentModelField.SelectionStart = 0;
        this._equipmentModelField.Size = new System.Drawing.Size(300, 23);
        this._equipmentModelField.TabIndex = 0;
        this._equipmentModelField.UseSystemPasswordChar = false;
        this._equipmentModelField.Click += new System.EventHandler(this._equipmentModelField_Click);

        // 
        // _powerConsumptionField
        // 
        this._powerConsumptionField.Depth = 0;
        this._powerConsumptionField.Hint = "Потребляемая мощность";
        this._powerConsumptionField.Location = new System.Drawing.Point(50, 145);
        this._powerConsumptionField.MouseState = MaterialSkin.MouseState.HOVER;
        this._powerConsumptionField.Name = "_powerConsumptionField";
        this._powerConsumptionField.PasswordChar = '\0';
        this._powerConsumptionField.SelectedText = "";
        this._powerConsumptionField.SelectionLength = 0;
        this._powerConsumptionField.SelectionStart = 0;
        this._powerConsumptionField.Size = new System.Drawing.Size(300, 23);
        this._powerConsumptionField.TabIndex = 1;
        this._powerConsumptionField.UseSystemPasswordChar = false;
        this._powerConsumptionField.Click += new System.EventHandler(this._powerConsumptionField_Click);

        // 
        // _noiseImmunityField
        // 
        this._noiseImmunityField.Depth = 0;
        this._noiseImmunityField.Hint = "Уровень помехоустойчивости";
        this._noiseImmunityField.Location = new System.Drawing.Point(50, 195);
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
        // _weightField
        // 
        this._weightField.Depth = 0;
        this._weightField.Hint = "Вес";
        this._weightField.Location = new System.Drawing.Point(50, 245);
        this._weightField.MouseState = MaterialSkin.MouseState.HOVER;
        this._weightField.Name = "_weightField";
        this._weightField.PasswordChar = '\0';
        this._weightField.SelectedText = "";
        this._weightField.SelectionLength = 0;
        this._weightField.SelectionStart = 0;
        this._weightField.Size = new System.Drawing.Size(300, 23);
        this._weightField.TabIndex = 3;
        this._weightField.UseSystemPasswordChar = false;
        this._weightField.Click += new System.EventHandler(this._weightField_Click);

        // 
        // _saveButton
        // 
        this._saveButton.Depth = 0;
        this._saveButton.Location = new System.Drawing.Point(150, 300);
        this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
        this._saveButton.Name = "_saveButton";
        this._saveButton.Primary = true;
        this._saveButton.Size = new System.Drawing.Size(100, 23);
        this._saveButton.TabIndex = 4;
        this._saveButton.Text = "Сохранить";
        this._saveButton.Click += new System.EventHandler(this.SaveEquipmentParameters);

        // 
        // AddEquipmentParametersForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(400, 350);
        this.Controls.Add(this._equipmentModelField);
        this.Controls.Add(this._powerConsumptionField);
        this.Controls.Add(this._noiseImmunityField);
        this.Controls.Add(this._weightField);
        this.Controls.Add(this._saveButton);
        this.Name = "AddEquipmentParametersForm";
        this.Text = "Новое оборудование";
        this.Load += new System.EventHandler(this.AddEquipmentParametersForm_Load);
        this.ResumeLayout(false);
    }
}
