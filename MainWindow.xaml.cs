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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string CurrentNum = "";
        private const int MaxLenNumber = 11;

        private List<double> Nums = new();
        private string? Operator;
        private bool NewOperation = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddShowNumber(string number)
        {
            if (CurrentNum.Length < MaxLenNumber)
            {
                CurrentNum += number;
                ResultText.Text = CurrentNum;
            }
        }

        private void MakeOperation(string? _operator)
        {
            if (Nums.Count < 2 && CurrentNum != "")
            {
                Operator = _operator;
                Nums.Add(Convert.ToDouble(CurrentNum));
                CurrentNum = "";
            }

        }

        private double SolveOperation(double num1, double num2, string? operation)
        {
            double result;
            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                default:
                    result = 0;
                    break;
            }
                

            return result;
        }

        private void ShowResult()
        {
            double result;
            MakeOperation(Operator);
            if (Nums.Count == 2)
            {
                result = SolveOperation(Nums[0], Nums[1], Operator);
                ResultText.Text = result.ToString();
                Nums.Clear();
                CurrentNum = result.ToString();
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

        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber("0");
        }

        private void NegateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            AddShowNumber(".");
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {

            ShowResult();

        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {

            MakeOperation("+");
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation("-");
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation("/");
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentNum = "";
            ResultText.Text = CurrentNum;
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PorcentajeButton_Click(object sender, RoutedEventArgs e)
        {
            MakeOperation("%");
        }

        private void ReciprocalButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {

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
