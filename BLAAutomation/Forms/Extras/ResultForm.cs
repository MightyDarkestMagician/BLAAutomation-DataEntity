using System;
using System.Drawing;
using System.Windows.Forms;
using BLAAutomation.Models;

public partial class ResultForm : Form
{
    private Project Project;
    private string[] BestSolution;
    private Bitmap Bitmap;
    private DrawScheme DrawScheme;

    public ResultForm(Project project, string[] bestSolution)
    {
        Project = project;
        BestSolution = bestSolution;

        Text = "Лучшее решение";
        Size = new Size(800, 600);

        // Создаем Bitmap для рисования схемы
        Bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
        DrawScheme = new DrawScheme(project, Bitmap);

        // Отрисовываем схему проекта
        DrawScheme.DrawProject();

        // Создаем PictureBox для отображения схемы
        PictureBox pictureBox = new PictureBox
        {
            Dock = DockStyle.Fill,
            Image = DrawScheme.Image,
            SizeMode = PictureBoxSizeMode.AutoSize
        };

        // Добавляем GroupBoxes отсеков на форму
        foreach (var groupBox in DrawScheme.CompartmentGroupBoxes)
        {
            Controls.Add(groupBox);
        }

        // Добавляем кнопки позиций устройств на форму
        foreach (var button in DrawScheme.PositionButtons)
        {
            Controls.Add(button);
        }

        // Добавляем кнопки антенн на форму
        foreach (var button in DrawScheme.AntennaButton)
        {
            Controls.Add(button);
        }

        Controls.Add(pictureBox);
    }


    private void ResultForm_Load(object sender, EventArgs e)
    {

    }

}
