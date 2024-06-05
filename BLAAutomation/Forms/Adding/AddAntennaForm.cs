using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class AddAntennaForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public AddAntennaForm()
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

    private void SaveAntenna(object sender, EventArgs e)
    {
        try
        {
            // Сохранение антенны
            string antennaMark = _antennaMarkField.Text;
            if (double.TryParse(_frequencyRangeField.Text, out double frequencyRange) &&
                double.TryParse(_gainField.Text, out double gain) &&
                double.TryParse(_powerField.Text, out double power) &&
                double.TryParse(_impedanceField.Text, out double impedance))
            {
                // Валидация данных
                if (string.IsNullOrEmpty(antennaMark) || frequencyRange <= 0 || gain <= 0 || power <= 0 || impedance <= 0)
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

                // Сохранение антенны в базу данных
                using (var context = new UavContext())
                {
                    var antenna = new UavAntenna
                    {
                        AntennaMark = antennaMark,
                        UavModel = selectedProject.UavModel,
                        AntennaModel = antennaMark, // Assuming AntennaModel is same as AntennaMark
                        Name = antennaMark,
                        CoordinateX = 0,
                        CoordinateY = 0,
                        CoordinateZ = 0
                    };

                    var antennaParams = new AntennaParameters
                    {
                        AntennaMark = antennaMark,
                        FrequencyRange = frequencyRange,
                        Gain = gain,
                        Power = power,
                        Impedance = impedance
                    };

                    context.UavAntennas.Add(antenna);
                    context.AntennaParameters.Add(antennaParams);
                    context.SaveChanges();
                }

                MessageBox.Show("Антенна успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _projectComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
    private void _impedanceField_Click(object sender, EventArgs e) { }
    private void _powerField_Click(object sender, EventArgs e) { }
    private void _gainField_Click(object sender, EventArgs e) { }
    private void _frequencyRangeField_Click(object sender, EventArgs e) { }
    private void _antennaMarkField_Click(object sender, EventArgs e) { }
    private void AddAntennaForm_Load(object sender, EventArgs e) { }
}
