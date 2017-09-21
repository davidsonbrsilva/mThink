using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mThink.Agents
{
    public class Monkey : Agent
    {
        public override string Label { get; set; }
        public View View { get; set; }
        public double[,] Table { get; set; }
        public override Uri Uri { get; set; }
        public override int Speed { get; set; }

        public Monkey(View view) : base(view)
        {
            Label = "M" + ID;
            View = view;
            Uri = new Uri("monkey.png", UriKind.Relative);

            Table = new double[View.Symbols, View.Predators.Count];

            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    Table[i, j] = Math.Round(View.Random.NextDouble(), 2);
                }
            }
        }

        public Predator CheckArea()
        {
            int iRecalc;
            int jRecalc;

            for (int i = Position.X - View.VisionRadius; i < Position.X + View.VisionRadius; i++)
            {
                if (i >= View.Map.GetLength(0))
                {
                    iRecalc = i - View.Map.GetLength(0);
                }
                else if (i < 0)
                {
                    iRecalc = i + View.Map.GetLength(0);
                }
                else
                {
                    iRecalc = i;
                }

                for (int j = Position.Y - View.VisionRadius; j <= Position.Y + View.VisionRadius; j++)
                {
                    if (j >= View.Map.GetLength(1))
                    {
                        jRecalc = j - View.Map.GetLength(1);
                    }
                    else if (j < 0)
                    {
                        jRecalc = j + View.Map.GetLength(1);
                    }
                    else
                    {
                        jRecalc = j;
                    }

                    if (View.Map[iRecalc, jRecalc].Agent != null && View.Map[iRecalc, jRecalc].Agent != this)
                    {
                        if (View.Map[iRecalc, jRecalc].Agent.GetType() == typeof(Tiger) || View.Map[iRecalc, jRecalc].Agent.GetType() == typeof(Eagle))
                        {
                            return (Predator)View.Map[iRecalc, jRecalc].Agent;
                        }
                    }
                }
            }

            return null;
        }

        public void SendSignal(Predator predator)
        {
            double highest = 0.0;
            int indexSymbol = 0;
            int indexPredator = 0;

            for (int i = 0; i < View.Predators.Count; i++)
            {
                if (View.Predators[i] == predator)
                    indexPredator = i;
            }

            // Obtém o sinal de maior valor para o predador encontrado
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                if (Table[i, indexPredator] > highest)
                {
                    highest = Table[i, indexPredator];
                    indexSymbol = i;
                }
            }

            int iRecalc;
            int jRecalc;

            // Envia o sinal para o raio em X definido
            for (int i = Position.X - View.SignalRadius; i <= Position.X + View.SignalRadius; i++)
            {
                // O Mapa é esférico.
                if (i >= View.Map.GetLength(0))
                {
                    iRecalc = i - View.Map.GetLength(0);
                }
                else if (i < 0)
                {
                    iRecalc = i + View.Map.GetLength(0);
                }
                else
                {
                    iRecalc = i;
                }

                // Envia o sinal para o raio em Y definido
                for (int j = Position.Y - View.SignalRadius; j <= Position.Y + View.SignalRadius; j++)
                {
                    // O Mapa é esférico
                    if (j >= View.Map.GetLength(1))
                    {
                        jRecalc = j - View.Map.GetLength(1);
                    }
                    else if (j < 0)
                    {
                        jRecalc = j + View.Map.GetLength(1);
                    }
                    else
                    {
                        jRecalc = j;
                    }

                    // Verifica se encontrou um macaco
                    if (View.Map[iRecalc, jRecalc].Agent != null && View.Map[iRecalc, jRecalc].Agent.GetType() == typeof(Monkey))
                    {
                        if(View.Map[iRecalc, jRecalc].Agent != this)
                        {
                            Monkey monkey = (Monkey)View.Map[iRecalc, jRecalc].Agent;

                            //Console.WriteLine(monkey.Label + " recebeu o sinal de " + this.Label);

                            Predator predatorSeen = monkey.CheckArea();

                            // Atualiza a tabela para o predador visto pelo macaco que recebeu o sinal
                            if (predatorSeen != null)
                            {
                                double newValue = monkey.Table[indexSymbol, indexPredator] + 0.01;

                                if (newValue <= 1)
                                {

                                    monkey.Table[indexSymbol, indexPredator] = newValue;
                                }
                                else
                                {
                                    monkey.Table[indexSymbol, indexPredator] = 1;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ShowTable()
        {
            Console.WriteLine("\tMonkey " + this.Label);
            Console.Write("\t");

            foreach (Predator predator in View.Predators)
            {
                Console.Write("\t" + predator.Label);
            }

            Console.WriteLine();

            for (int i = 0; i < Table.GetLength(0); i++)
            {
                Console.Write("\tS" + (i + 1) + "\t");
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    Console.Write(Table[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public override void Move()
        {
            base.Move();
            Predator predator = CheckArea();
            if (predator != null)
            {
                //Console.WriteLine(this.Label + " viu o predador " + predator.Label);
                SendSignal(predator);
            }
        }
    }
}
