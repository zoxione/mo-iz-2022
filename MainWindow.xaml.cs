using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace IZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Core _core;
        
        public MainWindow()
        {
            _core = new Core();

            InitializeComponent();


        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool ReadData(string algorithmName)
        {
            double number;
            if (algorithmName == "dichotomy")
            {
                if (double.TryParse(dichotomy_textBox_a.Text, out number))
                {
                    _core.a = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(dichotomy_textBox_b.Text, out number))
                {
                    _core.b = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(dichotomy_textBox_c.Text, out number))
                {
                    _core.c = number;
                }
                else
                {
                    return false;
                }


                if (double.TryParse(dichotomy_textBox_l1.Text, out number))
                {
                    _core.L1 = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(dichotomy_textBox_l2.Text, out number))
                {
                    _core.L2 = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(dichotomy_textBox_epsilon.Text, out number))
                {
                    _core.epsilon = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(dichotomy_textBox_l0.Text, out number))
                {
                    _core.l0 = number;
                }
                else
                {
                    return false;
                }

                if (dichotomy_comboBox.SelectedIndex == 0) 
                {
                    _core.isMin = true;
                }
                else
                {
                    _core.isMin = false;
                }

                dichotomy_textBlock_output.Text = "";
            }
            else if (algorithmName == "fibonacci")
            {
                if (double.TryParse(fibonacci_textBox_a.Text, out number))
                {
                    _core.a = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(fibonacci_textBox_b.Text, out number))
                {
                    _core.b = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(fibonacci_textBox_c.Text, out number))
                {
                    _core.c = number;
                }
                else
                {
                    return false;
                }


                if (double.TryParse(fibonacci_textBox_l1.Text, out number))
                {
                    _core.L1 = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(fibonacci_textBox_l2.Text, out number))
                {
                    _core.L2 = number;
                }
                else
                {
                    return false;
                }

                if (double.TryParse(fibonacci_textBox_l0.Text, out number))
                {
                    _core.l0 = number;
                }
                else
                {
                    return false;
                }

                if (fibonacci_comboBox.SelectedIndex == 0)
                {
                    _core.isMin = true;
                }
                else
                {
                    _core.isMin = false;
                }

                _core.indexEquation = fibonacci_comboBox.SelectedIndex;

                fibonacci_textBlock_output.Text = "";
            }

            return true;
        }

        private void Dichotomy_Button_Submit(object sender, RoutedEventArgs e)
        {
            if (ReadData("dichotomy"))
            {
                try 
                { 
                    Algorithms algorithms = new Algorithms(_core);
                    DichotomyProps dp = algorithms.Dichotomy();

                    dichotomy_textBlock_output.Text += $"n = {dp.n}\n\n";

                    for (int i = 0; i < dp.Lines.Count; i++) 
                    {
                        dichotomy_textBlock_output.Text += $"L{i} = [{dp.Lines[i].L1};{dp.Lines[i].L2}]: ";
                        dichotomy_textBlock_output.Text += $"\nf({dp.Lines[i].pointValue1.Item1}) = {dp.Lines[i].pointValue1.Item2}";
                        dichotomy_textBlock_output.Text += $"\nf({dp.Lines[i].pointValue2.Item1}) = {dp.Lines[i].pointValue2.Item2}\n";
                        if (i != dp.Lines.Count - 1)
                        {
                            dichotomy_textBlock_output.Text += "\n";
                        }
                    }

                    dichotomy_textBlock_result.Text = $"x*={dp.resPoint} \nf*(x*)={dp.resValue}";
                }
                catch (Exception error)
                {
                    MessageBox.Show($"Ошибка: \n{error.Message}");
                }
            }
            else 
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }

        private void Fibonacci_Button_Submit(object sender, RoutedEventArgs e)
        {
            if (ReadData("fibonacci"))
            {
                try
                {
                    Algorithms algorithms = new Algorithms(_core);
                    FibonacciProps fp = algorithms.Fibonacci();

                    fibonacci_textBlock_output.Text += $"N = {fp.N} => Fn = {fp.Fn} n = {fp.n}";
                    fibonacci_textBlock_output.Text += $"\nm = {fp.m}\n\n";

                    for (int i = 0; i < fp.Lines.Count; i++)
                    {
                        fibonacci_textBlock_output.Text += $"L{i} = [{fp.Lines[i].L1};{fp.Lines[i].L2}]: ";
                        fibonacci_textBlock_output.Text += $"\nf({fp.Lines[i].pointValue1.Item1}) = {fp.Lines[i].pointValue1.Item2}";
                        fibonacci_textBlock_output.Text += $"\nf({fp.Lines[i].pointValue2.Item1}) = {fp.Lines[i].pointValue2.Item2}\n";
                        if (i != fp.Lines.Count - 1)
                        {
                            fibonacci_textBlock_output.Text += "\n";
                        }
                    }

                    fibonacci_textBlock_result.Text = $"x*={fp.resPoint} \nf*(x*)={fp.resValue}";
                }
                catch (Exception error)
                {
                    MessageBox.Show($"Ошибка: \n{error.Message}");
                }
            }
            else
            {
                MessageBox.Show("Данные введены неверно!");
            }
        }

        private void Dichotomy_Button_Clear(object sender, RoutedEventArgs e)
        {
            dichotomy_comboBox.SelectedIndex = 0;
            dichotomy_textBox_a.Text = "0";
            dichotomy_textBox_b.Text = "0";
            dichotomy_textBox_c.Text = "0";
            dichotomy_textBox_l1.Text = "0";
            dichotomy_textBox_l2.Text = "0";
            dichotomy_textBox_epsilon.Text = "0";
            dichotomy_textBox_l0.Text = "0";
            dichotomy_textBlock_output.Text = "";
            dichotomy_textBlock_result.Text = "";
        }

        private void Dichotomy_Button_Example(object sender, RoutedEventArgs e)
        {
            dichotomy_comboBox.SelectedIndex = 1;
            dichotomy_textBox_a.Text = "-2";
            dichotomy_textBox_b.Text = "12";
            dichotomy_textBox_c.Text = "0";
            dichotomy_textBox_l1.Text = "0";
            dichotomy_textBox_l2.Text = "10";
            dichotomy_textBox_epsilon.Text = "0,2";
            dichotomy_textBox_l0.Text = "1";
            dichotomy_textBlock_output.Text = "";
            dichotomy_textBlock_result.Text = "";
        }

        private void Fibonacci_Button_Clear(object sender, RoutedEventArgs e)
        {
            fibonacci_comboBox.SelectedIndex = 0;
            fibonacci_textBox_a.Text = "0";
            fibonacci_textBox_b.Text = "0";
            fibonacci_textBox_c.Text = "0";
            fibonacci_textBox_l1.Text = "0";
            fibonacci_textBox_l2.Text = "0";
            fibonacci_textBox_l0.Text = "0";
            fibonacci_textBlock_output.Text = "";
            fibonacci_textBlock_result.Text = "";
        }

        private void Fibonacci_Button_Example(object sender, RoutedEventArgs e)
        {
            fibonacci_comboBox.SelectedIndex = 0;
            fibonacci_textBox_a.Text = "1";
            fibonacci_textBox_b.Text = "-2";
            fibonacci_textBox_c.Text = "5";
            fibonacci_textBox_l1.Text = "-2";
            fibonacci_textBox_l2.Text = "6";
            fibonacci_textBox_l0.Text = "1";
            fibonacci_textBlock_output.Text = "";
            fibonacci_textBlock_result.Text = "";
        }
    }
}
