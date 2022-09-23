using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClassLibrary;

namespace Lab2Var2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                Data = new();

                DataContext = this;

                Data.PropertyChanged += Data_PropertyChanged;
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            InitializeComponent();

            FunctionType.ItemsSource = Enum.GetValues(typeof(SPf));
        }

        private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          

        }

        public ViewData Data { get; set; }

        public bool IsMeasured { get; set; }

        public bool IsSplined { get; set; }

        private void MeasuredData_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Data.InputChecker.MeasuredDataError;
        }

        private void MeasuredData_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Data.Update();

                Data.SplinesData.Measured.GetGrid();
                Data.SplinesData.Measured.Measure();

                IsMeasured = true;
                IsSplined = false;

                Data.Chart.Clear();
                Data.Chart.AddSeries(Data.SplinesData.Measured.Grid, Data.SplinesData.Measured.Measured, "Measured", 0);
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Splines_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (!Data.InputChecker.SplineParameterError) && IsMeasured && (!IsSplined);
        }

        private void Splines_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                IsSplined = true;

                var status = Data.SplinesData.Interpolate();

                if (status == 0)
                {
                    Data.FirstDerivatieve = $"First Derivatieve: {Data.SplinesData.Derivatieves[0]:0.00}; {Data.SplinesData.Derivatieves[1]:0.00}";
                    Data.SecondDerivatieve = $"Second Derivatieve: {Data.SplinesData.Derivatieves[2]:0.00}; {Data.SplinesData.Derivatieves[3]:0.00}";
                    Data.SplineLeft = $"Spline-Left: {Data.SplinesData.CubicSpline[0]:0.00}";
                    Data.SplineRight = $"Spline-Right: {Data.SplinesData.CubicSpline[Data.SplinesData.Measured.Length - 1]:0.00}";

                    var gridUniform = new double[Data.SplinesData.SplineParameters.UniformLength];
                    var step = (Data.SplinesData.Measured.Right - Data.SplinesData.Measured.Left) / (Data.SplinesData.SplineParameters.UniformLength - 1);

                    for (int i = 0; i < Data.SplinesData.SplineParameters.UniformLength; i++)
                    {
                        gridUniform[i] = Data.SplinesData.Measured.Left + (i * step);
                    }

                    Data.Chart.AddSeries(gridUniform, Data.SplinesData.CubicSpline, "Spline", 1);

                    status = Data.SplinesData.Integrate();

                    if (status == 0)
                    {
                        Data.FirstIntegral = $"First integral: {Data.SplinesData.Integrals[0]:0.00}";
                        Data.SecondIntegral = $"Second integral: {Data.SplinesData.Integrals[1]:0.00}";
                    }
                    else
                    {
                        MessageBox.Show($"Error in Integrate(): {status}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Error in Interpolate(): {status}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show($"Unexpected error: {error.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
