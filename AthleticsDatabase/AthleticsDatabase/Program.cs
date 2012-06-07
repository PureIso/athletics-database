using System;
using System.IO;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var newPath = Path.Combine(Application.StartupPath, "oldFileAD");
            if (File.Exists(newPath)) File.Delete(newPath);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
