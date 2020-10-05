using IniParser;
using IniParser.Model;
using PersonalizedTrayIcon.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PersonalizedTrayIcon
{
    public static class ConfigurationParser
    {
        public static UserConfiguration FromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var message = string.Format("Configuration file '{0}' does not exist.", filePath);
                throw new ConfigurationException(message);
            }

            return null;
        }

        public static UserConfiguration FromString(string iniData)
        {
            var parser = new StringIniParser();
            var sections = parser.ParseString(iniData).Sections;
            var icons = new List<TrayIcon>();
            
            foreach (var section in sections)
            {
                var trayIcon = ParseIconSection(section);
                icons.Add(trayIcon);
            }

            var config = new UserConfiguration();
            config.Icons = icons;
            return config;
        }

        private static TrayIcon ParseIconSection(SectionData sectionData)
        {
            var trayIcon = new TrayIcon();
            trayIcon.IconPath = sectionData.Keys["Icon"];
            trayIcon.ExecPath = sectionData.Keys["Exec"];
            return trayIcon;
        }

    }
}
