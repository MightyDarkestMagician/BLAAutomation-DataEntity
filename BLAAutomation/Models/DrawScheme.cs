using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BLAAutomation.Models
{
    public class DrawScheme
    {
        private const double CompartmentContourDepth = 4.0;
        private const double ScaleCoeff = 3.0;
        private Size DeviceSize = new Size(38, 30);
        private Size AntennaSize = new Size(29, 31);
        private Project SelectedProject;

        private Bitmap Image;
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
            CompartmentRectangle = new Rectangle[SelectedProject.Compartments.Count];
            CompartmentGroupBoxes = new GroupBox[SelectedProject.Compartments.Count];
            for (int i = 0; i < SelectedProject.Compartments.Count; i++)
            {
                var tempCompartment = SelectedProject.Compartments.ElementAt(i);
                double x = (tempCompartment.CoordinateX - tempCompartment.Length / 2.0) * ScaleCoeff;
                double y = (tempCompartment.CoordinateY - tempCompartment.Width / 2.0) * ScaleCoeff;
                double length = tempCompartment.Length * ScaleCoeff;
                double width = tempCompartment.Width * ScaleCoeff;
                CompartmentGroupBoxes[i] = new GroupBox()
                {
                    Name = "groupBox_Compartment" + tempCompartment.CompartmentNumber.ToString(),
                    Text = "Отсек №" + tempCompartment.CompartmentNumber.ToString(),
                    Location = new Point((int)(x), (int)(y)),
                    Size = new Size((int)(length), (int)(width)),
                    BackColor = Color.White
                };
            }
        }

        private void DrawPositions()
        {
            PositionButtons = new Button[SelectedProject.SelectedFuselage.PositionsForPlacement.Count];
            for (int i = 0; i < SelectedProject.SelectedFuselage.PositionsForPlacement.Count; i++)
            {
                var tempPosition = SelectedProject.SelectedFuselage.PositionsForPlacement.ElementAt(i);
                double x = (tempPosition.CoordinateX - DeviceSize.Width / 2.0) * ScaleCoeff;
                double y = (tempPosition.CoordinateY - DeviceSize.Height / 2.0) * ScaleCoeff;

                PositionButtons[i] = new Button()
                {
                    Name = "Button_DevicePos_" + tempPosition.Id_PositionInFuselage,
                    Text = "P" + (i + 1).ToString(),
                    Location = new Point((int)(x), (int)(y)),
                    Size = DeviceSize,
                    BackColor = Color.LightGreen
                };
            }
        }

        private void DrawAntennas()
        {
            AntennaButton = new Button[SelectedProject.SelectedFuselage.AntennasInFuselage.Count];
            for (int i = 0; i < SelectedProject.SelectedFuselage.AntennasInFuselage.Count; i++)
            {
                var tempAntenna = SelectedProject.SelectedFuselage.AntennasInFuselage.ElementAt(i);
                double x = (tempAntenna.CoordinateX - AntennaSize.Width / 2.0) * ScaleCoeff;
                double y = (tempAntenna.CoordinateY - AntennaSize.Height / 2.0) * ScaleCoeff;

                AntennaButton[i] = new Button()
                {
                    Name = "Button_Antenna" + i,
                    Text = "A" + (i + 1).ToString(),
                    Location = new Point((int)(x), (int)(y)),
                    Size = AntennaSize,
                    BackColor = Color.LightSkyBlue
                };
            }
        }

        public void DisplayOnForm(Form form)
        {
            PictureBox pictureBox = new PictureBox
            {
                Image = Image,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            form.Controls.Add(pictureBox);
            foreach (var groupBox in CompartmentGroupBoxes)
            {
                form.Controls.Add(groupBox);
            }
            foreach (var button in PositionButtons)
            {
                form.Controls.Add(button);
            }
            foreach (var button in AntennaButton)
            {
                form.Controls.Add(button);
            }
        }
    }
}
