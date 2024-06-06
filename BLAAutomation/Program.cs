using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using BLAAutomation.Models;
using System.IO;

namespace BLAAutomation
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Устанавливаем путь DataDirectory в безопасное место
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BLAAutomation");
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);

            Database.SetInitializer(new UavDatabaseInitializer());

            using (var context = new UavContext())
            {
                context.Database.Initialize(force: true);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
