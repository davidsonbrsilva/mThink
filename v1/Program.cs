using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeysIA
{
    class Program
    {
        public static int Symbols = 10;
        public static int SignalRadius = 6;
        public static int VisionRadius = 12;
        public static int MapSizeX = 50;
        public static int MapSizeY = 50;
        public static int MonkeysAmount = 3;
        public static int PredatorsAmount = 3;

        public static Agent[,] Map = new Agent[MapSizeX, MapSizeY];
        public static Monkey[] Monkeys = new Monkey[MonkeysAmount];
        public static Predator[] Predators = new Predator[PredatorsAmount];

        static void Main(string[] args)
        {
            Initialize();
            Execute(10000);
            ShowMonkeys();
            Console.Read();
        }

        public static void Initialize()
        {
            Random random = new Random();

            for (int i = 0; i < Monkeys.Length; i++)
            {
                Monkeys[i] = new Monkey(random);
            }

            for (int i = 0; i < Predators.Length; i++)
            {
                Predators[i] = new Predator(random);
            }
        }

        public static void Execute(int times)
        {
            for (int i = 0; i < times; i++)
            {
                NextTime();
                ShowSteps(i);
            }
        }

        public static void NextTime()
        {
            foreach (Monkey monkey in Monkeys)
            {
                monkey.Move();
            }

            foreach (Predator predator in Predators)
            {
                predator.Move();
            }
        }

        public static void ShowMap()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == null)
                    {
                        Console.Write(".. ");
                    }

                    else
                    {
                        if (Map[i, j].GetType() == typeof(Monkey))
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(Map[i, j].Name + " ");
                            Console.ResetColor();
                        }
                        else if (Map[i, j].GetType() == typeof(Predator))
                        {
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write(Map[i, j].Name + " ");
                            Console.ResetColor();
                        }
                    }

                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public static void ShowMonkeys()
        {
            foreach (Monkey monkey in Monkeys)
            {
                monkey.ShowTable();
                Console.WriteLine("------------------------------------");
            }
        }

        public static void ShowSteps(int i)
        {
            if (i % 500 == 0)
            {
                Console.WriteLine("\n\tTIME " + ((i / 500) + 1));
                ShowMap();
                ShowMonkeys();
            }
        }
    }
}
