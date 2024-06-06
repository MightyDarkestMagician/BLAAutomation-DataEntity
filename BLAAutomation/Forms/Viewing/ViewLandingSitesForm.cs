using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class ViewLandingSitesForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public ViewLandingSitesForm()
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
        LoadLandingSites();
    }

    private void LoadLandingSites()
    {
        using (var context = new UavContext())
        {
            var landingSites = context.LandingSites.ToList()
                .Select(ls => new LandingSiteDto
                {
                    LandingSiteNumber = ls.LandingSiteNumber,
                    UavModel = ls.UavModel,
                    CoordinateX = ls.CoordinateX,
                    CoordinateY = ls.CoordinateY,
                    CoordinateZ = ls.CoordinateZ,
                    WeightLimit = ls.WeightLimit
                }).ToList();

            landingSiteDataGridView.DataSource = landingSites;
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {
        string filterText = filterField.Text;
        using (var context = new UavContext())
        {
            var filteredLandingSites = context.LandingSites
                .Where(ls => ls.UavModel.Contains(filterText))
                .ToList()
                .Select(ls => new LandingSiteDto
                {
                    LandingSiteNumber = ls.LandingSiteNumber,
                    UavModel = ls.UavModel,
                    CoordinateX = ls.CoordinateX,
                    CoordinateY = ls.CoordinateY,
                    CoordinateZ = ls.CoordinateZ,
                    WeightLimit = ls.WeightLimit
                }).ToList();

            landingSiteDataGridView.DataSource = filteredLandingSites;
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (landingSiteDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = landingSiteDataGridView.SelectedRows[0];
            int landingSiteNumber = (int)selectedRow.Cells["LandingSiteNumber"].Value;

            var editLandingSiteForm = new EditLandingSiteForm(landingSiteNumber);
            editLandingSiteForm.ShowDialog();
            LoadLandingSites();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (landingSiteDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = landingSiteDataGridView.SelectedRows[0];
            int landingSiteNumber = (int)selectedRow.Cells["LandingSiteNumber"].Value;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот пункт посадки?", "Удаление пункта посадки", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var context = new UavContext())
                {
                    var landingSite = context.LandingSites.Find(landingSiteNumber);
                    if (landingSite != null)
                    {
                        context.LandingSites.Remove(landingSite);
                        context.SaveChanges();
                    }
                }
                LoadLandingSites();
            }
        }
    }

    private void ExportButton_Click(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var landingSites = context.LandingSites.ToList()
                .Select(ls => new LandingSiteDto
                {
                    LandingSiteNumber = ls.LandingSiteNumber,
                    UavModel = ls.UavModel,
                    CoordinateX = ls.CoordinateX,
                    CoordinateY = ls.CoordinateY,
                    CoordinateZ = ls.CoordinateZ,
                    WeightLimit = ls.WeightLimit
                }).ToList();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить пункты посадки как CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("LandingSiteNumber,UavModel,CoordinateX,CoordinateY,CoordinateZ,WeightLimit");

                    foreach (var landingSite in landingSites)
                    {
                        writer.WriteLine($"{landingSite.LandingSiteNumber},{landingSite.UavModel},{landingSite.CoordinateX},{landingSite.CoordinateY},{landingSite.CoordinateZ},{landingSite.WeightLimit}");
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
            Title = "Импортировать пункты посадки из CSV"
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
                        if (!line.StartsWith("LandingSiteNumber"))
                        {
                            var values = line.Split(',');
                            var landingSite = new LandingSite
                            {
                                LandingSiteNumber = int.Parse(values[0]),
                                UavModel = values[1],
                                CoordinateX = double.Parse(values[2]),
                                CoordinateY = double.Parse(values[3]),
                                CoordinateZ = double.Parse(values[4]),
                                WeightLimit = double.Parse(values[5])
                            };
                            context.LandingSites.Add(landingSite);
                        }
                    }
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно импортированы.", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadLandingSites();
        }
    }

    private void ViewLandingSitesForm_Load(object sender, EventArgs e)
    {
        LoadLandingSites();
    }
}

public class LandingSiteDto
{
    public int LandingSiteNumber { get; set; }
    public string UavModel { get; set; }
    public double CoordinateX { get; set; }
    public double CoordinateY { get; set; }
    public double CoordinateZ { get; set; }
    public double WeightLimit { get; set; }
}
