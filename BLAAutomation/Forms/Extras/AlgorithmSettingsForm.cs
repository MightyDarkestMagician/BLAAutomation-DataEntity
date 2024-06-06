using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using BLAAutomation.Models;
using System.Drawing;

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
                                                           .Include(p => p.UavParameters.Fuselages.Select(f => f.CompartmentsInFuselage))
                                                           .Include(p => p.UavParameters.Fuselages.Select(f => f.AntennasInFuselage))
                                                           .Include(p => p.UavParameters.Fuselages.Select(f => f.PositionsForPlacement))
                                                           .FirstOrDefault(p => p.ProjectNumber == selectedProjectNumber);
                    if (selectedProject == null)
                    {
                        MessageBox.Show("Проект не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    selectedProject.LoadRelatedData();

                    // Проверка загруженных данных
                    ShowLoadedProjectData(selectedProject);

                    // Проверка и отображение данных SelectedFuselage
                    ShowSelectedFuselageData(selectedProject.SelectedFuselage);

                    // Сохранение настроек алгоритма
                    var settings = new GeneticAlgorithm(selectedProject, populationSize, generationCount, crossoverCount, mutationCount);
                    settings.Run();

                    // Получение лучшего решения и вывод на форму
                    var bestSolution = settings.GetBestSolution();
                    DisplayBestSolution(selectedProject, bestSolution);
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

    private void ShowLoadedProjectData(Project project)
    {
        string data = $"Проект: {project.ProjectName}\nМодель БЛА: {project.UavModel}\n" +
                      $"Количество отсеков: {project.UavParameters.Compartments.Count}\n" +
                      $"Количество устройств: {project.DevicesForPlacement.Count}\n";

        foreach (var compartment in project.UavParameters.Compartments)
        {
            data += $"Отсек {compartment.CompartmentNumber}: " +
                    $"грузоподъемность {compartment.LoadCapacity}, " +
                    $"координаты ({compartment.CoordinateX}, {compartment.CoordinateY}, {compartment.CoordinateZ})\n";
        }

        foreach (var device in project.DevicesForPlacement)
        {
            data += $"Устройство {device.UavDevice.DeviceModel}: " +
                    $"вес {device.UavDevice.Weight}, " +
                    $"помехоустойчивость {device.UavDevice.NoiseImmunity}\n";
        }

        MessageBox.Show(data, "Данные проекта", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void ShowSelectedFuselageData(Fuselage fuselage)
    {
        if (fuselage == null)
        {
            MessageBox.Show("SelectedFuselage is null.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string data = $"Fuselage ID: {fuselage.Id_Fuselage}\nName: {fuselage.Name}\n" +
                      $"Antennas: {fuselage.AntennasInFuselage.Count}\n" +
                      $"Compartments: {fuselage.CompartmentsInFuselage.Count}\n";

        foreach (var compartment in fuselage.CompartmentsInFuselage)
        {
            data += $"Compartment {compartment.Id_Compartment}: " +
                    $"Coordinates ({compartment.CoordinateX}, {compartment.CoordinateY}, {compartment.CoordinateZ})\n";
        }

        MessageBox.Show(data, "SelectedFuselage Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void DisplayBestSolution(Project project, string[] bestSolution)
    {
        var resultForm = new Form
        {
            Text = "Лучшее решение",
            Size = new Size(800, 600)
        };

        // Создаем Bitmap для рисования схемы
        Bitmap bitmap = new Bitmap(resultForm.ClientSize.Width, resultForm.ClientSize.Height);
        DrawScheme drawScheme = new DrawScheme(project, bitmap);

        // Отрисовываем схему проекта
        drawScheme.DrawProject();

        // Создаем PictureBox для отображения схемы
        PictureBox pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            Image = drawScheme.Image, // исправлено
            SizeMode = PictureBoxSizeMode.AutoSize
        };

        // Добавляем GroupBoxes отсеков на форму
        foreach (var groupBox in drawScheme.CompartmentGroupBoxes)
        {
            resultForm.Controls.Add(groupBox);
        }

        // Добавляем кнопки позиций устройств на форму
        foreach (var button in drawScheme.PositionButtons)
        {
            resultForm.Controls.Add(button);
        }

        // Добавляем кнопки антенн на форму
        foreach (var button in drawScheme.AntennaButton)
        {
            resultForm.Controls.Add(button);
        }

        resultForm.Controls.Add(pictureBox);
        resultForm.ShowDialog();
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
