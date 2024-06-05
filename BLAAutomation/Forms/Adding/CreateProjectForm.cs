using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class CreateProjectForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public CreateProjectForm()
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
        LoadUavModelsIntoComboBox();
    }

    private void LoadUavModelsIntoComboBox()
    {
        using (var context = new UavContext())
        {
            var uavModels = context.UavParameters.ToList();
            _uavModelComboBox.DataSource = uavModels;
            _uavModelComboBox.DisplayMember = "Name";
            _uavModelComboBox.ValueMember = "Model";
        }
    }

    private void SaveProject(object sender, EventArgs e)
    {
        try
        {
            // Сохранение проекта
            string projectName = _projectNameField.Text;

            // Валидация данных
            if (string.IsNullOrEmpty(projectName))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Получение выбранного БЛА
            var selectedUavModel = _uavModelComboBox.SelectedValue.ToString();

            if (string.IsNullOrEmpty(selectedUavModel))
            {
                MessageBox.Show("Пожалуйста, выберите модель БЛА.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сохранение проекта в базу данных
            using (var context = new UavContext())
            {
                var project = new Project
                {
                    ProjectName = projectName,
                    UavModel = selectedUavModel
                };

                context.Projects.Add(project);
                context.SaveChanges();
            }

            MessageBox.Show("Проект успешно создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void _uavModelComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void _projectNameField_Click(object sender, EventArgs e)
    {

    }

    private void CreateProjectForm_Load(object sender, EventArgs e)
    {

    }
}
