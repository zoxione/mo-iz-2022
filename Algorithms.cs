using System;
using System.Collections.Generic;

namespace IZ
{
    internal class Algorithms : IAlgorithms
    {
        private Core _core;

        public Algorithms(Core c) {
            _core = c;
        }

        /// <summary>
        /// Решение функции f(x) = a*x*x + b*x + c
        /// </summary>
        private double SolveEquation(double point)
        {
            double value = 0;

            // a*x*x + b*x + c
            value = _core.a * point * point + _core.b * point + _core.c;

            return value;
        }

        /// <summary>
        /// Поиск чисел Фибоначчи
        /// </summary>
        private Tuple<int, int, List<double>> FindFibonacciNumber(double x)
        {
            int a = 0, b = 1, cnt = 0;
            List<double> F = new List<double>();

            do
            {
                F.Add(b);
                b += a;
                a = b - a;
                cnt++;
            } while (b <= x);

            return Tuple.Create(b, cnt, F);
        }

        /// <summary>
        /// Вычисление знака для очередной итерации
        /// </summary>
        private int ComputeSign(int sign, double value1, double value2)
        {
            if (_core.isMin)
            {
                // ищем минимум
                if (value1 > value2)
                {
                    // шаг удачный - нашли меньшее
                    // то знак не меняется
                    sign = sign;
                }
                else
                {
                    // шаг неудачный - нашли больше или равно
                    // то знак меняется
                    sign = sign * (-1);
                }
            }
            else
            {
                //  ищем максимум
                if (value1 < value2)
                {
                    // шаг удачный - нашли большее
                    // то знак не меняется
                    sign = sign;
                }
                else
                {
                    // шаг неудачный - нашли меньше или равно
                    // то знак меняется
                    sign = sign * (-1);
                }
            }

            return sign;
        }

        /// <summary>
        /// Реализация метода Дихотомии
        /// </summary>
        public DichotomyProps Dichotomy()
        {
            DichotomyProps dp = new DichotomyProps();
            double tempL1 = _core.L1;
            double tempL2 = _core.L2;
            bool isEnd = false;
            
            int n = (int)Math.Ceiling((2 / Math.Log(2)) * Math.Log((_core.L2 - _core.L1 - _core.epsilon) / (_core.l0 - _core.epsilon)));
            if (n % 2 == 1) { n += 1; }
            if (n <= 0) { throw new Exception("Ошибка при подсчете n"); }
            dp.n = n;

            while (!isEnd)
            {
                double mid = (tempL1 + tempL2) / 2;
                double point1 = mid - _core.epsilon / 2;
                double point2 = mid + _core.epsilon / 2;

                double value1 = SolveEquation(point1);
                double value2 = SolveEquation(point2);

                dp.Lines.Add(new Line(tempL1, tempL2, Tuple.Create(point1, value1), Tuple.Create(point2, value2)));

                double resPoint, resValue;
                if (_core.isMin)
                {
                    if (value1 < value2)
                    {
                        tempL2 = point2;
                        resPoint = point1;
                        resValue = value1;
                    }
                    else
                    {
                        tempL1 = point1;
                        resPoint = point2;
                        resValue = value2;
                    }
                }
                else
                {
                    if (value1 > value2)
                    {
                        tempL2 = point2;
                        resPoint = point1;
                        resValue = value1;
                    }
                    else
                    {
                        tempL1 = point1;
                        resPoint = point2;
                        resValue = value2;
                    }
                }

                if (tempL2 - tempL1 <= _core.l0)
                {
                    isEnd = true;
                    dp.resPoint = resPoint;
                    dp.resValue = resValue;
                }
            }

            return dp;
        }

        /// <summary>
        /// Реализация метода Фибоначчи
        /// </summary>
        public FibonacciProps Fibonacci()
        {
            FibonacciProps fp = new FibonacciProps();
            double tempL1 = _core.L1;
            double tempL2 = _core.L2;

            // Поиск чисел фибоначчи
            double N = (_core.L2 - _core.L1) / _core.l0;
            var tupleDataTemp1 = FindFibonacciNumber(N);
            int Fn = tupleDataTemp1.Item1;
            int n = tupleDataTemp1.Item2;
            List<double> FValues = tupleDataTemp1.Item3;

            double m = (_core.L2 - _core.L1) / Fn;
            fp.N = N;
            fp.Fn = Fn;
            fp.n = n;
            fp.m = m;

            double pointA = _core.L1;
            double valueA = SolveEquation(pointA);

            double pointB = _core.L2;
            double valueB = SolveEquation(pointB);

            double point1 = 0, value1  = 0;
            double point2 = 0, value2 = 0;
            double bestPoint = 0, bestValue = 0;

            // Знак 
            // удачный шаг: +
            // неудачный шаг: -
            int sign = 1;

            // Выбор с какой стороны ищем x1 и вычисление знака
            if (_core.isMin)
            {
                // ищем минимум
                if (valueA < valueB)
                {
                    // начинаем с А
                    point1 = pointA + m * FValues[FValues.Count - 2];
                    value1 = SolveEquation(point1);
                    sign = ComputeSign(sign, valueA, value1);
                }
                else
                {
                    // начинаем с B
                    point1 = pointB - m * FValues[FValues.Count - 2];
                    value1 = SolveEquation(point1);
                    sign = ComputeSign(sign, valueB, value1);
                }
            }
            else 
            {
                // ищем максимум
                if (valueA > valueB)
                {
                    // начинаем с А
                    point1 = pointA + m * FValues[FValues.Count - 2];
                    value1 = SolveEquation(point1);
                    sign = ComputeSign(sign, valueA, value1);
                }
                else
                {
                    // начинаем с B
                    point1 = pointB - m * FValues[FValues.Count - 2];
                    value1 = SolveEquation(point1);
                    sign = ComputeSign(sign, valueB, value1);
                }
            }

            // Пусть пока лучшая точка - x1
            // (даже если результат функции хуже)
            bestPoint = point1;
            bestValue = value1;

            // Цикл вычисления
            for (int i = 1; i <= n - 1; i++)
            {
                // Вычисление очередной точки
                if (i == n - 1)
                {
                    // последняя точка - середина
                    point2 = (tempL1 + tempL2) / 2;
                    value2 = SolveEquation(point2);
                }
                else
                {
                    // x(i) = x(i-1) + знак * m * F(n-i-2)
                    point2 = bestPoint + sign * m * FValues[FValues.Count - i - 2];
                    value2 = SolveEquation(point2);
                }

                // Сохрание отрезка, на котором лучшая точка и вычисленная на этой итерации
                if (bestPoint > point2)
                {
                    fp.Lines.Add(new Line(tempL1, tempL2, Tuple.Create(point2, value2), Tuple.Create(bestPoint, bestValue)));
                }
                else
                {
                    fp.Lines.Add(new Line(tempL1, tempL2, Tuple.Create(bestPoint, bestValue), Tuple.Create(point2, value2)));
                }

                // Проверка на успех шага
                if (_core.isMin)
                {
                    // ищем минимум
                    if (bestValue > value2)
                    {
                        // шаг удачный - нашли меньшее

                        // изменение отрезка
                        if (point2 > bestPoint)
                        {
                            tempL1 = bestPoint;
                        }
                        else
                        {
                            tempL2 = bestPoint;
                        }

                        // новая лучшая точка
                        bestPoint = point2;
                        bestValue = value2;
                    }
                    else
                    {
                        // шаг неудачный - нашли больше или равно
                        sign = sign * (-1);

                        // изменение отрезка
                        if (point2 > bestPoint)
                        {
                            tempL2 = point2;
                        }
                        else
                        {
                            tempL1 = point2;
                        }
                    }
                }
                else
                {
                    // ищем максимум
                    if (bestValue < value2)
                    {
                        // шаг удачный - нашли большее

                        // изменение отрезка
                        if (point2 > bestPoint)
                        {
                            tempL1 = bestPoint;
                        }
                        else
                        {
                            tempL2 = bestPoint;
                        }

                        // новая лучшая точка
                        bestPoint = point2;
                        bestValue = value2;
                    }
                    else
                    {
                        // шаг неудачный - нашли больше или равно
                        sign = sign * (-1);

                        // изменение отрезка
                        if (point2 > bestPoint)
                        {
                            tempL2 = point2;
                        }
                        else
                        {
                            tempL1 = point2;
                        }
                    }
                }
            }

            // В конце - выбор результата между лучшим и серединой
            if (_core.isMin)
            {
                if (bestValue > value2)
                {
                    // шаг удачный - нашли меньшее
                    fp.resValue = value2;
                    fp.resPoint = point2;
                }
                else
                {
                    // шаг неудачный - нашли большее или равно
                    fp.resValue = bestValue;
                    fp.resPoint = bestPoint;
                }
            }
            else
            {
                if (bestValue < value2)
                {
                    // шаг удачный - нашли большее
                    fp.resValue = value2;
                    fp.resPoint = point2;
                }
                else
                {
                    // шаг неудачный - нашли меньшее или равно
                    fp.resValue = bestValue;
                    fp.resPoint = bestPoint;
                }
            }

            return fp;
        }
    }
}
