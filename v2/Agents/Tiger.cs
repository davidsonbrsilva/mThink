using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mThink.Agents
{
    public class Tiger : Predator
    {
        public override string Label { get; set; }
        public override Uri Uri { get; set; }
        public override int Speed { get; set; }

        public Tiger(View view) : base(view)
        {
            Label = "T" + ID;
            NumberOfPredators++;
            Uri = new Uri("tiger.png", UriKind.Relative);
        }
    }
}
