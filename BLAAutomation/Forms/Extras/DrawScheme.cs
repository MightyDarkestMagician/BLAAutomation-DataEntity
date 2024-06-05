using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLAAutomation.Models;
using BLAAutomation.Forms.Algorithms;

public partial class DrawScheme : Form
{
    private const double CompartmentContourDepth = 4.0;
    private const double ScaleCoeff = 3.0;
    private Size DeviceSize = new Size(38, 30);
    private Size AntennaSize = new Size(29, 31);
    private Project SelectedProject;

    public Bitmap Image { get; private set; }
    internal Rectangle[] CompartmentRectangle;
    internal GroupBox[] CompartmentGroupBoxes;
    internal Button[] PositionButtons;
    internal Button[] AntennaButton;

    public DrawScheme(Project project, Bitmap image)
    {
        SelectedProject = project;
        Image = image;
        InitializeComponent();
    }

    private void DrawScheme_Load(object sender, EventArgs e)
    {
        DrawProject();
    }

    public void DrawProject()
    {
        DrawCompartments();
        DrawPositions();
        DrawAntennas();
    }

    private void DrawCompartments()
    {
        var compartments = SelectedProject.Compartments.ToArray();
        CompartmentRectangle = new Rectangle[compartments.Length];
        CompartmentGroupBoxes = new GroupBox[compartments.Length];
        for (int i = 0; i < compartments.Length; i++)
        {
            var tempCompartment = compartments[i];
            double x = (tempCompartment.CoordinateX - tempCompartment.Length / 2.0) * ScaleCoeff;
            double y = (tempCompartment.CoordinateY - tempCompartment.Width / 2.0) * ScaleCoeff;
            double length = tempCompartment.Length * ScaleCoeff;
            double width = tempCompartment.Width * ScaleCoeff;
            CompartmentGroupBoxes[i] = new GroupBox()
            {
                Name = "groupBox_Compartment" + tempCompartment.CompartmentNumber,
                Text = "Отсек №" + tempCompartment.CompartmentNumber,
                Location = new Point((int)x, (int)y),
                Size = new Size((int)length, (int)width),
                BackColor = Color.White
            };
        }
    }

    private void DrawPositions()
    {
        var positions = SelectedProject.SelectedFuselage.ToArray();
        PositionButtons = new Button[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            var tempPosition = positions[i];
            double x = (tempPosition.CoordinateX - DeviceSize.Width / 2.0) * ScaleCoeff;
            double y = (tempPosition.CoordinateY - DeviceSize.Height / 2.0) * ScaleCoeff;

            PositionButtons[i] = new Button()
            {
                Name = "Button_DevicePos_" + tempPosition.AntennaMark,
                Text = "P" + (i + 1).ToString(),
                Location = new Point((int)x, (int)y),
                Size = DeviceSize,
                BackColor = Color.LightGreen
            };
        }
    }

    private void DrawAntennas()
    {
        var antennas = SelectedProject.SelectedFuselage.ToArray();
        AntennaButton = new Button[antennas.Length];
        for (int i = 0; i < antennas.Length; i++)
        {
            var tempAntenna = antennas[i];
            double x = (tempAntenna.CoordinateX - AntennaSize.Width / 2.0) * ScaleCoeff;
            double y = (tempAntenna.CoordinateY - AntennaSize.Height / 2.0) * ScaleCoeff;

            AntennaButton[i] = new Button()
            {
                Name = "Button_Antenna" + i,
                Text = "A" + (i + 1).ToString(),
                Location = new Point((int)x, (int)y),
                Size = AntennaSize,
                BackColor = Color.LightSkyBlue
            };
        }
    }
}
