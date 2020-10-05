using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PersonalizedTrayIcon
{
    class CustomApplicationContext: ApplicationContext
    {

        public CustomApplicationContext()
        {
            var config = @"[RecycleBin]
Icon = RecycleBin.ico
Exec = start shell:RecycleBinFolder

[Shutdown]
Icon = Shutdown.ico
Exec = shutdown /s /t 0
";

            var data = ConfigurationParser.ParseConfiguration(config);
        }

    }
}
