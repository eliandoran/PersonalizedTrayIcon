using PersonalizedTrayIcon.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PersonalizedTrayIcon
{
    public static class NotifyIconBuilder
    {

        public static List<NotifyIcon> FromConfiguration(UserConfiguration configuration)
        {
            var icons = new List<NotifyIcon>();

            foreach (var iconData in configuration.Icons)
            {
                var notifyIcon = new NotifyIcon();
                notifyIcon.Icon = iconData.Icon;
                notifyIcon.Tag = iconData;

                icons.Add(notifyIcon);
            }

            return icons;
        }

    }
}
