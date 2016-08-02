using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core;
using Ceti.Core.Support;
using Ceti.Core.Test.Entities;


namespace Ceti.Core.Test.Components
{
    public class EarthComponent : CetiComponent<StandardInputData<Planet>, StandardOutputData<int>>
    {
        public override string Name
        {
            get { return "earth.cpt"; }
        }
    }
}
