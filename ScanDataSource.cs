using NTwain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsSignalRClient
{
    public class ScanDataSource
    {
        public int id { get; set; }
        public string name { get; set; }

        public IEnumerable<PixelType> Depths { get; set; }
        public IEnumerable<string> mode { get; set; }

        public IEnumerable<TWFix32> DPIs { get; set; }

        public IEnumerable<string> dpi { get; set; }

        public IEnumerable<SupportedSize> SupportSizes { get; set; }
        public IEnumerable<string> size { get; set; }
    }
}
