using System;
using System.Collections.Generic;

namespace Approximation.Functions
{
    public class Hyperbol : Function
    {
        public Hyperbol(List<double> x, List<double> y) : base(x, y)
        {
        }

        public override string ToString()
        {
            if (coef.Count != 0)
            {
                return "y = " + coef[0] + "/x + " + coef[1];
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
                value += Math.Round(1/(_x[i] * _x[i]), 3);
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(1/_x[i], 3);
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(_y[i]/_x[i],3);
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += Math.Round(1 / _x[i], 3);
            }

            list2.Add(value);
            list2.Add(_y.Count);
            value = 0;

            for (int i = 0; i < _y.Count; i++)
            {
                value += _y[i];
            }

            list2.Add(value);
            matrix.Add(list1);
            matrix.Add(list2);

            TheGaussMethod();

            return SumOfSqurDeviations();
        }

        public override double ValueInX(double x)
        {
            return coef[0] / x + coef[1];
        }

        private double SumOfSqurDeviations()
        {
            var functionValueAtPoint = new List<double>();
            var deviationsAtPoint = new List<double>();
            double sum = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                double value = coef[0]/_x[i] + coef[1];
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
