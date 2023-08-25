using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXml.Models
{
    public record InputNode
    {
        public string tagname { get; set; }
        public int tagLevel { get; set; }
        public bool? isrepeatable { get; set; }
        public string? count { get; set; }
        public string? parenttag { get; set; }
    }
}
