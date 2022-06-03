using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Impedance
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Point> points;

        public MainWindow()
        {
            InitializeComponent();
            var point = new Point();
            this.DataContext = point;
            points = new ObservableCollection<Point>();
            pointList.ItemsSource = points;
        }

        private void AddPointClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var coor1 = zFirst.Text;
                var coor2 = zSecond.Text;
                var point = new Point(double.Parse(coor1), double.Parse(coor2));
                points.Add(point);
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void SubmitClick(object sender, RoutedEventArgs e)
        {
            var expressionImpedance = expression.Text;

            if (expressionImpedance != "")
            {
                CalculateParametrs(expressionImpedance);
            }
            else
            {
                MessageBox.Show("Введите уравнение импеданса");
            }
        }

        private void CalculateParametrs(string expressionImpedance)
        {
            var x = new List<double>();
            var y = new List<double>();
            var mnk = new Approximation.Algorithm();

            foreach (var point in points)
            {
                x.Add(point.ZFirst);
                y.Add(point.ZSecond);
            }

            var approximationFunc = mnk.Calculate(x, y);

            var mathParser = new Parser.MathParser();
            var isRadians = false;
            var usingCombination = new List<string>();
            var rand = new Random();
            var isEnding = false;
            var step = 0;
            var paramR = 0;
            var paramC = 0;
            var minSum = double.MaxValue;
            var isR = expressionImpedance.Contains("R");
            var isC = expressionImpedance.Contains("C");
            var answer = "";

            while (!isEnding)
            {
                var isNew = false;
                int r = 1;
                int c = 1;

                while (!isNew)
                {
                    isNew = true;
                    r = isR ? rand.Next(1, 100) : 1;
                    c = isC ? rand.Next(1, 100) : 1;

                    var check = r.ToString() + ":" + c.ToString();

                    foreach(var str in usingCombination)
                    {
                        if (check == str)
                        {
                            isNew = false;
                        }
                    }
                }

                usingCombination.Add(r.ToString() + ":" + c.ToString());
                var w = 1;
                double sum = 0;
                var wMax = 101;
                expressionImpedance =expressionImpedance.Replace("R", r.ToString());
                expressionImpedance=expressionImpedance.Replace("C", c.ToString());

                while (w <= wMax)
                {
                    expressionImpedance = expressionImpedance.Replace("w", w.ToString());
                    var valueImp = mathParser.Parse(expressionImpedance, isRadians);
                    var valueHod = approximationFunc.ValueInX(w);
                    var dif = Math.Pow((valueHod - valueImp), 2);
                    sum += dif;
                    w += 10;
                }

                if (sum < minSum)
                {
                    step = 0;
                    minSum = sum;
                    sum = 0;
                    paramC = c;
                    paramR = r;
                }
                else
                {
                    step++;
                }

                if(step>25 || usingCombination.Count >= 10000)
                {
                    isEnding = true;
                }
            }

            answer = "Реальный годограф аппроскимируется функцией " + approximationFunc.ToString() + "\n";

            if(isR && isC)
            {
                answer += "Параметры при которых наилучшим образом описывается годограф: \n R = " + paramR + ", C = " + paramC;
            }
            else
            {
                if (isC)
                {
                    answer += "Параметры при которых наилучшим образом описывается годограф: \n C = " + paramC;
                }

                if (isR)
                {
                    answer += "Параметры при которых наилучшим образом описывается годограф: \n R = " + paramR;
                }
            }

            ans.Text = answer;
        }
    }
}
