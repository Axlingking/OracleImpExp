using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OracleImpExp.Import
{
    class DmpFile
    {
        public DmpFile(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; set; }

        private string name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    name = Path.GetFileNameWithoutExtension(FilePath);

                return name;
            }
        }

        public string Schema { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
