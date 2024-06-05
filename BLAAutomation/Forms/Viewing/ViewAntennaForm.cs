using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class ViewAntennaForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public ViewAntennaForm()
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
        LoadAntennas();
    }

    private void LoadAntennas()
    {
        using (var context = new UavContext())
        {
            antennaDataGridView.DataSource = context.AntennaParameters.ToList();
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {
        string filterText = filterField.Text;
        using (var context = new UavContext())
        {
            var filteredAntennas = context.AntennaParameters
                .Where(a => a.AntennaMark.Contains(filterText))
                .ToList();
            antennaDataGridView.DataSource = filteredAntennas;
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (antennaDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = antennaDataGridView.SelectedRows[0];
            string antennaMark = (string)selectedRow.Cells["AntennaMark"].Value;

            var editAntennaForm = new EditAntennaForm(antennaMark);
            editAntennaForm.ShowDialog();
            LoadAntennas();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (antennaDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = antennaDataGridView.SelectedRows[0];
            string antennaMark = (string)selectedRow.Cells["AntennaMark"].Value;

            var result = MessageBox.Show("Вы уверены, что хотите удалить эту антенну?", "Удаление антенны", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var context = new UavContext())
                {
                    var antenna = context.AntennaParameters.Find(antennaMark);
                    if (antenna != null)
                    {
                        context.AntennaParameters.Remove(antenna);
                        context.SaveChanges();
                    }
                }
                LoadAntennas();
            }
        }
    }

    private void ExportButton_Click(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var antennas = context.AntennaParameters.ToList();
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить антенны как CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("AntennaMark,FrequencyRange,Gain,Power,Impedance");

                    foreach (var antenna in antennas)
                    {
                        writer.WriteLine($"{antenna.AntennaMark},{antenna.FrequencyRange},{antenna.Gain},{antenna.Power},{antenna.Impedance}");
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
            Title = "Импортировать антенны из CSV"
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
                        if (!line.StartsWith("AntennaMark"))
                        {
                            var values = line.Split(',');
                            var antenna = new AntennaParameters
                            {
                                AntennaMark = values[0],
                                FrequencyRange = double.Parse(values[1]),
                                Gain = double.Parse(values[2]),
                                Power = double.Parse(values[3]),
                                Impedance = double.Parse(values[4])
                            };
                            context.AntennaParameters.Add(antenna);
                        }
                    }
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно импортированы.", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadAntennas();
        }
    }

    private void ViewAntennaForm_Load(object sender, EventArgs e)
    {
    }
}
