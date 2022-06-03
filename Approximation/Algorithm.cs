using Approximation.Functions;
using System;
using System.Collections.Generic;

namespace Approximation
{
    public class Algorithm
    {
        public Function Calculate(List<double> list1, List<double> list2)
        {
            //var list1 = new List<double> { 1, 2, 3, 4, 5 };
            //var list2 = new List<double> { 5, 10, 15, 20, 25 };

            var approxim = new Function(list1, list2);
            double minDeviation = double.MaxValue;

            var exhib = new Exhibitor(list1, list2);
            var exhibValue = exhib.Calculate();

            if (exhibValue < minDeviation)
            {
                minDeviation = exhibValue;
                approxim = exhib;
            }

            var hyperbol = new Hyperbol(list1, list2);
            var hypebolValue = hyperbol.Calculate();

            if (hypebolValue < minDeviation)
            {
                minDeviation = hypebolValue;
                approxim = hyperbol;
            }

            var linear = new Linear(list1, list2);
            var linearValue = linear.Calculate();

            if (linearValue < minDeviation)
            {
                minDeviation = linearValue;
                approxim = linear;
            }

            var logarithm = new Logarithm(list1, list2);
            var logarithValue = logarithm.Calculate();

            if (logarithValue < minDeviation)
            {
                minDeviation = logarithValue;
                approxim = logarithm;
            }

            for (int i = 2; i < list1.Count; i++)
            {
                var polynom = new Polynomial(list1, list2, i);
                var polynomValue = polynom.Calculate();

                if (polynomValue < minDeviation)
                {
                    minDeviation = polynomValue;
                    approxim = polynom;
                }
            }

            return approxim;

        }
    }
}
