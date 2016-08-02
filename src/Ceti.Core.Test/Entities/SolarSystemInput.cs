using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ceti.Core.Test.Entities
{
    public class SolarSystemInput : CetiInputData
    {
        public double Age { get; set; }

        public double Mass { get; set; }

        public int StarCount { get; set; }

        public int PlanetCount { get; set; }

        public int CometCount { get; set; }

        public Planet Earth { get; set; }

        public Planet Mars { get; set; }
    }
}
