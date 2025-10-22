using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpToScript.Core.Abstractions
{
    public interface ITypeMapper
    {
        (string tsType, bool isArray) Map(string csType);
    }
}
