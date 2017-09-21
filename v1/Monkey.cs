using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeysIA
{
    class Monkey : Agent
    {
        public double[,] Table { get; set; }
        public override string Name { get; set; }
        public override int Index { get; set; }

        private static int totalOfInstances = 0;

        public Monkey(Random random) : base(random)
        {
            // Incrementa o total de instâncias da classe para nomear os objetos criados
            totalOfInstances++;

            // Nomea o macaco como M1, por exemplo
            Name = "M" + totalOfInstances;

            // Define a tabela de acordo com a quantidade de símbolos e predadores especificados no programa
            Table = new double[Program.Symbols, Program.Predators.Length];

            // Cria uma variável do tipo Random para definir números aleatórios

            // Popula a tabela criada com valores flutuantes aleatórios entre 0.0 e 1.0
            for (int i = 0; i < Table.GetLength(0); i++)
            {
                for (int j = 0; j < Table.GetLength(1); j++)
                {
                    Table[i, j] = Math.Round(random.NextDouble(), 2);
                }
            }
        }

        public Predator CheckArea()
        {
            // Verifica os agentes dentro do raio do sinal enviado para atualizar suas tabelas se forem macacos
            for (int i = Position.X - Program.VisionRadius; i <= Position.X + Program.VisionRadius; i++)
            {
                int iRecalc = 0;
                int jRecalc = 0;

                // O mapa é esférico: o fim da borda direita recomeça na borda esquerda, por exemplo
                if (i >= Program.Map.GetLength(0))
                {
                    iRecalc = i - Program.Map.GetLength(0);
                }
                else if (i < 0)
                {
                    iRecalc = i + Program.Map.GetLength(0);
                }
                else
                {
                    iRecalc = i;
                }

                for (int j = Position.Y - Program.VisionRadius; j <= Position.Y + Program.VisionRadius; j++)
                {
                    if (j >= Program.Map.GetLength(1))
                    {
                        jRecalc = j - Program.Map.GetLength(1);
                    }
                    else if (j < 0)
                    {
                        jRecalc = j + Program.Map.GetLength(1);
                    }
                    else
                    {
                        jRecalc = j;
                    }
                    // Se existe algum agente na posição atual e este agente é um macaco
                    if (Program.Map[iRecalc, jRecalc] != null && Program.Map[iRecalc, jRecalc].GetType() == typeof(Predator))
                    {
                        //Console.WriteLine(this.Name + " viu um predador: " + Program.Map[iRecalc, jRecalc].Name);
                        return (Predator)Program.Map[iRecalc, jRecalc];
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

            

            

            int iRecalc;
            int jRecalc;
            //Console.WriteLine(this.Name + " enviou um sinal de alerta");
            // Verifica os agentes dentro do raio do sinal enviado para atualizar suas tabelas se forem macacos
            for (int i = Position.X - Program.SignalRadius; i <= Position.X + Program.SignalRadius; i++)
            {
                // O mapa é esférico: o fim da borda direita recomeça na borda esquerda, por exemplo
                if (i >= Program.Map.GetLength(0))
                {
                    iRecalc = i - Program.Map.GetLength(0);
                }
                else if (i < 0)
                {
                    iRecalc = i + Program.Map.GetLength(0);
                }
                else
                {
                    iRecalc = i;
                }

                for (int j = Position.Y - Program.SignalRadius; j <= Position.Y + Program.SignalRadius; j++)
                {
                    if (j >= Program.Map.GetLength(1))
                    {
                        jRecalc = j - Program.Map.GetLength(1);
                    }
                    else if (j < 0)
                    {
                        jRecalc = j + Program.Map.GetLength(1);
                    }
                    else
                    {
                        jRecalc = j;
                    }

                    // Se existe algum agente na posição atual e este agente é um macaco
                    if (Program.Map[iRecalc, jRecalc] != null && Program.Map[iRecalc, jRecalc].GetType() == typeof(Monkey) && Program.Map[iRecalc, jRecalc] != this)
                    {
                        Monkey monkey = (Monkey) Program.Map[iRecalc, jRecalc];

                        Predator predatorSeen = monkey.CheckArea();

                        for (int k = 0; k < Program.Predators.Count(); k++)
                        {
                            if (Program.Predators[k] == predatorSeen)
                                indexPredator = k;
                        }

                        // Obtém o sinal de maior valor para o predador encontrado
                        for (int k = 0; k < Table.GetLength(0); k++)
                        {
                            if (Table[k, indexPredator] > highest)
                            {
                                highest = Table[k, indexPredator];
                                indexSymbol = k;
                            }
                        }

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
                        //Console.WriteLine(monkey.Name + " recebeu o sinal enviado por " + this.Name);
                    }
                }
            }
        }

        public void ShowTable()
        {

            Console.Write("\n\tMonkey " + this.Name + "\n\n\t");

            foreach (Predator predator in Program.Predators)
            {
                Console.Write("\t" + predator.Name);
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
