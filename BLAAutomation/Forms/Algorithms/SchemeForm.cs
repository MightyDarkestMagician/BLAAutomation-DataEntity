using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;

namespace BLAAutomation
{
    public partial class SchemeForm : MaterialForm
    {
        public SchemeForm()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void SchemeForm_Load(object sender, EventArgs e)
        {
            // Вызывается при загрузке формы. Если нужно, добавьте сюда код.
        }
    }
}
