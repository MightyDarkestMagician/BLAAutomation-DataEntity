using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class AddFuselageForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public AddFuselageForm()
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

    private void SaveFuselage(object sender, EventArgs e)
    {
        try
        {
            // Сохранение фюзеляжа
            string fuselageModel = _fuselageModelField.Text;
            if (double.TryParse(_weightField.Text, out double weight) &&
                int.TryParse(_totalCompartmentsField.Text, out int totalCompartments))
            {
                // Валидация данных
                if (string.IsNullOrEmpty(fuselageModel) || weight <= 0 || totalCompartments <= 0)
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

                // Сохранение фюзеляжа в базу данных
                using (var context = new UavContext())
                {
                    var fuselage = new UavParameters
                    {
                        UavModel = fuselageModel,
                        Weight = weight,
                        TotalCompartments = totalCompartments,
                        Name = "Fuselage for " + selectedProject.ProjectName
                    };

                    context.UavParameters.Add(fuselage);
                    context.SaveChanges();
                }

                MessageBox.Show("Фюзеляж успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _totalCompartmentsField_Click(object sender, EventArgs e)
    {

    }

    private void _weightField_Click(object sender, EventArgs e)
    {

    }

    private void _fuselageModelField_Click(object sender, EventArgs e)
    {

    }

    private void AddFuselageForm_Load(object sender, EventArgs e)
    {

    }

    private void AddFuselageForm_Load_1(object sender, EventArgs e)
    {

    }
}
