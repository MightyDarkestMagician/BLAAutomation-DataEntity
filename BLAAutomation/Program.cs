using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using BLAAutomation.Models;

namespace BLAAutomation
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Database.SetInitializer(new InitializeDatabase());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
