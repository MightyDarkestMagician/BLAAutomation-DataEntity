using BLAAutomation.Models;
using LiveCharts;
using LiveCharts.WinForms; // Используем только WinForms
using LiveCharts.Wpf;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

public partial class AntennaGainChartForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private LiveCharts.WinForms.CartesianChart _chart;

    public AntennaGainChartForm()
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
        InitializeFormComponents();
        this.Load += new EventHandler(AntennaGainChartForm_Load);
    }

    private void InitializeFormComponents()
    {
        this.Text = "График усиления антенн";
        this.Size = new System.Drawing.Size(800, 600);

        // Инициализация графика
        _chart = new LiveCharts.WinForms.CartesianChart
        {
            Dock = DockStyle.Fill
        };

        this.Controls.Add(_chart);
    }

    private void AntennaGainChartForm_Load(object sender, EventArgs e)
    {
        LoadChartData();
    }

    private void LoadChartData()
    {
        using (var context = new UavContext())
        {
            var antennas = context.AntennaParameters.ToList();
            var gains = antennas.Select(a => a.Gain).ToArray();
            var labels = antennas.Select(a => a.AntennaMark).ToArray();

            // Заполнение данных графика
            _chart.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Усиление антенны",
                        Values = new ChartValues<double>(gains)
                    }
                };

            // Настройка осей
            _chart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Антенны",
                Labels = labels
            });

            _chart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Усиление",
                LabelFormatter = value => value.ToString("N")
            });
        }
    }
}
