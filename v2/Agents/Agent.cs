using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mThink.Geographic;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace mThink.Agents
{
    public abstract class Agent
    {
        public int ID { get; set; }
        public abstract string Label { get; set; }
        public Position Position { get; set; }
        public abstract Uri Uri { get; set; }
        public abstract int Speed { get; set; }

        private static int NumberOfMonkeys = 0;
        private static int NumberOfEagles = 0;
        private static int NumberOfTigers = 0;

        private View View { get; set; }

        public Agent(View view)
        {
            AssignID();
            View = view;
            Position = GeneratePosition();
            View.Map[Position.X, Position.Y].Agent = this;
        }

        public virtual void Move()
        {
            Position newPosition = GeneratePosition();
            View.Map[Position.X, Position.Y].Agent = null;
            View.Map[newPosition.X, newPosition.Y].Agent = this;
            Position = newPosition;
        }

        public Position GeneratePosition()
        {
            Position newPosition;

            do
            {
                int xRecalc, yRecalc;

                if (Position != null)
                {
                    int x = View.Random.Next(Position.X - 1, Position.X + 2);
                    int y = View.Random.Next(Position.Y - 1, Position.Y + 2);

                    if (x < 0)
                    {
                        xRecalc = x + View.MapSize;
                    }
                    else if (x >= View.MapSize)
                    {
                        xRecalc = x - View.MapSize;
                    }
                    else
                    {
                        xRecalc = x;
                    }

                    if (y < 0)
                    {
                        yRecalc = y + View.MapSize;
                    }
                    else if (y >= View.MapSize)
                    {
                        yRecalc = y - View.MapSize;
                    }
                    else
                    {
                        yRecalc = y;
                    }
                }
                else
                {
                    xRecalc = View.Random.Next(View.MapSize);
                    yRecalc = View.Random.Next(View.MapSize);
                }

                newPosition = new Position(xRecalc, yRecalc);
            }
            while (View.Map[newPosition.X, newPosition.Y].Agent != null);

            return newPosition;
        }

        protected void AssignID()
        {
            if(GetType() == typeof(Monkey))
            {
                NumberOfMonkeys++;
                ID = NumberOfMonkeys;
            }
            else if(GetType() == typeof(Eagle))
            {
                NumberOfEagles++;
                ID = NumberOfEagles;
            }
            else if (GetType() == typeof(Tiger))
            {
                NumberOfTigers++;
                ID = NumberOfTigers;
            }
        }

        public void Draw()
        {
            BitmapImage bmp = new BitmapImage(Uri);
            ImageBrush img = new ImageBrush
            {
                ImageSource = bmp,
                Stretch = Stretch.UniformToFill
            };
            Rectangle canvas = new Rectangle
            {
                Fill = img
            };

            Grid.SetRow(canvas, Position.Y);
            Grid.SetColumn(canvas, Position.X);

            View.Grid.Children.Add(canvas);
        }

        public void DrawArea(int areaRadius, SolidColorBrush areaColor)
        {
            int diameter = areaRadius * 2 + 1;
            int startRow = Position.Y - areaRadius;
            int endRow = Position.Y + areaRadius;
            int startColumn = Position.X - areaRadius;
            int endColumn = Position.X + areaRadius;

            if (startRow < 0)
            {
                if (startColumn < 0)
                {
                    int startRowCanvas1 = startRow + View.Map.GetLength(0);
                    int spanRowCanvas1 = View.Map.GetLength(0) - startRowCanvas1;
                    int startRowCanvas2 = 0;
                    int spanRowCanvas2 = endRow;

                    int startColumnCanvas1 = startColumn + View.Map.GetLength(1);
                    int spanColumnCanvas1 = View.Map.GetLength(1) - startColumnCanvas1;
                    int startColumnCanvas2 = 0;
                    int spanColumnCanvas2 = endColumn;

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas3 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas4 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRowCanvas1);
                    Grid.SetRowSpan(canvas1, spanRowCanvas1);
                    Grid.SetColumn(canvas1, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas1, spanColumnCanvas1);

                    Grid.SetRow(canvas2, startRowCanvas2);
                    Grid.SetRowSpan(canvas2, spanRowCanvas2);
                    Grid.SetColumn(canvas2, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas2, spanColumnCanvas2);

                    Grid.SetRow(canvas3, startRowCanvas1);
                    Grid.SetRowSpan(canvas3, spanRowCanvas1);
                    Grid.SetColumn(canvas3, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas3, spanColumnCanvas2);

                    Grid.SetRow(canvas4, startRowCanvas2);
                    Grid.SetRowSpan(canvas4, spanRowCanvas2);
                    Grid.SetColumn(canvas4, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas4, spanColumnCanvas1);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                    View.Grid.Children.Add(canvas3);
                    View.Grid.Children.Add(canvas4);
                }
                else if (endColumn > View.Map.GetLength(1))
                {
                    int startRowCanvas1 = startRow + View.Map.GetLength(0);
                    int spanRowCanvas1 = View.Map.GetLength(0) - startRowCanvas1;
                    int startRowCanvas2 = 0;
                    int spanRowCanvas2 = endRow;

                    int startColumnCanvas1 = startColumn;
                    int spanColumnCanvas1 = View.Map.GetLength(1) - startColumnCanvas1;
                    int startColumnCanvas2 = 0;
                    int spanColumnCanvas2 = endColumn - View.Map.GetLength(1);

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas3 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas4 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRowCanvas1);
                    Grid.SetRowSpan(canvas1, spanRowCanvas1);
                    Grid.SetColumn(canvas1, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas1, spanColumnCanvas1);

                    Grid.SetRow(canvas2, startRowCanvas2);
                    Grid.SetRowSpan(canvas2, spanRowCanvas2);
                    Grid.SetColumn(canvas2, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas2, spanColumnCanvas2);

                    Grid.SetRow(canvas3, startRowCanvas1);
                    Grid.SetRowSpan(canvas3, spanRowCanvas1);
                    Grid.SetColumn(canvas3, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas3, spanColumnCanvas2);

                    Grid.SetRow(canvas4, startRowCanvas2);
                    Grid.SetRowSpan(canvas4, spanRowCanvas2);
                    Grid.SetColumn(canvas4, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas4, spanColumnCanvas1);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                    View.Grid.Children.Add(canvas3);
                    View.Grid.Children.Add(canvas4);
                }
                else
                {
                    int startRowCanvas1 = startRow + View.Map.GetLength(0);
                    int spanRowCanvas1 = View.Map.GetLength(0) - startRowCanvas1;
                    int startRowCanvas2 = 0;
                    int spanRowCanvas2 = endRow;

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRowCanvas1);
                    Grid.SetRowSpan(canvas1, spanRowCanvas1);
                    Grid.SetColumn(canvas1, startColumn);
                    Grid.SetColumnSpan(canvas1, diameter);

                    Grid.SetRow(canvas2, startRowCanvas2);
                    Grid.SetRowSpan(canvas2, spanRowCanvas2);
                    Grid.SetColumn(canvas2, startColumn);
                    Grid.SetColumnSpan(canvas2, diameter);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                }
            }
            else if (endRow > View.Map.GetLength(0))
            {
                if (startColumn < 0)
                {
                    int startRowCanvas1 = startRow;
                    int spanRowCanvas1 = View.Map.GetLength(0) - startRowCanvas1;
                    int startRowCanvas2 = 0;
                    int spanRowCanvas2 = endRow - View.Map.GetLength(0);

                    int startColumnCanvas1 = startColumn + View.Map.GetLength(1);
                    int spanColumnCanvas1 = View.Map.GetLength(1) - startColumnCanvas1;
                    int startColumnCanvas2 = 0;
                    int spanColumnCanvas2 = endColumn;

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas3 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas4 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRowCanvas1);
                    Grid.SetRowSpan(canvas1, spanRowCanvas1);
                    Grid.SetColumn(canvas1, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas1, spanColumnCanvas1);

                    Grid.SetRow(canvas2, startRowCanvas2);
                    Grid.SetRowSpan(canvas2, spanRowCanvas2);
                    Grid.SetColumn(canvas2, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas2, spanColumnCanvas2);

                    Grid.SetRow(canvas3, startRowCanvas1);
                    Grid.SetRowSpan(canvas3, spanRowCanvas1);
                    Grid.SetColumn(canvas3, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas3, spanColumnCanvas2);

                    Grid.SetRow(canvas4, startRowCanvas2);
                    Grid.SetRowSpan(canvas4, spanRowCanvas2);
                    Grid.SetColumn(canvas4, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas4, spanColumnCanvas1);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                    View.Grid.Children.Add(canvas3);
                    View.Grid.Children.Add(canvas4);
                }
                else if (endColumn > View.Map.GetLength(1))
                {
                    int startRowCanvas1 = startRow;
                    int spanRowCanvas1 = View.Map.GetLength(0) - startRowCanvas1;
                    int startRowCanvas2 = 0;
                    int spanRowCanvas2 = endRow - View.Map.GetLength(0);

                    int startColumnCanvas1 = startColumn;
                    int spanColumnCanvas1 = View.Map.GetLength(1) - startColumnCanvas1;
                    int startColumnCanvas2 = 0;
                    int spanColumnCanvas2 = endColumn - View.Map.GetLength(1);

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas3 = new Rectangle
                    {
                        Fill = areaColor
                    };
                    Rectangle canvas4 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRowCanvas1);
                    Grid.SetRowSpan(canvas1, spanRowCanvas1);
                    Grid.SetColumn(canvas1, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas1, spanColumnCanvas1);

                    Grid.SetRow(canvas2, startRowCanvas2);
                    Grid.SetRowSpan(canvas2, spanRowCanvas2);
                    Grid.SetColumn(canvas2, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas2, spanColumnCanvas2);

                    Grid.SetRow(canvas3, startRowCanvas1);
                    Grid.SetRowSpan(canvas3, spanRowCanvas1);
                    Grid.SetColumn(canvas3, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas3, spanColumnCanvas2);

                    Grid.SetRow(canvas4, startRowCanvas2);
                    Grid.SetRowSpan(canvas4, spanRowCanvas2);
                    Grid.SetColumn(canvas4, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas4, spanColumnCanvas1);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                    View.Grid.Children.Add(canvas3);
                    View.Grid.Children.Add(canvas4);
                }
                else
                {
                    int startRowCanvas1 = startRow;
                    int spanRowCanvas1 = View.Map.GetLength(0) - startRowCanvas1;
                    int startRowCanvas2 = 0;
                    int spanRowCanvas2 = endRow - View.Map.GetLength(0);

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRowCanvas1);
                    Grid.SetRowSpan(canvas1, spanRowCanvas1);
                    Grid.SetColumn(canvas1, startColumn);
                    Grid.SetColumnSpan(canvas1, diameter);

                    Grid.SetRow(canvas2, startRowCanvas2);
                    Grid.SetRowSpan(canvas2, spanRowCanvas2);
                    Grid.SetColumn(canvas2, startColumn);
                    Grid.SetColumnSpan(canvas2, diameter);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                }
            }
            else
            {
                if (startColumn < 0)
                {
                    int startColumnCanvas1 = startColumn + View.Map.GetLength(1);
                    int spanColumnCanvas1 = View.Map.GetLength(1) - startColumnCanvas1;
                    int startColumnCanvas2 = 0;
                    int spanColumnCanvas2 = endColumn;

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRow);
                    Grid.SetRowSpan(canvas1, diameter);
                    Grid.SetColumn(canvas1, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas1, spanColumnCanvas1);

                    Grid.SetRow(canvas2, startRow);
                    Grid.SetRowSpan(canvas2, diameter);
                    Grid.SetColumn(canvas2, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas2, spanColumnCanvas2);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                }
                else if (endColumn > View.Map.GetLength(1))
                {
                    int startColumnCanvas1 = startColumn;
                    int spanColumnCanvas1 = View.Map.GetLength(1) - startColumnCanvas1;
                    int startColumnCanvas2 = 0;
                    int spanColumnCanvas2 = endColumn - View.Map.GetLength(1);

                    Rectangle canvas1 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Rectangle canvas2 = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas1, startRow);
                    Grid.SetRowSpan(canvas1, diameter);
                    Grid.SetColumn(canvas1, startColumnCanvas1);
                    Grid.SetColumnSpan(canvas1, spanColumnCanvas1);

                    Grid.SetRow(canvas2, startRow);
                    Grid.SetRowSpan(canvas2, diameter);
                    Grid.SetColumn(canvas2, startColumnCanvas2);
                    Grid.SetColumnSpan(canvas2, spanColumnCanvas2);

                    View.Grid.Children.Add(canvas1);
                    View.Grid.Children.Add(canvas2);
                }
                else
                {
                    Rectangle canvas = new Rectangle
                    {
                        Fill = areaColor
                    };

                    Grid.SetRow(canvas, startRow);
                    Grid.SetRowSpan(canvas, diameter);
                    Grid.SetColumn(canvas, startColumn);
                    Grid.SetColumnSpan(canvas, diameter);

                    View.Grid.Children.Add(canvas);
                }
            }
        }
    }
}
