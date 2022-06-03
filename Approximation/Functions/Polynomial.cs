using System;
using System.Collections.Generic;

namespace Approximation.Functions
{
    public class Polynomial : Function
    {
        private readonly int _power;

        public Polynomial(List<double> x, List<double> y, int power) : base(x, y)
        {
            _power = power;
        }

        public override string ToString()
        {
            if (coef.Count != 0)
            {
                var answer = "y = ";

                for(int i = 0; i<=_power; i++)
                {
                    if (i == 0)
                    {
                        answer += coef[i] + "x^" + (_power - i);
                    }
                    else
                    {
                        answer += "+ " + coef[i] + "x^" + (_power - i);
                    }
                }

                return answer;
            }
            else
            {
                return "Расчет не произведен";
            }
        }

        public double Calculate()
        {
            for(int i = 0; i<_power+1; i++)
            {
                var line = new List<double>();

                for(int j = 0; j<_power+1; j++)
                {
                    double value1 = 0;

                    for (int k = 0; k < _x.Count; k++)
                    {
                        value1 += Math.Pow(_x[k], i+j);
                    }

                    line.Add(value1);
                }

                line.Reverse();

                double value2 = 0;

                for (int k = 0; k < _x.Count; k++)
                {
                    value2 += _y[k]*Math.Pow(_x[k], i);
                }

                line.Add(value2);
                matrix.Add(line);
            }

            TheGaussMethod();

            return SumOfSqurDeviations();
        }

        public override double ValueInX(double x)
        {
            double value = 0;

            for (int j = 0; j <= _power; j++)
            {
                value = Math.Pow(x, j) * coef[j];
            }

            return value;
        }

        private double SumOfSqurDeviations()
        {
            var functionValueAtPoint = new List<double>();
            var deviationsAtPoint = new List<double>();
            double sum = 0;

            for (int i = 0; i < _x.Count; i++)
            {
                double value = 0;

                for (int j = 0; j <= _power; j++)
                {
                    value = Math.Pow(_x[i], j) * coef[j];
                }

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
