using System;
using System.Collections.ObjectModel;

namespace ClassLibrary
{
    public class MeasuredData
    {
        public MeasuredData(int n, double left, double right, SPf function)
        {
            Length = n;
            Left = left;
            Right = right;
            Function = function;
            Grid = new double[Length];
        }

        public int Length { get; set; }

        public double Left { get; set; }

        public double Right { get; set; }

        public SPf Function { get; set; }

        public double[] Grid { get; set; }

        public double[] Measured { get; set; }

        public ObservableCollection<string> Info { get; set; } = new ObservableCollection<string>();

        public void GetGrid()
        {
            Grid = new double[Length];
            Grid[0] = Left;
            Grid[Length - 1] = Right;

            var rnd = new Random();

            for (int i = 1; i < Length - 1; i++)
            {
                Grid[i] = rnd.Next((int)Left, (int)Right) + rnd.NextDouble();
            }

            Array.Sort(Grid);
        }

        public void Measure()
        {
            Measured = new double[Length];

            switch (Function)
            {
                case SPf.Linear:
                    {
                        for (int i = 0; i < Length; i++)
                        {
                            Measured[i] = Grid[i];
                        }

                        break;
                    }

                case SPf.Cubic:
                    {
                        for (int i = 0; i < Length; i++)
                        {
                            Measured[i] = Math.Pow(Grid[i], 3);
                        }

                        break;
                    }

                case SPf.Random:
                    {
                        var rand = new Random();
                        for (int i = 0; i < Length; i++)
                        {
                            Measured[i] = 20 * rand.NextDouble();
                        }

                        break;
                    }
            }

            Info.Clear();

            for (int i = 0; i < Length; i++)
            {
                Info.Add($"Point: {Grid[i]}\n Measured value: {Measured[i]}\n");
            }
        }
    }
}
