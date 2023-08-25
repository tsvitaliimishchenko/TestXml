using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXml.Interfaces
{
    public interface ITranslator<in TSource, TDestination>
    {
        TDestination Translate(TSource source);
    }
}
