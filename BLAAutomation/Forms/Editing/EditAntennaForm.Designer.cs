partial class EditAntennaForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _antennaMarkField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _frequencyRangeField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _gainField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _powerField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _impedanceField;
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
            this._antennaMarkField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._frequencyRangeField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._gainField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._powerField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._impedanceField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // _antennaMarkField
            // 
            this._antennaMarkField.Depth = 0;
            this._antennaMarkField.Hint = "Маркировка антенны";
            this._antennaMarkField.Location = new System.Drawing.Point(49, 80);
            this._antennaMarkField.MouseState = MaterialSkin.MouseState.HOVER;
            this._antennaMarkField.Name = "_antennaMarkField";
            this._antennaMarkField.PasswordChar = '\0';
            this._antennaMarkField.SelectedText = "";
            this._antennaMarkField.SelectionLength = 0;
            this._antennaMarkField.SelectionStart = 0;
            this._antennaMarkField.Size = new System.Drawing.Size(300, 23);
            this._antennaMarkField.TabIndex = 0;
            this._antennaMarkField.UseSystemPasswordChar = false;
            this._antennaMarkField.Click += new System.EventHandler(this._antennaMarkField_Click);
            // 
            // _frequencyRangeField
            // 
            this._frequencyRangeField.Depth = 0;
            this._frequencyRangeField.Hint = "Частотный диапазон";
            this._frequencyRangeField.Location = new System.Drawing.Point(49, 123);
            this._frequencyRangeField.MouseState = MaterialSkin.MouseState.HOVER;
            this._frequencyRangeField.Name = "_frequencyRangeField";
            this._frequencyRangeField.PasswordChar = '\0';
            this._frequencyRangeField.SelectedText = "";
            this._frequencyRangeField.SelectionLength = 0;
            this._frequencyRangeField.SelectionStart = 0;
            this._frequencyRangeField.Size = new System.Drawing.Size(300, 23);
            this._frequencyRangeField.TabIndex = 1;
            this._frequencyRangeField.UseSystemPasswordChar = false;
            this._frequencyRangeField.Click += new System.EventHandler(this._frequencyRangeField_Click);
            // 
            // _gainField
            // 
            this._gainField.Depth = 0;
            this._gainField.Hint = "Усиление";
            this._gainField.Location = new System.Drawing.Point(49, 166);
            this._gainField.MouseState = MaterialSkin.MouseState.HOVER;
            this._gainField.Name = "_gainField";
            this._gainField.PasswordChar = '\0';
            this._gainField.SelectedText = "";
            this._gainField.SelectionLength = 0;
            this._gainField.SelectionStart = 0;
            this._gainField.Size = new System.Drawing.Size(300, 23);
            this._gainField.TabIndex = 2;
            this._gainField.UseSystemPasswordChar = false;
            this._gainField.Click += new System.EventHandler(this._gainField_Click);
            // 
            // _powerField
            // 
            this._powerField.Depth = 0;
            this._powerField.Hint = "Мощность";
            this._powerField.Location = new System.Drawing.Point(49, 209);
            this._powerField.MouseState = MaterialSkin.MouseState.HOVER;
            this._powerField.Name = "_powerField";
            this._powerField.PasswordChar = '\0';
            this._powerField.SelectedText = "";
            this._powerField.SelectionLength = 0;
            this._powerField.SelectionStart = 0;
            this._powerField.Size = new System.Drawing.Size(300, 23);
            this._powerField.TabIndex = 3;
            this._powerField.UseSystemPasswordChar = false;
            this._powerField.Click += new System.EventHandler(this._powerField_Click);
            // 
            // _impedanceField
            // 
            this._impedanceField.Depth = 0;
            this._impedanceField.Hint = "Импеданс";
            this._impedanceField.Location = new System.Drawing.Point(49, 252);
            this._impedanceField.MouseState = MaterialSkin.MouseState.HOVER;
            this._impedanceField.Name = "_impedanceField";
            this._impedanceField.PasswordChar = '\0';
            this._impedanceField.SelectedText = "";
            this._impedanceField.SelectionLength = 0;
            this._impedanceField.SelectionStart = 0;
            this._impedanceField.Size = new System.Drawing.Size(300, 23);
            this._impedanceField.TabIndex = 4;
            this._impedanceField.UseSystemPasswordChar = false;
            this._impedanceField.Click += new System.EventHandler(this._impedanceField_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Depth = 0;
            this._saveButton.Location = new System.Drawing.Point(149, 295);
            this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Primary = true;
            this._saveButton.Size = new System.Drawing.Size(100, 23);
            this._saveButton.TabIndex = 5;
            this._saveButton.Text = "Сохранить";
            this._saveButton.Click += new System.EventHandler(this.SaveAntenna);
            // 
            // EditAntennaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this._antennaMarkField);
            this.Controls.Add(this._frequencyRangeField);
            this.Controls.Add(this._gainField);
            this.Controls.Add(this._powerField);
            this.Controls.Add(this._impedanceField);
            this.Controls.Add(this._saveButton);
            this.Name = "EditAntennaForm";
            this.Text = "Редактировать антенну";
            this.Load += new System.EventHandler(this.EditAntennaForm_Load);
            this.ResumeLayout(false);

    }

    #endregion
}
