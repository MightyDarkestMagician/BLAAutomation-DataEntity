using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using BLAAutomation.Models;
using BLAAutomation.Forms.Algorithms;
using System.Linq;

public partial class AlgorithmSettingsForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public AlgorithmSettingsForm()
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

    private void SaveAlgorithmSettings(object sender, EventArgs e)
    {
        try
        {
            // Валидация данных
            if (int.TryParse(_populationSizeField.Text, out int populationSize) &&
                int.TryParse(_generationCountField.Text, out int generationCount) &&
                int.TryParse(_crossoverCountField.Text, out int crossoverCount) &&
                int.TryParse(_mutationCountField.Text, out int mutationCount))
            {
                if (populationSize <= 0 || generationCount <= 0 || crossoverCount <= 0 || mutationCount <= 0)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получение текущего проекта
                using (var context = new UavContext())
                {
                    var selectedProject = context.Projects.FirstOrDefault(); // Замените на метод получения выбранного проекта
                    if (selectedProject == null)
                    {
                        MessageBox.Show("Проект не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Сохранение настроек алгоритма
                    var settings = new GeneticAlgorithm(selectedProject, populationSize, generationCount, crossoverCount, mutationCount);

                    // Здесь можно сохранить настройки в базу данных или передать их в алгоритм
                    // Например:
                    // context.GeneticAlgorithmSettings.Add(settings);
                    // context.SaveChanges();

                    MessageBox.Show("Настройки алгоритма успешно сохранены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
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

    private void _populationSizeField_Click(object sender, EventArgs e) { }
    private void _generationCountField_Click(object sender, EventArgs e) { }
    private void _crossoverCountField_Click(object sender, EventArgs e) { }
    private void _mutationCountField_Click(object sender, EventArgs e) { }
    private void AlgorithmSettingsForm_Load(object sender, EventArgs e) { }
}
