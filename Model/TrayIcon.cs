using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PersonalizedTrayIcon.Model
{
    public class TrayIcon
    {
        
        public Icon Icon { get; set; }

        public string ExecFile { get; set; }

        public string[] ExecArguments { get; set; }

    }
}
