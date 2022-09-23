using System.Linq;
using System.Runtime.InteropServices;

namespace ClassLibrary
{
    public class SplinesData
    {
        public SplinesData(MeasuredData md, SplineParameters sp)
        {
            Measured = md;
            SplineParameters = sp;
        }

        public SplineParameters SplineParameters { get; set; }

        public MeasuredData Measured { get; set; }

        public double[] CubicSpline { get; set; }

        public double[] Integrals { get; set; } = new double[2];

        public double[] Derivatieves { get; set; } = new double[4];

        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKL_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double InterpolateMKL(int length, int uniformLength, double[] points, double[] func, double[] res);

        [DllImport("..\\..\\..\\..\\x64\\Debug\\MKL_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern double IntegrateMKL(int length, double[] points, double[] func, double[] limits, double[] integrals);

        public double Interpolate()
        {
            var InterpRes = new double[3 * SplineParameters.UniformLength];

            var status = InterpolateMKL(Measured.Length, SplineParameters.UniformLength, Measured.Grid, Measured.Measured, InterpRes);

            if (status == 0)
            {
                var res = new double[SplineParameters.UniformLength];

                for (int i = 0; i < SplineParameters.UniformLength; i++)
                {
                    res[i] = InterpRes[0 + (3 * i)];
                }

                CubicSpline = res;

                Derivatieves[0] = InterpRes[1];
                Derivatieves[1] = InterpRes[(3 * SplineParameters.UniformLength) - 2];
                Derivatieves[2] = InterpRes[2];
                Derivatieves[3] = InterpRes[(3 * SplineParameters.UniformLength) - 1];

                return 0;
            }
            else
            {
                return status;
            }
        }

        public double Integrate()
        {
            var Integral = new double[Measured.Length];

            var status = IntegrateMKL(Measured.Length,
                Measured.Grid,
                Measured.Measured,
                new double[] { SplineParameters.X1, SplineParameters.X2 },
                Integral);

            if (status == 0)
            {
                Integrals[0] = Integral.Sum();

                status = IntegrateMKL(Measured.Length,
                    Measured.Grid,
                    Measured.Measured,
                    new double[] { SplineParameters.X2, SplineParameters.X3 },
                    Integral);

                if (status == 0)
                {
                    Integrals[1] = Integral.Sum();

                    return 0;
                }
                else
                {
                    return status;
                }
            }
            else
            {
                return status;
            }
        }
    }
}
