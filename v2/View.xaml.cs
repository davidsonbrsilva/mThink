using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using mThink.Geographic;
using mThink.Agents;
using System.Threading;
using System.Windows.Threading;

namespace mThink
{
    /// <summary>
    /// Lógica interna para View.xaml
    /// </summary>
    public partial class View : Window
    {
        public Grid Grid { get; set; }

        public Area[,] Map;
        public Random Random;

        public int MapSize;
        public int SignalRadius;
        public int VisionRadius;
        public int Trees;
        public int Symbols;

        public List<Monkey> Monkeys;
        public List<Predator> Predators;

        public View(int mapSize, int signalRadius, int visionRadius, int trees, int monkeys, int eagles, int tigers)
        {
            InitializeComponent();
            InitializeSystem(mapSize, signalRadius, visionRadius, trees, monkeys, eagles, tigers);
            Execute(15000);
        }

        public void InitializeSystem(int mapSize, int signalRadius, int visionRadius, int trees, int monkeys, int eagles, int tigers)
        {
            MapSize = mapSize;
            SignalRadius = signalRadius;
            VisionRadius = visionRadius;
            Trees = trees;
            Symbols = 10;

            Map = new Area[MapSize, MapSize];
            Random = new Random();
            Monkeys = new List<Monkey>();
            Predators = new List<Predator>();

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j] = new Area();
                }
            }
            for (int i = 0; i < eagles; i++)
            {
                Predators.Add(new Eagle(this));
            }
            for (int i = 0; i < tigers; i++)
            {
                Predators.Add(new Tiger(this));
            }
            for (int i = 0; i < monkeys; i++)
            {
                Monkeys.Add(new Monkey(this));
            }
        }

        public void Execute(int times)
        {
            Draw();
            Show();

            for (int i = 0; i < times; i++)
            {
                NextTime();
                ShowSteps(i);
                //ShowTables();
                //Draw();
                //DoEvents();
            }
        }

        public void ShowSteps(int i)
        {
            if (i % 500 == 0)
            {
                Console.WriteLine("\n\tTIME " + ((i / 500) + 1));
                ShowTables();
                Draw();
            }
        }

        public void ShowTables()
        {
            foreach(Monkey monkey in Monkeys)
            {
                monkey.ShowTable();
            }
        }

        public void NextTime()
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

        public void Draw()
        {
            Grid = new Grid
            {
                Background = new SolidColorBrush(Color.FromRgb(72, 196, 87))
            };

            // Cria a grade de acordo com o mapa especificado
            for (int i = 0; i < MapSize; i++)
            {
                RowDefinition row = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                ColumnDefinition col = new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };

                Grid.RowDefinitions.Add(row);
                Grid.ColumnDefinitions.Add(col);
            }

            foreach (Predator predator in Predators)
            {
                predator.Draw();
            }
            foreach (Predator predator in Predators)
            {
                predator.Draw();
            }
            foreach (Monkey monkey in Monkeys)
            {
                monkey.DrawArea(VisionRadius, new SolidColorBrush(Color.FromArgb(80, 191, 73, 73)));
            }
            foreach (Monkey monkey in Monkeys)
            {
                monkey.Draw();
            }

            Content = Grid;
            DoEvents();
            
            foreach (Predator predator in Predators)
            {
                predator.Draw();
            }
            foreach (Predator predator in Predators)
            {
                predator.Draw();
            }
            foreach (Monkey monkey in Monkeys)
            {
                if (monkey.CheckArea() != null)
                {
                    monkey.DrawArea(SignalRadius, new SolidColorBrush(Color.FromArgb(80, 193, 190, 72)));
                    Content = Grid;
                    DoEventsBreakTime();
                }
            }
            foreach (Monkey monkey in Monkeys)
            {
                monkey.Draw();
            }
        }

        public void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
            Thread.Sleep(50);
        }

        public void DoEventsBreakTime()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
            Thread.Sleep(500);
        }

        public object ExitFrame(object f)
        {
            ((DispatcherFrame)f).Continue = false;
            return null;
        }
    }
}
