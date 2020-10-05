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

            FileStream file = null;
            try
            {
                file = File.OpenRead(filePath);
                return FromStream(file);
            }
            catch (IOException e)
            {
                throw new ConfigurationException("Unable to read the configuration file due to an input/output error.", e);
            }
            finally
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }            
        }

        public static UserConfiguration FromStream(FileStream fileStream)
        {
            var reader = new StreamReader(fileStream);
            var parser = new StreamIniDataParser();
            var ini = parser.ReadData(reader);
            var icons = new List<TrayIcon>();

            // Fail if there are no defined sections.
            if (ini.Sections.Count == 0)
            {
                throw new ConfigurationException("There must be at least one icon section defined in the configuration.");
            }
            
            foreach (var section in ini.Sections)
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
            var keys = sectionData.Keys;

            if (!keys.ContainsKey("Icon"))
            {
                throw new ConfigurationException("Missing required field for icon section: Icon");
            }

            if (!keys.ContainsKey("Exec"))
            {
                throw new ConfigurationException("Missing required field for icon section: Exec");
            }
            
            trayIcon.IconPath = keys["Icon"];
            trayIcon.ExecPath = sectionData.Keys["Exec"];
            return trayIcon;
        }

    }
}
