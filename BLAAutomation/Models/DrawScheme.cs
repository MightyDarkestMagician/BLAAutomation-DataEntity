using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLAAutomation.Models;
using LiveCharts.Wpf.Charts.Base;

public class DrawScheme
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
    }

    public void DrawProject()
    {
        DrawCompartments();
        DrawPositions();
        DrawAntennas();
    }

    private void DrawCompartments()
    {
        if (SelectedProject.SelectedFuselage == null)
        {
            MessageBox.Show("SelectedFuselage is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (SelectedProject.SelectedFuselage.CompartmentsInFuselage == null)
        {
            MessageBox.Show("CompartmentsInFuselage is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var compartments = SelectedProject.SelectedFuselage.CompartmentsInFuselage.ToArray();
        CompartmentRectangle = new Rectangle[compartments.Length];
        CompartmentGroupBoxes = new GroupBox[compartments.Length];
        for (int i = 0; i < compartments.Length; i++)
        {
            var tempCompartment = compartments[i];
            double x = (tempCompartment.CoordinateX - tempCompartment.Length / 2.0) * ScaleCoeff;
            double y = (tempCompartment.CoordinateY - tempCompartment.Width / 2.0) * ScaleCoeff;
            double length = tempCompartment.Length* ScaleCoeff;
            double width = tempCompartment.Width * ScaleCoeff;

            CompartmentGroupBoxes[i] = new GroupBox()
            {
                Name = "groupBox_Compartment" + tempCompartment.Id_Compartment,
                Text = "Отсек №" + tempCompartment.Id_Compartment,
                Location = new Point((int)x, (int)y),
                Size = new Size((int)length, (int)width),
                BackColor = Color.White
            };
        }
    }

    private void DrawPositions()
    {
        if (SelectedProject.SelectedFuselage == null)
        {
            MessageBox.Show("SelectedFuselage is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (SelectedProject.SelectedFuselage.PositionsForPlacement == null)
        {
            MessageBox.Show("PositionsForPlacement is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var positions = SelectedProject.SelectedFuselage.PositionsForPlacement.ToArray();
        PositionButtons = new Button[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            var tempPosition = positions[i];
            double x = (tempPosition.CoordinateX - DeviceSize.Width / 2.0) * ScaleCoeff;
            double y = (tempPosition.CoordinateY - DeviceSize.Height / 2.0) * ScaleCoeff;

            PositionButtons[i] = new Button()
            {
                Name = "Button_DevicePos_" + tempPosition.Id_PositionInFuselage,
                Text = "P" + (i + 1),
                Location = new Point((int)x, (int)y),
                Size = DeviceSize,
                BackColor = Color.LightGreen
            };
        }
    }

    private void DrawAntennas()
    {
        if (SelectedProject.SelectedFuselage == null)
        {
            MessageBox.Show("SelectedFuselage is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (SelectedProject.SelectedFuselage.AntennasInFuselage == null)
        {
            MessageBox.Show("AntennasInFuselage is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var antennas = SelectedProject.SelectedFuselage.AntennasInFuselage.ToArray();
        AntennaButton = new Button[antennas.Length];
        for (int i = 0; i < antennas.Length; i++)
        {
            var tempAntenna = antennas[i];
            double x = (tempAntenna.CoordinateX - AntennaSize.Width / 2.0) * ScaleCoeff;
            double y = (tempAntenna.CoordinateY - AntennaSize.Height / 2.0) * ScaleCoeff;

            AntennaButton[i] = new Button()
            {
                Name = "Button_Antenna" + i,
                Text = "A" + (i + 1),
                Location = new Point((int)x, (int)y),
                Size = AntennaSize,
                BackColor = Color.LightSkyBlue
            };
        }
    }
}
