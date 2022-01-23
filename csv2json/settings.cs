using System;
using System.Collections.Generic;
using System.Text;

namespace csv2json
{
    [Serializable]
    class settings
    {
        public string inputFileName { get; set; }
        public string outputFileName { get; set; }
        public string inputFileEncoding { get; set; }
        public string separator { get; set; }

    }
}
