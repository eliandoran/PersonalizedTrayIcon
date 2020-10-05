using PersonalizedTrayIcon.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            SetupIcons(icons);
        }

        private void SetupIcons(List<NotifyIcon> icons)
        {
            foreach (var icon in icons)
            {
                icon.Visible = true;
                icon.Click += NotifyIcon_Click;
            }
        }

        private void NotifyIcon_Click(object sender, EventArgs e)
        {
            var notifyIcon = (NotifyIcon)sender;
            var trayData = (TrayIcon)notifyIcon.Tag;
            Process.Start(trayData.ExecFile, string.Join(" ", trayData.ExecArguments));
        }
    }
}
