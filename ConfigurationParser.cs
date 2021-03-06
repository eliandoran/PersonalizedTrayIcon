﻿using IniParser;
using IniParser.Model;
using PersonalizedTrayIcon.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PersonalizedTrayIcon
{
    public static class ConfigurationParser
    {
        private const string EXCEPTION_FILE_NOT_FOUND = "Configuration file '{0}' does not exist.";
        private const string EXCEPTION_IO_ERROR = "Unable to read the configuration file due to an input/output error.";
        private const string EXCEPTION_MISSING_SECTIONS = "There must be at least one icon section defined in the configuration.";
        private const string EXCEPTION_MISSING_FIELD = "The section '{0}' is missing the following field: '{1}'.";
        private const string EXCEPTION_ICON_NOT_FOUND = "Icon file '{0}' does not exist.";

        private const string ICON_FIELD_ICON_PATH = "Icon";
        private const string ICON_FIELD_EXEC_PATH = "Exec";

        public static UserConfiguration FromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var message = string.Format(EXCEPTION_FILE_NOT_FOUND, filePath);
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
                throw new ConfigurationException(EXCEPTION_IO_ERROR, e);
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
                throw new ConfigurationException(EXCEPTION_MISSING_SECTIONS);
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

            if (!keys.ContainsKey(ICON_FIELD_ICON_PATH))
            {
                var message = string.Format(EXCEPTION_MISSING_FIELD, sectionData.SectionName, ICON_FIELD_ICON_PATH);
                throw new ConfigurationException(message);
            }

            if (!keys.ContainsKey(ICON_FIELD_EXEC_PATH))
            {
                var message = string.Format(EXCEPTION_MISSING_FIELD, sectionData.SectionName, ICON_FIELD_EXEC_PATH);
                throw new ConfigurationException(message);
            }

            trayIcon.Icon = LoadIcon(keys[ICON_FIELD_ICON_PATH]);
            SetExecData(trayIcon, sectionData.Keys[ICON_FIELD_EXEC_PATH]);
            return trayIcon;
        }

        private static Icon LoadIcon(string iconPath)
        {
            if (!File.Exists(iconPath))
            {
                var message = string.Format(EXCEPTION_ICON_NOT_FOUND, iconPath);
                throw new ConfigurationException(message);
            }

            return new Icon(iconPath, SystemInformation.SmallIconSize);
        }

        private static void SetExecData(TrayIcon trayIcon, string commandLine)
        {
            var fullArgs = NativeUtils.CommandLineToArgs(commandLine);
            var exec = fullArgs[0];
            var args = new string[0];
            
            if (fullArgs.Length > 1)
            {
                var argc = fullArgs.Length - 1;
                args = new string[argc];
                Array.Copy(fullArgs, 1, args, 0, argc);
            }

            trayIcon.ExecFile = exec;
            trayIcon.ExecArguments = args;
        }

    }
}
