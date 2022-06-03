using System;
using System.Collections.Generic;

namespace Approximation.Functions
{
    public class Power : Function
    {
        public Power(List<double> x, List<double> y) : base(x, y)
        {

        }
        public override string ToString()
        {
            if (coef.Count != 0)
            {
                return "y = " + coef[1] + "x^" + coef[0];
            }
            else
            {
                return "Расчет не произведен";
            }
        }

        public double Calculate()
        {
            var list1 = new List<double>();
            var list2 = new List<double>();
            double value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(Math.Log(_x[i]) * Math.Log(_x[i]), 3);
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(Math.Log(_x[i]), 3);
            }

            list1.Add(Math.Round(value, 3));
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(Math.Log(_y[i]) * Math.Log(_x[i]), 3);
            }

            list1.Add(Math.Round(value, 3));
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(Math.Log(_x[i]), 3);
            }

            list2.Add(Math.Round(value, 3));
            list2.Add(_y.Count);
            value = 0;

            for (int i = 0; i < _y.Count; i++)
            {
                value += Math.Round(Math.Log(_y[i]), 3);
            }

            list2.Add(Math.Round(value, 3));
            matrix.Add(list1);
            matrix.Add(list2);

            TheGaussMethod();
            coef[1] = Math.Round(Math.Pow(Math.E, coef[1]), 3);

            return SumOfSqurDeviations();
        }

        public override double ValueInX(double x)
        {
            return Math.Round(Math.Log(x), 3) * coef[0] + coef[1];
        }

        private double SumOfSqurDeviations()
        {
            var functionValueAtPoint = new List<double>();
            var deviationsAtPoint = new List<double>();
            double sum = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                double value = Math.Round(Math.Log(_x[i]), 3) * coef[0] + coef[1];
                functionValueAtPoint.Add(Math.Round(value, 3));
            }

            for (int i = 0; i < _y.Count; i++)
            {
                double value = Math.Pow(_y[i] - functionValueAtPoint[i], 2);
                deviationsAtPoint.Add(Math.Round(value, 3));
            }

            foreach (var value in deviationsAtPoint)
            {
                sum += value;
            }

            return Math.Round(sum, 5);
        }
    }
}
