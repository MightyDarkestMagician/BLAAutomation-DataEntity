using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditFuselageForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private string _fuselageModel;

    public EditFuselageForm(string fuselageModel)
    {
        _fuselageModel = fuselageModel;

        // Инициализация MaterialSkinManager
        _materialSkinManager = MaterialSkinManager.Instance;
        _materialSkinManager.AddFormToManage(this);
        _materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        _materialSkinManager.ColorScheme = new ColorScheme(
            Primary.BlueGrey800, Primary.BlueGrey900,
            Primary.BlueGrey500, Accent.LightBlue200,
            TextShade.WHITE
        );

        // Настройка элементов формы
        InitializeComponent();
        LoadFuselageData();
    }

    private void LoadFuselageData()
    {
        using (var context = new UavContext())
        {
            var fuselage = context.UavParameters.Find(_fuselageModel);
            if (fuselage != null)
            {
                _fuselageNameField.Text = fuselage.Name;
                _totalCompartmentsField.Text = fuselage.TotalCompartments.ToString();
                _weightField.Text = fuselage.Weight.ToString();
            }
        }
    }

    private void SaveFuselage(object sender, EventArgs e)
    {
        try
        {
            // Сохранение фюзеляжа
            string fuselageName = _fuselageNameField.Text;
            if (int.TryParse(_totalCompartmentsField.Text, out int totalCompartments) &&
                double.TryParse(_weightField.Text, out double weight))
            {
                // Валидация данных
                if (string.IsNullOrEmpty(fuselageName) || totalCompartments <= 0 || weight <= 0)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохранение фюзеляжа в базу данных
                using (var context = new UavContext())
                {
                    var fuselage = context.UavParameters.Find(_fuselageModel);
                    if (fuselage != null)
                    {
                        fuselage.Name = fuselageName;
                        fuselage.TotalCompartments = totalCompartments;
                        fuselage.Weight = weight;

                        context.Entry(fuselage).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Фюзеляж успешно обновлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void _fuselageNameField_Click(object sender, EventArgs e) { }
    private void _totalCompartmentsField_Click(object sender, EventArgs e) { }
    private void _weightField_Click(object sender, EventArgs e) { }
    private void EditFuselageForm_Load(object sender, EventArgs e) { }
}
