using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeysIA
{
    public abstract class Agent
    {
        public Position Position { get; set; }
        public abstract string Name { get; set; }
        public abstract int Index { get; set; }

        public Agent(Random random)
        {
            int x = random.Next(Program.Map.GetLength(0));
            int y = random.Next(Program.Map.GetLength(1));

            Position = new Position(x, y);

            while (Program.Map[Position.X, Position.Y] != null)
            {
                x = random.Next(Program.Map.GetLength(0));
                y = random.Next(Program.Map.GetLength(1));
                Position = new Position(x, y);
            }

            Program.Map[Position.X, Position.Y] = this;
        }

        public virtual void Move()
        {
            Random random = new Random();

            int x = random.Next(Program.Map.GetLength(0));
            int y = random.Next(Program.Map.GetLength(1));

            Position newPosition = new Position(x, y);

            while(Program.Map[newPosition.X, newPosition.Y] != null)
            {
                x = random.Next(Program.Map.GetLength(0));
                y = random.Next(Program.Map.GetLength(1));
                newPosition = new Position(x, y);
            }

            Program.Map[Position.X, Position.Y] = null;
            Program.Map[newPosition.X, newPosition.Y] = this;

            Position = newPosition;
        }
    }
}
