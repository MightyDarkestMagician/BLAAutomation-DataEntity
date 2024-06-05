using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

public partial class AboutForm : MaterialForm
{
    public AboutForm()
    {
        InitializeComponent();
        var materialSkinManager = MaterialSkinManager.Instance;
        materialSkinManager.AddFormToManage(this);
        materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
        materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
    }

    private void AboutForm_Load(object sender, EventArgs e)
    {
    }

    private void labelDevelopersValue_Click(object sender, EventArgs e)
    {

    }

    private void labelAppNameValue_Click(object sender, EventArgs e)
    {

    }
}