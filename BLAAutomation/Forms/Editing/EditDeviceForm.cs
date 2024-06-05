using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditDeviceForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private string _deviceModel;

    public EditDeviceForm(string deviceModel)
    {
        _deviceModel = deviceModel;

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
        LoadDeviceData();
    }

    private void LoadDeviceData()
    {
        using (var context = new UavContext())
        {
            var device = context.UavDevices.Find(_deviceModel);
            if (device != null)
            {
                _deviceModelField.Text = device.DeviceModel;
                _weightField.Text = device.Weight.ToString();
                _noiseImmunityField.Text = device.NoiseImmunity.ToString();
            }
        }
    }

    private void SaveDevice(object sender, EventArgs e)
    {
        try
        {
            // Сохранение устройства
            string deviceModel = _deviceModelField.Text;
            if (double.TryParse(_weightField.Text, out double weight) &&
                double.TryParse(_noiseImmunityField.Text, out double noiseImmunity))
            {
                // Валидация данных
                if (string.IsNullOrEmpty(deviceModel) || weight <= 0 || noiseImmunity <= 0)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохранение устройства в базу данных
                using (var context = new UavContext())
                {
                    var device = context.UavDevices.Find(_deviceModel);
                    if (device != null)
                    {
                        device.DeviceModel = deviceModel;
                        device.Weight = weight;
                        device.NoiseImmunity = noiseImmunity;

                        context.Entry(device).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Устройство успешно обновлено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _deviceModelField_Click(object sender, EventArgs e) { }
    private void _weightField_Click(object sender, EventArgs e) { }
    private void _noiseImmunityField_Click(object sender, EventArgs e) { }
    private void EditDeviceForm_Load(object sender, EventArgs e) { }
}
