﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalizedTrayIcon.Model
{
    class ConfigurationException: Exception
    {

        public ConfigurationException(string message) : base(message)
        {

        }

        public ConfigurationException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
