using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXml.Interfaces
{
    public interface ISerializer<TSource>
    {
        public string Serialize(TSource source);
    }
}
