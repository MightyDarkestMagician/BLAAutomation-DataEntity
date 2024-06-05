using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class AddEquipmentParametersForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public AddEquipmentParametersForm()
    {
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
    }

    private void SaveEquipmentParameters(object sender, EventArgs e)
    {
        try
        {
            // Сохранение параметров оборудования
            string equipmentModel = _equipmentModelField.Text;
            if (double.TryParse(_powerConsumptionField.Text, out double powerConsumption) &&
                double.TryParse(_noiseImmunityField.Text, out double noiseImmunity) &&
                double.TryParse(_weightField.Text, out double weight))
            {
                // Валидация данных
                if (string.IsNullOrEmpty(equipmentModel) || powerConsumption <= 0 || noiseImmunity <= 0 || weight <= 0)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохранение параметров оборудования в базу данных
                using (var context = new UavContext())
                {
                    var equipmentParameters = new EquipmentParameters
                    {
                        EquipmentModel = equipmentModel,
                        PowerConsumption = powerConsumption,
                        NoiseImmunity = noiseImmunity,
                        Weight = weight
                    };

                    context.EquipmentParameters.Add(equipmentParameters);
                    context.SaveChanges();
                }

                MessageBox.Show("Параметры оборудования успешно добавлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _equipmentModelField_Click(object sender, EventArgs e) { }
    private void _powerConsumptionField_Click(object sender, EventArgs e) { }
    private void _noiseImmunityField_Click(object sender, EventArgs e) { }
    private void _weightField_Click(object sender, EventArgs e) { }
    private void AddEquipmentParametersForm_Load(object sender, EventArgs e) { }
}
