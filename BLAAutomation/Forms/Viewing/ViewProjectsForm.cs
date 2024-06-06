using MaterialSkin;
using MaterialSkin.Controls;
using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

public partial class ViewProjectsForm : MaterialForm
{
    private readonly MaterialSkinManager materialSkinManager;

    public ViewProjectsForm()
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
        LoadProjects();
    }

    private void LoadProjects()
    {
        using (var context = new UavContext())
        {
            var projects = context.Projects
                .Include(p => p.UavParameters)
                .ToList()
                .Select(p => new ProjectDto
                {
                    ProjectNumber = p.ProjectNumber,
                    ProjectName = p.ProjectName,
                    UavModel = p.UavModel
                }).ToList();

            projectsDataGridView.DataSource = projects;
        }
    }

    private void FilterButton_Click(object sender, EventArgs e)
    {
        string filterText = filterField.Text;
        using (var context = new UavContext())
        {
            var filteredProjects = context.Projects
                .Include(p => p.UavParameters)
                .Where(p => p.ProjectName.Contains(filterText))
                .ToList()
                .Select(p => new ProjectDto
                {
                    ProjectNumber = p.ProjectNumber,
                    ProjectName = p.ProjectName,
                    UavModel = p.UavModel
                }).ToList();

            projectsDataGridView.DataSource = filteredProjects;
        }
    }

    private void EditButton_Click(object sender, EventArgs e)
    {
        if (projectsDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = projectsDataGridView.SelectedRows[0];
            int projectNumber = (int)selectedRow.Cells["ProjectNumber"].Value;

            var editProjectForm = new EditProjectForm(projectNumber);
            editProjectForm.ShowDialog();
            LoadProjects();
        }
    }

    private void DeleteButton_Click(object sender, EventArgs e)
    {
        if (projectsDataGridView.SelectedRows.Count > 0)
        {
            var selectedRow = projectsDataGridView.SelectedRows[0];
            int projectNumber = (int)selectedRow.Cells["ProjectNumber"].Value;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот проект?", "Удаление проекта", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (var context = new UavContext())
                {
                    var project = context.Projects.Find(projectNumber);
                    if (project != null)
                    {
                        context.Projects.Remove(project);
                        context.SaveChanges();
                    }
                }
                LoadProjects();
            }
        }
    }

    private void ExportButton_Click(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var projects = context.Projects.ToList()
                .Select(p => new ProjectDto
                {
                    ProjectNumber = p.ProjectNumber,
                    ProjectName = p.ProjectName,
                    UavModel = p.UavModel
                }).ToList();

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                Title = "Сохранить проекты как CSV"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("ProjectNumber,ProjectName,UavModel");

                    foreach (var project in projects)
                    {
                        writer.WriteLine($"{project.ProjectNumber},{project.ProjectName},{project.UavModel}");
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
            Title = "Импортировать проекты из CSV"
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
                        if (!line.StartsWith("ProjectNumber"))
                        {
                            var values = line.Split(',');
                            var project = new Project
                            {
                                ProjectNumber = int.Parse(values[0]),
                                ProjectName = values[1],
                                UavModel = values[2]
                            };
                            context.Projects.Add(project);
                        }
                    }
                    context.SaveChanges();
                }
            }

            MessageBox.Show("Данные успешно импортированы.", "Импорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadProjects();
        }
    }

    private void ViewProjectsForm_Load(object sender, EventArgs e)
    {
        using (var context = new UavContext())
        {
            var projects = context.Projects.ToList()
                .Select(p => new ProjectDto
                {
                    ProjectNumber = p.ProjectNumber,
                    ProjectName = p.ProjectName,
                    UavModel = p.UavModel
                }).ToList();

            projectsDataGridView.DataSource = projects;
        }
    }

    private void projectsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
}


public class ProjectDto
{
    public int ProjectNumber { get; set; }
    public string ProjectName { get; set; }
    public string UavModel { get; set; }
}

