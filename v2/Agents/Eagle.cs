using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mThink.Agents
{
    public class Eagle : Predator
    {
        public override string Label { get; set; }
        public override Uri Uri { get; set; }
        public override int Speed { get; set; }

        public Eagle(View view) : base(view)
        {
            Label = "E" + ID;
            NumberOfPredators++;
            Uri = new Uri("eagle.png", UriKind.Relative);
        }
    }
}
