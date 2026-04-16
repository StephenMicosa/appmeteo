using System;
using System.Windows.Forms;
using WeatherApp.Configuration;
using WeatherApp.Forms;

namespace WeatherApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            EnvLoader.Load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
