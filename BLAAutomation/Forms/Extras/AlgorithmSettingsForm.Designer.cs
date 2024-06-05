partial class AlgorithmSettingsForm
{
    private System.ComponentModel.IContainer components = null;
    private MaterialSkin.Controls.MaterialSingleLineTextField _populationSizeField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _generationCountField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _crossoverCountField;
    private MaterialSkin.Controls.MaterialSingleLineTextField _mutationCountField;
    private MaterialSkin.Controls.MaterialRaisedButton _saveButton;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
            this._populationSizeField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._generationCountField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._crossoverCountField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._mutationCountField = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this._saveButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // _populationSizeField
            // 
            this._populationSizeField.Depth = 0;
            this._populationSizeField.Hint = "Количество особей в популяции";
            this._populationSizeField.Location = new System.Drawing.Point(49, 95);
            this._populationSizeField.MouseState = MaterialSkin.MouseState.HOVER;
            this._populationSizeField.Name = "_populationSizeField";
            this._populationSizeField.PasswordChar = '\0';
            this._populationSizeField.SelectedText = "";
            this._populationSizeField.SelectionLength = 0;
            this._populationSizeField.SelectionStart = 0;
            this._populationSizeField.Size = new System.Drawing.Size(300, 23);
            this._populationSizeField.TabIndex = 0;
            this._populationSizeField.UseSystemPasswordChar = false;
            this._populationSizeField.Click += new System.EventHandler(this._populationSizeField_Click);
            // 
            // _generationCountField
            // 
            this._generationCountField.Depth = 0;
            this._generationCountField.Hint = "Количество поколений";
            this._generationCountField.Location = new System.Drawing.Point(49, 128);
            this._generationCountField.MouseState = MaterialSkin.MouseState.HOVER;
            this._generationCountField.Name = "_generationCountField";
            this._generationCountField.PasswordChar = '\0';
            this._generationCountField.SelectedText = "";
            this._generationCountField.SelectionLength = 0;
            this._generationCountField.SelectionStart = 0;
            this._generationCountField.Size = new System.Drawing.Size(300, 23);
            this._generationCountField.TabIndex = 1;
            this._generationCountField.UseSystemPasswordChar = false;
            this._generationCountField.Click += new System.EventHandler(this._generationCountField_Click);
            // 
            // _crossoverCountField
            // 
            this._crossoverCountField.Depth = 0;
            this._crossoverCountField.Hint = "Количество особей для скрещивания";
            this._crossoverCountField.Location = new System.Drawing.Point(49, 161);
            this._crossoverCountField.MouseState = MaterialSkin.MouseState.HOVER;
            this._crossoverCountField.Name = "_crossoverCountField";
            this._crossoverCountField.PasswordChar = '\0';
            this._crossoverCountField.SelectedText = "";
            this._crossoverCountField.SelectionLength = 0;
            this._crossoverCountField.SelectionStart = 0;
            this._crossoverCountField.Size = new System.Drawing.Size(300, 23);
            this._crossoverCountField.TabIndex = 2;
            this._crossoverCountField.UseSystemPasswordChar = false;
            this._crossoverCountField.Click += new System.EventHandler(this._crossoverCountField_Click);
            // 
            // _mutationCountField
            // 
            this._mutationCountField.Depth = 0;
            this._mutationCountField.Hint = "Количество особей для мутации";
            this._mutationCountField.Location = new System.Drawing.Point(49, 194);
            this._mutationCountField.MouseState = MaterialSkin.MouseState.HOVER;
            this._mutationCountField.Name = "_mutationCountField";
            this._mutationCountField.PasswordChar = '\0';
            this._mutationCountField.SelectedText = "";
            this._mutationCountField.SelectionLength = 0;
            this._mutationCountField.SelectionStart = 0;
            this._mutationCountField.Size = new System.Drawing.Size(300, 23);
            this._mutationCountField.TabIndex = 3;
            this._mutationCountField.UseSystemPasswordChar = false;
            this._mutationCountField.Click += new System.EventHandler(this._mutationCountField_Click);
            // 
            // _saveButton
            // 
            this._saveButton.Depth = 0;
            this._saveButton.Location = new System.Drawing.Point(149, 245);
            this._saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Primary = true;
            this._saveButton.Size = new System.Drawing.Size(100, 23);
            this._saveButton.TabIndex = 4;
            this._saveButton.Text = "Сохранить";
            this._saveButton.Click += new System.EventHandler(this.SaveAlgorithmSettings);
            // 
            // AlgorithmSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this._populationSizeField);
            this.Controls.Add(this._generationCountField);
            this.Controls.Add(this._crossoverCountField);
            this.Controls.Add(this._mutationCountField);
            this.Controls.Add(this._saveButton);
            this.Name = "AlgorithmSettingsForm";
            this.Text = "Настройки алгоритма";
            this.Load += new System.EventHandler(this.AlgorithmSettingsForm_Load);
            this.ResumeLayout(false);

    }

    #endregion
}
