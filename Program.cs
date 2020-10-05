using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace PersonalizedTrayIcon
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                var context = new CustomApplicationContext();
                Application.Run(context);
            }
            catch (Model.ConfigurationException e)
            {
                MessageBox.Show(e.Message, "Personalized Tray Icon", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }            
        }
    }
}
