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

public partial class FuselageWeightChartForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private LiveCharts.WinForms.CartesianChart _chart;

    public FuselageWeightChartForm()
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
        this.Load += new EventHandler(FuselageWeightChartForm_Load);
    }

    private void InitializeFormComponents()
    {
        this.Text = "График массы отсеков";
        this.Size = new System.Drawing.Size(800, 600);

        // Инициализация графика
        _chart = new LiveCharts.WinForms.CartesianChart
        {
            Dock = DockStyle.Fill
        };

        this.Controls.Add(_chart);
    }

    private void FuselageWeightChartForm_Load(object sender, EventArgs e)
    {
        LoadChartData();
    }

    private void LoadChartData()
    {
        using (var context = new UavContext())
        {
            var fuselages = context.UavCompartments.ToList();
            var weights = fuselages.Select(f => f.LoadCapacity).ToArray();
            var labels = fuselages.Select(f => $"Отсек {f.CompartmentNumber}").ToArray();

            // Заполнение данных графика
            _chart.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Грузоподъемность отсека",
                        Values = new ChartValues<double>(weights)
                    }
                };

            // Настройка осей
            _chart.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Отсеки",
                Labels = labels
            });

            _chart.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Грузоподъемность (кг)",
                LabelFormatter = value => value.ToString("N")
            });
        }
    }
}