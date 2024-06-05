using BLAAutomation.Models;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace BLAAutomation
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Database.SetInitializer(new InitializeDatabase());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
