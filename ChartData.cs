using System;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace Lab2Var2
{
    public class ChartData
    {
        public ChartData()
        {
            Formatter = value => value.ToString("F4");
        }

        public SeriesCollection Series { get; set; } = new SeriesCollection();

        public Func<double, string> Formatter { get; set; }

        public void AddSeries(double[] points, double[] values, string title, int mode)
        {
            var Values = new ChartValues<ObservablePoint>();

            for (int i = 0; i < values.Length; i++)
            {
                Values.Add(new(points[i], values[i]));
            }

            if(mode == 0)
            {
                Series.Add(new ScatterSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Blue,
                    MinPointShapeDiameter = 5,
                    MaxPointShapeDiameter = 5
                });
            }
            else if(mode == 1)
            {
                Series.Add(new LineSeries
                {
                    Title = title,
                    Values = Values,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.Red,
                    PointGeometry = null,
                    LineSmoothness = 0
                });
            }
        }

        public void Clear()
        {
            Series?.Clear();
        }
    }
}
