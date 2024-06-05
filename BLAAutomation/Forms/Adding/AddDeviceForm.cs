using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class AddDeviceForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public AddDeviceForm()
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
        LoadProjectsIntoComboBox();
    }

    private void LoadProjectsIntoComboBox()
    {
        using (var context = new UavContext())
        {
            var projects = context.Projects.ToList();
            _projectComboBox.DataSource = projects;
            _projectComboBox.DisplayMember = "ProjectName";
            _projectComboBox.ValueMember = "UavModel";
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

                // Получение выбранного проекта
                var selectedProject = _projectComboBox.SelectedItem as Project;

                if (selectedProject == null)
                {
                    MessageBox.Show("Пожалуйста, выберите проект.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохранение устройства в базу данных
                using (var context = new UavContext())
                {
                    var device = new UavDevice
                    {
                        DeviceModel = deviceModel,
                        Weight = weight,
                        NoiseImmunity = noiseImmunity
                    };

                    context.UavDevices.Add(device);
                    context.SaveChanges();
                }

                MessageBox.Show("Устройство успешно добавлено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _projectComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void _noiseImmunityField_Click(object sender, EventArgs e)
    {

    }

    private void _weightField_Click(object sender, EventArgs e)
    {

    }

    private void _deviceModelField_Click(object sender, EventArgs e)
    {

    }

    private void AddDeviceForm_Load(object sender, EventArgs e)
    {

    }
}
