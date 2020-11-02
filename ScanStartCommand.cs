using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsSignalRClient
{
    public class ScanStartCommand
    {
        public string Id { get; set; }

        public string deviceName { get; set; }

        public string DPI { get; set; }

        public string Size { get; set; }

        public string Mode { get; set; }
    }
}
