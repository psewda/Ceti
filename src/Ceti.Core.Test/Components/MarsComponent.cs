using Ceti.Core.Support;
using Ceti.Core.Test.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Test.Components
{
    public class MarsComponent : CetiComponent<StandardInputData<Planet>, StandardOutputData<int>>
    {
        public override string Name
        {
            get { return "mars.cpt"; }
        }
    }
}
