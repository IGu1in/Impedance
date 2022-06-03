namespace Impedance
{
    public class Point
    {
        public double ZFirst { get; set; }
        public double ZSecond { get; set; }
        public Point()
        {

        }
        public Point(double first, double second)
        {
            ZFirst = first;
            ZSecond = second;
        }

        public override string ToString()
        {
            return "Z'="+ZFirst+",Z''="+ZSecond+"; ";
        }
    }
}
