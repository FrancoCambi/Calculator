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
        private Types Type = Types.Estándar;

        private List<double> Nums = new();
        private string? Operator = "";
        private double Result;
        private double QueueNum;
        private string Preview = "";

        public MainWindow()
        {
            InitializeComponent();
            TypeText.Text = Type.ToString();

            bool punctuation = Double.TryParse("0.5", out _);

            if (!punctuation)
            {
                PointButton.Content = ",";
            }
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
        }

        private void AddShowNumber(string? number)
        {
            if (CurrentNum == "0")
            {
                CurrentNum = number;


                ResultText.FontSize = DefaultFontSize;
                ResultText.TextWrapping = TextWrapping.NoWrap;
                ResultText.Text = CurrentNum;

            }
            else if (CurrentNum.Length < MaxLenNumber)
            {
                CurrentNum += number;

                ResultText.FontSize = DefaultFontSize;
                ResultText.TextWrapping = TextWrapping.NoWrap;
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
                    bool text = Result != double.MaxValue;
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
                ResultText.FontSize = DefaultFontSize / 2;
                ResultText.Text = "Límite alcanzado.";
                Reset(false);
            }
        }

        private void MakePowerOperation(double power)
        {
            var isNumeric = Double.TryParse(ResultText.Text, out _);

            if (isNumeric)
            {
                if (power == -1 && Convert.ToDouble(ResultText.Text) == 0)
                {
                    ResultText.FontSize = DefaultFontSize / 2.3;
                    ResultText.Text = "El cero no tiene inverso multiplicativo.";
                    ResultText.TextWrapping = TextWrapping.Wrap;

                }
                else
                {
                    Result = Math.Round(Math.Pow(Convert.ToDouble(ResultText.Text), power), MaxLenNumber - 3);
                    CurrentNum = "";
                    ShowResult(Result, true);
                }
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

            Operator = "+";
            MakeOperation(false);
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {

            Operator = "-";
            MakeOperation(false);

            
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            Operator = "/";
            MakeOperation(false);
        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            Operator = "*";
            MakeOperation(false);
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

            MakePowerOperation(-1);
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            MakePowerOperation(2);
        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            MakePowerOperation(0.5);
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

        private void NumButton_Click(object sender, RoutedEventArgs e)
        {
            Button? button = sender as Button;

            if (button != null)
            {
                AddShowNumber(button.Content.ToString());
            }
        }

        // CIENTIFICA

        private void TypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Type == Types.Estándar)
            {
                Type = Types.Científica;
                TypeText.Text = Types.Científica.ToString();
                GridEstandar.Visibility = Visibility.Hidden;
                GridCientifica.Visibility = Visibility.Visible;
            }
            else
            {
                Type = Types.Estándar;
                TypeText.Text = Types.Estándar.ToString();
                GridCientifica.Visibility = Visibility.Hidden;
                GridEstandar.Visibility = Visibility.Visible;
            }
        }

        private void NatLogC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TenPowerC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void XPowerYC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PiC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AbsC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FactorialC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ParenthesisOpenC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ParenthesisCloseC_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SwitchSomeOptC_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
