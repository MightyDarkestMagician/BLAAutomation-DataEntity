using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;

public partial class EditEquipmentPlacementSchemeForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private int _schemeNumber;

    public EditEquipmentPlacementSchemeForm(int schemeNumber)
    {
        _schemeNumber = schemeNumber;

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
        LoadSchemeData();
        LoadProjectsIntoComboBox();
        LoadEquipmentIntoComboBox();
        LoadCompartmentsIntoComboBox();
    }

    private void LoadSchemeData()
    {
        using (var context = new UavContext())
        {
            var scheme = context.EquipmentPlacementSchemes.Find(_schemeNumber);
            if (scheme != null)
            {
                _schemeNumberField.Text = scheme.SchemeNumber.ToString();
                _schemeDescriptionField.Text = scheme.SchemeDescription;
                _creationDateField.Value = scheme.CreationDate;
                _projectComboBox.SelectedValue = scheme.UavModel;
                _equipmentComboBox.SelectedValue = scheme.EquipmentModel;
                _compartmentComboBox.SelectedValue = scheme.CompartmentNumber;
            }
        }
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

    private void LoadEquipmentIntoComboBox()
    {
        using (var context = new UavContext())
        {
            var equipment = context.EquipmentParameters.ToList();
            _equipmentComboBox.DataSource = equipment;
            _equipmentComboBox.DisplayMember = "EquipmentModel";
            _equipmentComboBox.ValueMember = "EquipmentModel";
        }
    }

    private void LoadCompartmentsIntoComboBox()
    {
        using (var context = new UavContext())
        {
            var compartments = context.UavCompartments.ToList();
            _compartmentComboBox.DataSource = compartments;
            _compartmentComboBox.DisplayMember = "CompartmentNumber";
            _compartmentComboBox.ValueMember = "CompartmentNumber";
        }
    }

    private void SaveScheme(object sender, EventArgs e)
    {
        try
        {
            // Сохранение схемы
            if (int.TryParse(_schemeNumberField.Text, out int schemeNumber) &&
                !string.IsNullOrEmpty(_schemeDescriptionField.Text))
            {
                // Получение выбранных значений
                var selectedProject = _projectComboBox.SelectedItem as Project;
                var selectedEquipment = _equipmentComboBox.SelectedItem as EquipmentParameters;
                var selectedCompartment = _compartmentComboBox.SelectedItem as UavCompartment;

                if (selectedProject == null || selectedEquipment == null || selectedCompartment == null)
                {
                    MessageBox.Show("Пожалуйста, выберите проект, оборудование и отсек.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Сохранение схемы в базу данных
                using (var context = new UavContext())
                {
                    var scheme = context.EquipmentPlacementSchemes.Find(_schemeNumber);
                    if (scheme != null)
                    {
                        scheme.SchemeNumber = schemeNumber;
                        scheme.SchemeDescription = _schemeDescriptionField.Text;
                        scheme.CreationDate = _creationDateField.Value;
                        scheme.UavModel = selectedProject.UavModel;
                        scheme.EquipmentModel = selectedEquipment.EquipmentModel;
                        scheme.CompartmentNumber = selectedCompartment.CompartmentNumber;

                        context.Entry(scheme).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }

                MessageBox.Show("Схема успешно обновлена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля корректно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void _schemeNumberField_Click(object sender, EventArgs e) { }
    private void _schemeDescriptionField_Click(object sender, EventArgs e) { }
    private void _projectComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
    private void _equipmentComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
    private void _compartmentComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
    private void EditEquipmentPlacementSchemeForm_Load(object sender, EventArgs e) { }
}
