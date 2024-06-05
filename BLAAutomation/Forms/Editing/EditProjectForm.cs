using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditProjectForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private int _projectId;

    public EditProjectForm(int projectId)
    {
        _projectId = projectId;

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
        LoadProjectData();
    }

    private void LoadProjectData()
    {
        using (var context = new UavContext())
        {
            var project = context.Projects.Find(_projectId);
            if (project != null)
            {
                _projectNameField.Text = project.ProjectName;
                _uavModelField.Text = project.UavModel;
            }
        }
    }

    private void SaveProject(object sender, EventArgs e)
    {
        try
        {
            // Сохранение проекта
            string projectName = _projectNameField.Text;
            string uavModel = _uavModelField.Text;

            // Валидация данных
            if (string.IsNullOrEmpty(projectName) || string.IsNullOrEmpty(uavModel))
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Сохранение проекта в базу данных
            using (var context = new UavContext())
            {
                var project = context.Projects.Find(_projectId);
                if (project != null)
                {
                    project.ProjectName = projectName;
                    project.UavModel = uavModel;

                    context.Entry(project).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Проект успешно обновлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void _projectNameField_Click(object sender, EventArgs e) { }
    private void _uavModelField_Click(object sender, EventArgs e) { }
    private void EditProjectForm_Load(object sender, EventArgs e) { }
}
