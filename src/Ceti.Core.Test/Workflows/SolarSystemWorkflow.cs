using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceti.Core;
using Ceti.Core.Support;
using Ceti.Core.Runners;
using Ceti.Core.Test.Entities;


namespace Ceti.Core.Test.Workflows
{
    public class SolarSystemWorkflow : CetiWorkflow<SolarSystemInput, SolarSystemOutput>
    {
        public override string Name
        {
            get { return "solor.system.wf"; }
        }

        [CetiEntryPoint]
        public CetiAgentSelector ProcessEarthComponent()
        {
            return null;
        }

        public CetiAgentSelector ProcessMarsComponent()
        {
            return null;
        }
    }
}
