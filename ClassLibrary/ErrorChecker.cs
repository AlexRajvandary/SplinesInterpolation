using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ClassLibrary
{
    public class ErrorChecker : IDataErrorInfo, INotifyPropertyChanged
    {
        private int length;
        private int uniformLength;
        private double left;
        private double right;
        private double x1;
        private double x2;
        private double x3;
        private SPf function;
        private string error;

        public ErrorChecker(int length, int uniformLength, double left, double right, SPf function, double x1, double x2, double x3)
        {
            Length = length;
            UniformLength = uniformLength;
            Left = left;
            Right = right;
            Function = function;
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }

        public Action DataChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Length
        {
            get => length;
            set
            {
                length = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(UniformLength));
            }
        }

        public int UniformLength
        {
            get => uniformLength;
            set
            {
                uniformLength = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(Length));
            }
        }

        public double Left
        {
            get => left;
            set
            {
                left = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(Right));
            }
        }

        public double Right
        {
            get => right;
            set
            {
                right = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(Left));
            }
        }

        public double X1
        {
            get => x1;
            set
            {
                x1 = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(X2));
                OnPropertyChanged(nameof(X3));
            }
        }

        public double X2
        {
            get => x2;
            set
            {
                x2 = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(X1));
                OnPropertyChanged(nameof(X3));
            }
        }

        public double X3
        {
            get => x3;
            set
            {
                x3 = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
                OnPropertyChanged(nameof(X1));
                OnPropertyChanged(nameof(X2));
            }
        }

        public SPf Function
        {
            get => function;
            set
            {
                function = value;
                DataChanged?.Invoke();
                OnPropertyChanged();
            }
        }

        public bool MeasuredDataError { get; set; } = false;

        public bool SplineParameterError { get; set; } = false;

        public string Error
        {
            get => error;
            set
            {
                error = value;
                OnPropertyChanged();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Length":
                        if ((Length < 3) || (Length > 100000) || (Length > UniformLength))
                        {
                            error = "Length is incorrect.";
                            MeasuredDataError = true;
                        }
                        else
                        {
                            MeasuredDataError = false;
                        }
                        break;
                    case "Right":
                    case "Left":
                        if (Right < Left)
                        {
                            error = "Right border is larger than left.";
                            MeasuredDataError = true;
                        }
                        else
                        {
                            MeasuredDataError = false;
                        }
                        break;
                    case "UniformLength":
                        if ((UniformLength < 3) || (UniformLength > 100000) || (Length > UniformLength))
                        {
                            error = "UniformLength is incorrect.";
                            SplineParameterError = true;
                        }
                        else
                        {
                            SplineParameterError = false;
                        }
                        break;
                    case "X1":
                    case "X2":
                    case "X3":
                        if (!((Left <= X1) && (X1 < X2) && (X2 < X3) && (X3 <= Right)))
                        {
                            error = "Limits";
                            SplineParameterError = true;
                        }
                        else
                        {
                            SplineParameterError = false;
                        }
                        break;
                    default:
                        break;
                }
                Error = error;
                return error;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
