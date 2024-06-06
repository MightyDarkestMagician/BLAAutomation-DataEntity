using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class ViewFuselageForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public ViewFuselageForm()
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
        LoadFuselages();
    }

    private void LoadFuselages()
    {
        using (var context = new UavContext())
        {
            var fuselages = context.UavParameters.ToList()
                .Select(f => new FuselageDto
                {
                    UavModel = f.UavModel,
                    Name = f.Name,
                    TotalCompartments = f.TotalCompartments,
                    Weight = f.Weight
                }).ToList();

            fuselageDataGridView.DataSource = fuselages;
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {
        string filterText = filterField.Text;
        using (var context = new UavContext())
        {
            var filteredFuselages = context.UavParameters
                .Where(f => f.Name.Contains(filterText))
                .ToList()
                .Select(f => new FuselageDto
                {
                    UavModel = f.UavModel,
                    Name = f.Name,
                    TotalCompartments = f.TotalCompartments,
                    Weight = f.Weight
                }).ToList();

            fuselageDataGridView.DataSource = filteredFuselages;
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (fuselageDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = fuselageDataGridView.SelectedRows[0];
            string fuselageModel = (string)selectedRow.Cells["UavModel"].Value;

            var editFuselageForm = new EditFuselageForm(fuselageModel);
            editFuselageForm.ShowDialog();
            LoadFuselages();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (fuselageDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = fuselageDataGridView.SelectedRows[0];
            string fuselageModel = (string)selectedRow.Cells["UavModel"].Value;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот фюзеляж?", "Удаление фюзеляжа", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var context = new UavContext())
                {
                    var fuselage = context.UavParameters.Find(fuselageModel);
                    if (fuselage != null)
                    {
                        context.UavParameters.Remove(fuselage);
                        context.SaveChanges();
                    }
                }
                LoadFuselages();
            }
        }
    }

    private void ExportButton_Click(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var fuselages = context.UavParameters.ToList()
                .Select(f => new FuselageDto
                {
                    UavModel = f.UavModel,
                    Name = f.Name,
                    TotalCompartments = f.TotalCompartments,
                    Weight = f.Weight
                }).ToList();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить фюзеляжи как CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("UavModel,Name,TotalCompartments,Weight");

                    foreach (var fuselage in fuselages)
                    {
                        writer.WriteLine($"{fuselage.UavModel},{fuselage.Name},{fuselage.TotalCompartments},{fuselage.Weight}");
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
            Title = "Импортировать фюзеляжи из CSV"
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
                        if (!line.StartsWith("UavModel"))
                        {
                            var values = line.Split(',');
                            var fuselage = new UavParameters
                            {
                                UavModel = values[0],
                                Name = values[1],
                                TotalCompartments = int.Parse(values[2]),
                                Weight = double.Parse(values[3])
                            };
                            context.UavParameters.Add(fuselage);
                        }
                    }
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно импортированы.", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadFuselages();
        }
    }

    private void ViewFuselageForm_Load(object sender, EventArgs e)
    {
        LoadFuselages();
    }

    private void fuselageDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}

public class FuselageDto
{
    public string UavModel { get; set; }
    public string Name { get; set; }
    public int TotalCompartments { get; set; }
    public double Weight { get; set; }
}