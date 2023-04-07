using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Threading;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentNum = "";
        private const int MaxLenNumber = 11;
        private const int DefaultFontSize = 50;

        private List<double> Nums = new();
        private string? Operator = "";
        private double Result;
        private double QueueNum;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Reset(bool text)
        {
            Nums.Clear();
            CurrentNum = "";
            Operator = "";
            if (text)
            {
                ResultText.Text = "0";
            }
            ResultText.FontSize = DefaultFontSize;
        }

        private void AddShowNumber(string number)
        {
            if (CurrentNum == "0")
            {
                CurrentNum = number;
                ResultText.FontSize = DefaultFontSize;
                ResultText.Text = CurrentNum;

            }
            else if (CurrentNum.Length < MaxLenNumber)
            {
                CurrentNum += number;
                ResultText.FontSize = DefaultFontSize;
                ResultText.Text = CurrentNum;
            }
        }

        private void MakeOperation(bool equal) 
        {
    
            if (CurrentNum == "" && equal && Nums.Count > 0)
            {
                CurrentNum = QueueNum.ToString();
            }
            if (CurrentNum != "")
            {

                Nums.Add(Convert.ToDouble(CurrentNum));
                QueueNum = Convert.ToDouble(CurrentNum);
                Result = Convert.ToDouble(CurrentNum);
                CurrentNum = "";

                if (Nums.Count == 2)
                {
                    Result = Math.Round(SolveOperation(Nums[0], Nums[1], Operator), MaxLenNumber - 3);
                    bool text = Result == double.MaxValue ? false : true;
                    ShowResult(Result, text);

                }

            }


        }

        private double SolveOperation(double num1, double num2, string? operation)
        {
            double result;
            switch (operation)
            {
                case "=":
                    result = num1; 
                    break;
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        ResultText.FontSize = DefaultFontSize / 2.3;
                        ResultText.Text = "No se puede dividir por cero.";
                        result = double.MaxValue;
                    }
                    else
                    {
                        result = num1 / num2;

                    }
                    break;
                case "%":
                    result = num1 * (num2/100);
                    break;
                default:
                    result = 0;
                    break;
            }
                

            return result;
        }

        private void ShowResult(double result, bool text)
        {
            if (result.ToString().Length <= MaxLenNumber)
            {
                if (text)
                {
                    ResultText.Text = Result.ToString();
                }
                Nums.Clear();
                Nums.Add(Result);
            }
            else
            {
                ResultText.Text = "Límite alcanzado.";
                ResultText.FontSize = DefaultFontSize / 2;
                Reset(false);
            }
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {

            MakeOperation(true);
            if (Operator == "")
            {

                Operator = "=";
            }

        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {

            MakeOperation(false);
            Operator = "+";
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {

            MakeOperation(false);
            Operator = "-";

            
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation(false);
            Operator = "/";
        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation(false);
            Operator = "*";
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNum.Length > 1)
            {
                CurrentNum = CurrentNum.Remove(CurrentNum.Length - 1, 1);
                ResultText.Text = CurrentNum;
            }
            else
            {
                Reset(true);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentNum = "0";
            ResultText.Text = CurrentNum;
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            Reset(true);
        }

        private void PorcentajeButton_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation(false);
            Operator = "%";
        }

        private void ReciprocalButton_Click(object sender, RoutedEventArgs e)
        {
            Result = Math.Round(Math.Pow(Convert.ToDouble(ResultText.Text), -1), MaxLenNumber - 3);
            CurrentNum = "";
            if (Convert.ToDouble(ResultText.Text) == 0)
            {
                ResultText.FontSize = DefaultFontSize / 2.3;
                ResultText.Text = "No se puede dividir por cero.";

            }
            else
            {
                ShowResult(Result, true);
            }
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            Result = Math.Round(Math.Pow(Convert.ToDouble(ResultText.Text), 2), MaxLenNumber - 3);
            CurrentNum = "";
            ShowResult(Result, true);
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            Result = Math.Round(Math.Pow(Convert.ToDouble(ResultText.Text), 0.5), MaxLenNumber - 3);
            CurrentNum = "";
            ShowResult(Result, true);
        }

        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNum != "")
            {
                double newCurrentNum = Convert.ToDouble(CurrentNum);
                newCurrentNum *= -1;
                CurrentNum = newCurrentNum.ToString();
                ResultText.Text = CurrentNum;
            }
            else
            {
                CurrentNum += "-";
            }
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber(".");
        }
        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("0");
        }


        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("1");
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("2");
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("3");
        }

        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("4");
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("5");
        }

        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("6");
        }

        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("7");
        }

        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("8");
        }

        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("9");
        }
    }
}
