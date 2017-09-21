using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeysIA
{
    class Predator : Agent
    {
        private static int totalOfInstances;
        public override string Name { get; set; }
        public override int Index { get; set; }

        public Predator(Random random) : base(random)
        {
            Index = totalOfInstances;

            totalOfInstances++;

            Name = "P" + totalOfInstances;
        }
    }
}
