﻿using BLAAutomation.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using System.Linq;

public partial class MainForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public MainForm()
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

        // Настройка элементов формы
        InitializeComponent();
        CreateMenu();
    }

    private void CreateMenu()
    {
        var menuStrip = new MenuStrip();

        // Проект
        var projectMenuItem = new ToolStripMenuItem("Проект");
        var viewProjectsMenuItem = new ToolStripMenuItem("Просмотреть проекты", null, ViewProjectsMenuItem_Click);
        var createProjectMenuItem = new ToolStripMenuItem("Создать проект", null, CreateProjectMenuItem_Click);
        projectMenuItem.DropDownItems.Add(viewProjectsMenuItem);
        projectMenuItem.DropDownItems.Add(createProjectMenuItem);

        // Данные
        var dataMenuItem = new ToolStripMenuItem("Данные");
        var addAntennaMenuItem = new ToolStripMenuItem("Добавить антенну", null, AddAntennaMenuItem_Click);
        var addDeviceMenuItem = new ToolStripMenuItem("Добавить устройство", null, AddDeviceMenuItem_Click);
        var addFuselageMenuItem = new ToolStripMenuItem("Добавить фюзеляж", null, AddFuselageMenuItem_Click);
        var addLandingSiteMenuItem = new ToolStripMenuItem("Добавить место посадки", null, AddLandingSiteMenuItem_Click);
        var addEquipmentSchemeMenuItem = new ToolStripMenuItem("Добавить схему размещения", null, AddEquipmentSchemeMenuItem_Click);
        dataMenuItem.DropDownItems.Add(addAntennaMenuItem);
        dataMenuItem.DropDownItems.Add(addDeviceMenuItem);
        dataMenuItem.DropDownItems.Add(addFuselageMenuItem);
        dataMenuItem.DropDownItems.Add(addLandingSiteMenuItem);
        dataMenuItem.DropDownItems.Add(addEquipmentSchemeMenuItem);

        // Алгоритм
        var algorithmMenuItem = new ToolStripMenuItem("Алгоритм", null, AlgorithmMenuItem_Click);

        // О программе
        var aboutMenuItem = new ToolStripMenuItem("О программе", null, AboutMenuItem_Click);

        menuStrip.Items.AddRange(new ToolStripItem[] {
            projectMenuItem,
            dataMenuItem,
            algorithmMenuItem,
            aboutMenuItem
        });

        this.MainMenuStrip = menuStrip;
        this.Controls.Add(menuStrip);
    }

    private void ViewProjectsMenuItem_Click(object sender, EventArgs e)
    {
        var form = new ViewProjectsForm();
        form.Show();
    }

    private void CreateProjectMenuItem_Click(object sender, EventArgs e)
    {
        var form = new CreateProjectForm();
        form.Show();
    }

    private void AddAntennaMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AddAntennaForm();
        form.Show();
    }

    private void AddDeviceMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AddDeviceForm();
        form.Show();
    }

    private void AddFuselageMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AddFuselageForm();
        form.Show();
    }

    private void AddLandingSiteMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AddLandingSiteForm();
        form.Show();
    }

    private void AddEquipmentSchemeMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AddEquipmentPlacementSchemeForm();
        form.Show();
    }

    private void AlgorithmMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AlgorithmSettingsForm();
        form.Show();
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AboutForm(); // Предполагается, что форма AboutForm существует
        form.Show();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var projects = context.Projects.ToList();
            var uavParameters = context.UavParameters.ToList();
            var uavCompartments = context.UavCompartments.ToList();
            var equipmentParameters = context.EquipmentParameters.ToList();
            var antennaParameters = context.AntennaParameters.ToList();
            var uavAntennas = context.UavAntennas.ToList();
            var landingSites = context.LandingSites.ToList();
            var uavDevices = context.UavDevices.ToList();
            var equipmentPlacementSchemes = context.EquipmentPlacementSchemes.ToList();

            MessageBox.Show($"Projects: {projects.Count}\n" +
                            $"UAV Parameters: {uavParameters.Count}\n" +
                            $"UAV Compartments: {uavCompartments.Count}\n" +
                            $"Equipment Parameters: {equipmentParameters.Count}\n" +
                            $"Antenna Parameters: {antennaParameters.Count}\n" +
                            $"UAV Antennas: {uavAntennas.Count}\n" +
                            $"Landing Sites: {landingSites.Count}\n" +
                            $"UAV Devices: {uavDevices.Count}\n" +
                            $"Equipment Placement Schemes: {equipmentPlacementSchemes.Count}");
        }
    }

}
