using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mThink.Agents
{
    public abstract class Predator : Agent
    {
        public static int NumberOfPredators = 0;

        public Predator(View view) : base(view)
        {
        }
    }
}
