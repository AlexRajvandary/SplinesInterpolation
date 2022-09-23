using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClassLibrary;

namespace Lab2Var2
{
    public class ViewData : INotifyPropertyChanged
    {
        private string firstIntegral;
        private string secondIntegral;
        private string firstDerivatieve;
        private string secondDerivatieve;
        private string splineLeft;
        private string splineRight;

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewData()
        {
            InputChecker = new ErrorChecker(length: 20,
                                            uniformLength: 40,
                                            left: 0,
                                            right: 40,
                                            function: SPf.Random,
                                            x1: 1,
                                            x2: 10,
                                            x3: 20);

            InputChecker.DataChanged += OnInputDataChangedHandler;
            
            var md = new MeasuredData(InputChecker.Length,
                InputChecker.Left,
                InputChecker.Right,
                InputChecker.Function);

            var sp = new SplineParameters(InputChecker.UniformLength,
                InputChecker.X1,
                InputChecker.X2,
                InputChecker.X3);

            SplinesData = new(md, sp);
        }

        public ChartData Chart { get; set; } = new ChartData();

        public ErrorChecker InputChecker { get; set; }

        public SplinesData SplinesData { get; set; }

        public string FirstIntegral
        {
            get => firstIntegral;
            set
            {
                firstIntegral = value;
                OnPropertyChanged();
            }
        }

        public string SecondIntegral
        {
            get => secondIntegral;
            set
            {
                secondIntegral = value;
                OnPropertyChanged();
            }
        }

        public string FirstDerivatieve
        {
            get => firstDerivatieve;
            set
            {
                firstDerivatieve = value;
                OnPropertyChanged();
            }
        }

        public string SecondDerivatieve
        {
            get => secondDerivatieve;
            set
            {
                secondDerivatieve = value;
                OnPropertyChanged();
            }
        }

        public string SplineLeft
        {
            get => splineLeft;
            set
            {
                splineLeft = value;
                OnPropertyChanged();
            }
        }

        public string SplineRight
        {
            get => splineRight;
            set
            {
                splineRight = value;
                OnPropertyChanged();
            }
        }

        public void Update()
        {
            FirstIntegral = string.Empty;
            SecondIntegral = string.Empty;
            FirstDerivatieve = string.Empty;
            SecondDerivatieve = string.Empty;
            SplineLeft = string.Empty;
            SplineRight = string.Empty;

            SplinesData.Measured.Left = InputChecker.Left;
            SplinesData.Measured.Right = InputChecker.Right;
            SplinesData.Measured.Function = InputChecker.Function;
            SplinesData.Measured.Length = InputChecker.Length;

            SplinesData.SplineParameters.UniformLength = InputChecker.UniformLength;
            SplinesData.SplineParameters.X1 = InputChecker.X1;
            SplinesData.SplineParameters.X2 = InputChecker.X2;
            SplinesData.SplineParameters.X3 = InputChecker.X3;
        }

        private void OnInputDataChangedHandler() => Update();

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
