using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditEquipmentParametersForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private string _equipmentModel;

    public EditEquipmentParametersForm(string equipmentModel)
    {
        _equipmentModel = equipmentModel;

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
        LoadEquipmentData();
    }

    private void LoadEquipmentData()
    {
        using (var context = new UavContext())
        {
            var equipment = context.EquipmentParameters.Find(_equipmentModel);
            if (equipment != null)
            {
                _equipmentModelField.Text = equipment.EquipmentModel;
                _powerConsumptionField.Text = equipment.PowerConsumption.ToString();
                _noiseImmunityField.Text = equipment.NoiseImmunity.ToString();
                _weightField.Text = equipment.Weight.ToString();
            }
        }
    }

    private void SaveEquipment(object sender, EventArgs e)
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
                    var equipment = context.EquipmentParameters.Find(_equipmentModel);
                    if (equipment != null)
                    {
                        equipment.EquipmentModel = equipmentModel;
                        equipment.PowerConsumption = powerConsumption;
                        equipment.NoiseImmunity = noiseImmunity;
                        equipment.Weight = weight;

                        context.Entry(equipment).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Параметры оборудования успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    private void EditEquipmentParametersForm_Load(object sender, EventArgs e) { }
}
