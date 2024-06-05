using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditAntennaForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private string _antennaMark;

    public EditAntennaForm(string antennaMark)
    {
        _antennaMark = antennaMark;

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
        LoadAntennaData();
    }

    private void LoadAntennaData()
    {
        using (var context = new UavContext())
        {
            var antenna = context.AntennaParameters.Find(_antennaMark);
            if (antenna != null)
            {
                _antennaMarkField.Text = antenna.AntennaMark;
                _frequencyRangeField.Text = antenna.FrequencyRange.ToString();
                _gainField.Text = antenna.Gain.ToString();
                _powerField.Text = antenna.Power.ToString();
                _impedanceField.Text = antenna.Impedance.ToString();
            }
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

                // Сохранение антенны в базу данных
                using (var context = new UavContext())
                {
                    var antenna = context.AntennaParameters.Find(_antennaMark);
                    if (antenna != null)
                    {
                        antenna.AntennaMark = antennaMark;
                        antenna.FrequencyRange = frequencyRange;
                        antenna.Gain = gain;
                        antenna.Power = power;
                        antenna.Impedance = impedance;

                        context.Entry(antenna).State = EntityState.Modified;
                        context.SaveChanges();
                    }

                    var uavAntenna = context.UavAntennas.Find(_antennaMark);
                    if (uavAntenna != null)
                    {
                        uavAntenna.AntennaMark = antennaMark;

                        context.Entry(uavAntenna).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Антенна успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _antennaMarkField_Click(object sender, EventArgs e) { }
    private void _frequencyRangeField_Click(object sender, EventArgs e) { }
    private void _gainField_Click(object sender, EventArgs e) { }
    private void _powerField_Click(object sender, EventArgs e) { }
    private void _impedanceField_Click(object sender, EventArgs e) { }
    private void EditAntennaForm_Load(object sender, EventArgs e) { }
}
