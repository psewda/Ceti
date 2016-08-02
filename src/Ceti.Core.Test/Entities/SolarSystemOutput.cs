using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Test.Entities
{
    public class SolarSystemOutput : CetiOutputData
    {
        public List<KeyValuePair<string, int>> ArtificialSatellites { get; set; }
    }
}
