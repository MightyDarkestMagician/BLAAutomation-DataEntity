using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

public partial class ResultForm : MaterialForm
{
    private readonly MaterialSkinManager _materialSkinManager;
    private DrawScheme _drawScheme;

    public ResultForm(Project project)
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
        LoadResultData(project);
    }

    private void ResultForm_Load(object sender, EventArgs e)
    {
        // Отрисовка схемы после загрузки формы
        _drawScheme?.DrawProject();
    }

    private void LoadResultData(Project project)
    {
        // Инициализация DrawScheme
        Bitmap image = new Bitmap(800, 600);
        _drawScheme = new DrawScheme(project, image);

        // Добавление DrawScheme на форму
        PictureBox pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            Image = image,
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        this.Controls.Add(pictureBox);

        // Отрисовка схемы
        _drawScheme.DrawProject();
    }
}