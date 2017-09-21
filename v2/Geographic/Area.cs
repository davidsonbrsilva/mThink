using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mThink.Agents;

namespace mThink.Geographic
{
    public class Area
    {
        public Agent Agent { get; set; }
        public Type Type { get; set; }

        public Area(Agent agent = null)
        {
            Type = Type.Free;
        }
    }
}
