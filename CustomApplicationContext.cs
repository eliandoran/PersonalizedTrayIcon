using PersonalizedTrayIcon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PersonalizedTrayIcon
{    
    class CustomApplicationContext: ApplicationContext
    {
        private const string CONFIG_PATH = "PersonalizedTrayIcon.ini";

        public CustomApplicationContext()
        {
            try
            {
                var config = ConfigurationParser.FromFile(CONFIG_PATH);

            }
            catch (ConfigurationException e)
            {
                MessageBox.Show(e.Message, "Personalized Tray Icon", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
