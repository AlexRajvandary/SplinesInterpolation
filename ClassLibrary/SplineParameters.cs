namespace ClassLibrary
{
    public class SplineParameters
    {
        public SplineParameters(int uniformLength, double x1, double x2, double x3)
        {
            UniformLength = uniformLength;
            X1 = x1;
            X2 = x2;
            X3 = x3;
        }

        public int UniformLength { get; set; }

        public double X1 { get; set; }

        public double X2 { get; set; }

        public double X3 { get; set; }
    }
}
