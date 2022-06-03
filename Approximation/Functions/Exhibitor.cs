using System;
using System.Collections.Generic;

namespace Approximation.Functions
{
    public class Exhibitor : Function
    {
        public Exhibitor(List<double> x, List<double> y) : base(x, y)
        {
        }

        public override string ToString()
        {
            if (coef.Count != 0)
            {
                return "y = " + coef[1] + "e^" + coef[0] + "x";
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
                value += _x[i] * _x[i];
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += _x[i];
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += _x[i] * Math.Round(Math.Log(_y[i]), 3);
            }

            list1.Add(value);
            value = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                value += _x[i];
            }

            list2.Add(value);
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

            return SumOfSqurDeviations();
        }

        public override double ValueInX(double x)
        {
            double coefB = coef[1];

            return coefB* Math.Exp(coef[0] * x);
        }

        private double SumOfSqurDeviations()
        {
            var functionValueAtPoint = new List<double>();
            var deviationsAtPoint = new List<double>();
            double sum = 0;
            double coefB = coef[1];

            for (int i = 0; i < _x.Count; i++)
            {
                double value = coefB * Math.Exp(coef[0]*_x[i]);
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
