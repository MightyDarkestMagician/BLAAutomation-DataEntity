using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class ViewEquipmentPlacementSchemesForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public ViewEquipmentPlacementSchemesForm()
    {
        // Инициализация MaterialSkinManager
        materialSkinManager = MaterialSkinManager.Instance;
        materialSkinManager.AddFormToManage(this);
        materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        materialSkinManager.ColorScheme = new ColorScheme(
            Primary.BlueGrey800, Primary.BlueGrey900,
            Primary.BlueGrey500, Accent.LightBlue200,
            TextShade.WHITE
        );

        InitializeComponent();
        LoadEquipmentPlacementSchemes();
    }

    private void LoadEquipmentPlacementSchemes()
    {
        using (var context = new UavContext())
        {
            var schemes = context.EquipmentPlacementSchemes
                .Include(s => s.EquipmentParameters)
                .Include(s => s.UavDevice)
                .Include(s => s.UavCompartment)
                .Include(s => s.UavParameters)
                .ToList()
                .Select(s => new EquipmentPlacementSchemeDto
                {
                    SchemeNumber = s.SchemeNumber,
                    EquipmentModel = s.EquipmentModel,
                    DeviceModel = s.DeviceModel,
                    CompartmentNumber = s.CompartmentNumber,
                    SchemeDescription = s.SchemeDescription,
                    CreationDate = s.CreationDate,
                    UavModel = s.UavModel
                }).ToList();

            equipmentPlacementSchemesDataGridView.DataSource = schemes;
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {
        string filterText = filterField.Text;
        using (var context = new UavContext())
        {
            var filteredSchemes = context.EquipmentPlacementSchemes
                .Where(s => s.SchemeDescription.Contains(filterText))
                .Include(s => s.EquipmentParameters)
                .Include(s => s.UavDevice)
                .Include(s => s.UavCompartment)
                .Include(s => s.UavParameters)
                .ToList()
                .Select(s => new EquipmentPlacementSchemeDto
                {
                    SchemeNumber = s.SchemeNumber,
                    EquipmentModel = s.EquipmentModel,
                    DeviceModel = s.DeviceModel,
                    CompartmentNumber = s.CompartmentNumber,
                    SchemeDescription = s.SchemeDescription,
                    CreationDate = s.CreationDate,
                    UavModel = s.UavModel
                }).ToList();

            equipmentPlacementSchemesDataGridView.DataSource = filteredSchemes;
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (equipmentPlacementSchemesDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = equipmentPlacementSchemesDataGridView.SelectedRows[0];
            int schemeNumber = (int)selectedRow.Cells["SchemeNumber"].Value;

            var editEquipmentPlacementSchemeForm = new EditEquipmentPlacementSchemeForm(schemeNumber);
            editEquipmentPlacementSchemeForm.ShowDialog();
            LoadEquipmentPlacementSchemes();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (equipmentPlacementSchemesDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = equipmentPlacementSchemesDataGridView.SelectedRows[0];
            int schemeNumber = (int)selectedRow.Cells["SchemeNumber"].Value;

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту схему размещения оборудования?", "Удаление схемы размещения оборудования", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var context = new UavContext())
                {
                    var scheme = context.EquipmentPlacementSchemes.Find(schemeNumber);
                    if (scheme != null)
                    {
                        context.EquipmentPlacementSchemes.Remove(scheme);
                        context.SaveChanges();
                    }
                }
                LoadEquipmentPlacementSchemes();
            }
        }
    }

    private void ExportButton_Click(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var schemes = context.EquipmentPlacementSchemes
                .Include(s => s.EquipmentParameters)
                .Include(s => s.UavDevice)
                .Include(s => s.UavCompartment)
                .Include(s => s.UavParameters)
                .ToList()
                .Select(s => new EquipmentPlacementSchemeDto
                {
                    SchemeNumber = s.SchemeNumber,
                    EquipmentModel = s.EquipmentModel,
                    DeviceModel = s.DeviceModel,
                    CompartmentNumber = s.CompartmentNumber,
                    SchemeDescription = s.SchemeDescription,
                    CreationDate = s.CreationDate,
                    UavModel = s.UavModel
                }).ToList();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить схемы размещения оборудования как CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("SchemeNumber,EquipmentModel,DeviceModel,CompartmentNumber,SchemeDescription,CreationDate,UavModel");

                    foreach (var scheme in schemes)
                    {
                        writer.WriteLine($"{scheme.SchemeNumber},{scheme.EquipmentModel},{scheme.DeviceModel},{scheme.CompartmentNumber},{scheme.SchemeDescription},{scheme.CreationDate},{scheme.UavModel}");
                    }
                }

                MessageBox.Show("Данные успешно экспортированы.", "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private void ImportButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog
        {
            Filter = "CSV files (*.csv)|*.csv",
            Title = "Импортировать схемы размещения оборудования из CSV"
        };

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            using (StreamReader reader = new StreamReader(openFileDialog.FileName, Encoding.UTF8))
            {
                using (var context = new UavContext())
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!line.StartsWith("SchemeNumber"))
                        {
                            var values = line.Split(',');
                            var scheme = new EquipmentPlacementScheme
                            {
                                SchemeNumber = int.Parse(values[0]),
                                EquipmentModel = values[1],
                                DeviceModel = values[2],
                                CompartmentNumber = int.Parse(values[3]),
                                SchemeDescription = values[4],
                                CreationDate = DateTime.Parse(values[5]),
                                UavModel = values[6]
                            };
                            context.EquipmentPlacementSchemes.Add(scheme);
                        }
                    }
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно импортированы.", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadEquipmentPlacementSchemes();
        }
    }

    private void ViewEquipmentPlacementSchemesForm_Load(object sender, EventArgs e)
    {
        LoadEquipmentPlacementSchemes();
    }

    private void equipmentPlacementSchemesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}

public class EquipmentPlacementSchemeDto
{
    public int SchemeNumber { get; set; }
    public string EquipmentModel { get; set; }
    public string DeviceModel { get; set; }
    public int CompartmentNumber { get; set; }
    public string SchemeDescription { get; set; }
    public DateTime CreationDate { get; set; }
    public string UavModel { get; set; }
}
