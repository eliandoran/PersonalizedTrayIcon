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
            var configuration = ConfigurationParser.FromFile(CONFIG_PATH);
            var icons = NotifyIconBuilder.FromConfiguration(configuration);
            ShowIcons(icons);
        }

        private void ShowIcons(List<NotifyIcon> icons)
        {
            foreach (var icon in icons)
            {
                icon.Visible = true;
            }
        }

    }
}
