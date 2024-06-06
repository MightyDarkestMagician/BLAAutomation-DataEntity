using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using BLAAutomation.Models;
using System.Windows.Controls;

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

        InitializeComponent();
        LoadProjects();
    }

    private void LoadProjects()
    {
        using (var context = new UavContext())
        {
            var projects = context.Projects.ToList();
            foreach (var project in projects)
            {
                projectComboBox.Items.Add(new ComboBoxItem { Text = project.ProjectName, Value = project.ProjectNumber });
            }
        }
    }

    private void SaveAlgorithmSettings(object sender, EventArgs e)
    {
        try
        {
            if (projectComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите проект.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

                var selectedProjectNumber = ((ComboBoxItem)projectComboBox.SelectedItem).Value;
                using (var context = new UavContext())
                {
                    var selectedProject = context.Projects.Include(p => p.UavParameters)
                                                           .Include(p => p.UavParameters.Compartments)
                                                           .Include(p => p.DevicesForPlacement.Select(dfp => dfp.UavDevice))
                                                           .FirstOrDefault(p => p.ProjectNumber == selectedProjectNumber);
                    if (selectedProject == null)
                    {
                        MessageBox.Show("Проект не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Сохранение настроек алгоритма
                    var settings = new GeneticAlgorithm(selectedProject, populationSize, generationCount, crossoverCount, mutationCount);
                    settings.MainProcess();
                }

                MessageBox.Show("Настройки алгоритма успешно сохранены и процесс выполнен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    // ComboBox item class
    private class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}