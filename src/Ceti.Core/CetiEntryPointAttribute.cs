using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CetiEntryPointAttribute : Attribute
    {
        
    }
}
