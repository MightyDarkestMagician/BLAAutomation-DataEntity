using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class AddEquipmentPlacementSchemeForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;

    public AddEquipmentPlacementSchemeForm()
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
        LoadEquipmentModelsIntoComboBox();
        LoadCompartmentNumbersIntoComboBox();
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

    private void LoadEquipmentModelsIntoComboBox()
    {
        using (var context = new UavContext())
        {
            var equipmentModels = context.EquipmentParameters.ToList();
            _equipmentComboBox.DataSource = equipmentModels;
            _equipmentComboBox.DisplayMember = "EquipmentModel";
            _equipmentComboBox.ValueMember = "EquipmentModel";
        }
    }

    private void LoadCompartmentNumbersIntoComboBox()
    {
        using (var context = new UavContext())
        {
            var compartments = context.UavCompartments.ToList();
            _compartmentComboBox.DataSource = compartments;
            _compartmentComboBox.DisplayMember = "CompartmentNumber";
            _compartmentComboBox.ValueMember = "CompartmentNumber";
        }
    }

    private void SaveEquipmentPlacementScheme(object sender, EventArgs e)
    {
        try
        {
            // Сохранение схемы размещения оборудования
            if (int.TryParse(_schemeNumberField.Text, out int schemeNumber) &&
                DateTime.TryParse(_creationDateField.Text, out DateTime creationDate))
            {
                string schemeDescription = _schemeDescriptionField.Text;

                // Валидация данных
                if (schemeNumber <= 0 || string.IsNullOrEmpty(schemeDescription))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Получение выбранного проекта, оборудования и отсека
                var selectedProject = _projectComboBox.SelectedItem as Project;
                var selectedEquipmentModel = _equipmentComboBox.SelectedValue.ToString();
                var selectedCompartmentNumber = (int)_compartmentComboBox.SelectedValue;

                if (selectedProject == null || string.IsNullOrEmpty(selectedEquipmentModel) || selectedCompartmentNumber <= 0)
                {
                    MessageBox.Show("Пожалуйста, выберите корректные значения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохранение схемы размещения оборудования в базу данных
                using (var context = new UavContext())
                {
                    var equipmentPlacementScheme = new EquipmentPlacementScheme
                    {
                        SchemeNumber = schemeNumber,
                        EquipmentModel = selectedEquipmentModel,
                        CompartmentNumber = selectedCompartmentNumber,
                        SchemeDescription = schemeDescription,
                        CreationDate = creationDate,
                        UavModel = selectedProject.UavModel
                    };

                    context.EquipmentPlacementSchemes.Add(equipmentPlacementScheme);
                    context.SaveChanges();
                }

                MessageBox.Show("Схема размещения оборудования успешно добавлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void _equipmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void _compartmentComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void _schemeNumberField_Click(object sender, EventArgs e)
    {

    }

    private void _creationDateField_Click(object sender, EventArgs e)
    {

    }

    private void _schemeDescriptionField_Click(object sender, EventArgs e)
    {

    }

    private void AddEquipmentPlacementSchemeForm_Load(object sender, EventArgs e)
    {

    }
}
