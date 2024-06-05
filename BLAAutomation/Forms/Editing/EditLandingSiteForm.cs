using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditLandingSiteForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private int _landingSiteId;

    public EditLandingSiteForm(int landingSiteId)
    {
        _landingSiteId = landingSiteId;

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
        LoadLandingSiteData();
    }

    private void LoadLandingSiteData()
    {
        using (var context = new UavContext())
        {
            var landingSite = context.LandingSites.Find(_landingSiteId);
            if (landingSite != null)
            {
                _landingSiteNumberField.Text = landingSite.LandingSiteNumber.ToString();
                _coordinateXField.Text = landingSite.CoordinateX.ToString();
                _coordinateYField.Text = landingSite.CoordinateY.ToString();
                _coordinateZField.Text = landingSite.CoordinateZ.ToString();
                _weightLimitField.Text = landingSite.WeightLimit.ToString();
                _projectComboBox.SelectedValue = landingSite.UavModel;
            }
        }
    }

    private void SaveLandingSite(object sender, EventArgs e)
    {
        try
        {
            // Сохранение посадочного места
            if (int.TryParse(_landingSiteNumberField.Text, out int landingSiteNumber) &&
                double.TryParse(_coordinateXField.Text, out double coordinateX) &&
                double.TryParse(_coordinateYField.Text, out double coordinateY) &&
                double.TryParse(_coordinateZField.Text, out double coordinateZ) &&
                double.TryParse(_weightLimitField.Text, out double weightLimit))
            {
                // Валидация данных
                if (landingSiteNumber <= 0 || coordinateX < 0 || coordinateY < 0 || coordinateZ < 0 || weightLimit <= 0)
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

                // Сохранение посадочного места в базу данных
                using (var context = new UavContext())
                {
                    var landingSite = context.LandingSites.Find(_landingSiteId);
                    if (landingSite != null)
                    {
                        landingSite.LandingSiteNumber = landingSiteNumber;
                        landingSite.CoordinateX = coordinateX;
                        landingSite.CoordinateY = coordinateY;
                        landingSite.CoordinateZ = coordinateZ;
                        landingSite.WeightLimit = weightLimit;
                        landingSite.UavModel = selectedProject.UavModel;

                        context.Entry(landingSite).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Посадочное место успешно обновлено.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _landingSiteNumberField_Click(object sender, EventArgs e) { }
    private void _coordinateXField_Click(object sender, EventArgs e) { }
    private void _coordinateYField_Click(object sender, EventArgs e) { }
    private void _coordinateZField_Click(object sender, EventArgs e) { }
    private void _weightLimitField_Click(object sender, EventArgs e) { }
    private void _projectComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
    private void EditLandingSiteForm_Load(object sender, EventArgs e) { }
}
