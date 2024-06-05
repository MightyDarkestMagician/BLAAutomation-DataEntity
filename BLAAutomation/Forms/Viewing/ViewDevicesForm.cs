using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public partial class ViewDeviceForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public ViewDeviceForm()
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
        LoadDevices();
    }

    private void LoadDevices()
    {
        using (var context = new UavContext())
        {
            deviceDataGridView.DataSource = context.UavDevices.ToList();
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {
        string filterText = filterField.Text;
        using (var context = new UavContext())
        {
            var filteredDevices = context.UavDevices
                .Where(d => d.DeviceModel.Contains(filterText))
                .ToList();
            deviceDataGridView.DataSource = filteredDevices;
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (deviceDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = deviceDataGridView.SelectedRows[0];
            string deviceModel = (string)selectedRow.Cells["DeviceModel"].Value;

            var editDeviceForm = new EditDeviceForm(deviceModel);
            editDeviceForm.ShowDialog();
            LoadDevices();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (deviceDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = deviceDataGridView.SelectedRows[0];
            string deviceModel = (string)selectedRow.Cells["DeviceModel"].Value;

            var result = MessageBox.Show("Вы уверены, что хотите удалить это устройство?", "Удаление устройства", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var context = new UavContext())
                {
                    var device = context.UavDevices.Find(deviceModel);
                    if (device != null)
                    {
                        context.UavDevices.Remove(device);
                        context.SaveChanges();
                    }
                }
                LoadDevices();
            }
        }
    }

    private void ExportButton_Click(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var devices = context.UavDevices.ToList();
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить устройства как CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("DeviceModel,Weight,NoiseImmunity");

                    foreach (var device in devices)
                    {
                        writer.WriteLine($"{device.DeviceModel},{device.Weight},{device.NoiseImmunity}");
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
            Title = "Импортировать устройства из CSV"
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
                        if (!line.StartsWith("DeviceModel"))
                        {
                            var values = line.Split(',');
                            var device = new UavDevice
                            {
                                DeviceModel = values[0],
                                Weight = double.Parse(values[1]),
                                NoiseImmunity = double.Parse(values[2])
                            };
                            context.UavDevices.Add(device);
                        }
                    }
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно импортированы.", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDevices();
        }
    }

    private void ViewDeviceForm_Load(object sender, EventArgs e)
    {
    }
}
