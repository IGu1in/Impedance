using System;
using System.Collections.Generic;

namespace Approximation.Functions
{
    public class Function
    {
        protected readonly List<double> _x; 
        protected readonly List<double> _y;
        protected List<double> coef;
        protected List<List<double>> matrix;

        public Function(List<double> x, List<double> y)
        {
            if (x.Count == y.Count)
            {
                _x = x;
                _y = y;
            }
            else
            {
                throw new Exception("Неверные входные данные");
            }

            coef = new List<double>();
            matrix = new List<List<double>>();
        }

        public virtual double ValueInX(double x) { return x; }

        protected void TheGaussMethod()
        {
            var isQuadraticity = true;

            foreach (var expression in matrix)
            {
                if (expression.Count - 1 != matrix.Count)
                {
                    isQuadraticity = false;
                }
            }

            if (isQuadraticity)
            {
                for (int step = 0; step < matrix.Count; step++)
                {
                    var divider = matrix[step][step];

                    for (int i = 0; i < matrix[step].Count; i++)
                    {
                        matrix[step][i] = Math.Round(matrix[step][i] / divider, 15);
                    }

                    for (int numberStr = step + 1; numberStr < matrix.Count; numberStr++)
                    {
                        var coeff = matrix[numberStr][step];

                        for (int i = step; i < matrix[numberStr].Count; i++)
                        {
                            matrix[numberStr][i] -= Math.Round(matrix[step][i] * coeff, 15);
                        }
                    }
                }
            }
            else
            {
                throw new Exception();
            }

            for (int step = 0; step < matrix.Count; step++)
            {
                if (coef.Count != 0)
                {
                    for (int i = 0; i < step; i++)
                    {
                        matrix[matrix.Count - 1 - step][matrix[matrix.Count - 1 - step].Count - i - 2] *= coef[i];
                    }

                    for (int i = 0; i < step; i++)
                    {
                        matrix[matrix.Count - 1 - step][matrix[matrix.Count - 1 - step].Count - 1] -= matrix[matrix.Count - 1 - step][matrix[step].Count - 2 - i];
                    }

                    coef.Add(Math.Round(matrix[matrix.Count - 1 - step][matrix[step].Count - 1], 15));
                }
                else
                {
                    coef.Add(Math.Round(matrix[matrix.Count - 1 - step][matrix[matrix.Count - 1 - step].Count - 1], 15));
                }
            }

            coef.Reverse();
        }
    }
}
