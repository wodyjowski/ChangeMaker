using ChangeMaker.Helpers;
using ChangeMaker.Logic;
using ChangeMaker.Logic.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace ChangeMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel viewModel = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ButtonCompute_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();

            viewModel.ExeTimeGreedy = string.Empty;

            var digits = viewModel.CoinString.ParseCoins();

            var valueToCalculate = viewModel.Amount.ParseSingeValue();

            if (valueToCalculate == null) return;

            if (viewModel.Greedy)
            {
                ComputeGreedy(digits, (float)valueToCalculate);
            }

            if (viewModel.Dynamic)
            {
                ComputeDynamic(digits, (float)valueToCalculate);
            }

        }

        private async void ComputeDynamic(IEnumerable<int> digits, float valueToCalculate)
        {
            DynamicAlgorithm algorithm = new DynamicAlgorithm(digits);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = await Task.Run(() =>
            {
                return algorithm.CalculateResult(valueToCalculate);
            });
            watch.Stop();

            if (viewModel.Time) viewModel.ExeTimeDynamic = $"{watch.Elapsed.TotalMilliseconds.ToString()}ms";

            listDynamic.Items.Clear();
            if (result != null)
            {
                foreach (var r in result)
                {
                    var item = new ListBoxItem();
                    item.Content = $"{r.Value} x {r.Key}";
                    listDynamic.Items.Add(item);
                }

                DynamicAlgorithm greedyAlg = (DynamicAlgorithm) algorithm;


                Debug.WriteLine($"Arrays for input: {valueToCalculate}");
                Debug.WriteLine("----------------------------------");
                for (int i = 1; i < greedyAlg.oArray.Length; i++)
                {
                    Debug.WriteLine($"amount: {i} -> o:{greedyAlg.oArray[i]}   s:{greedyAlg.sArray[i]}  ({greedyAlg.coinArray[greedyAlg.sArray[i]]})");
                }

            }
            else
            {
                var item = new ListBoxItem();
                item.Content = "Cannot solve";
                listDynamic.Items.Add(item);
            }
        }

        private async void ComputeGreedy(IEnumerable<int> digits, float valueToCalculate)
        {
            GreedyAlgorithm algorithm = new GreedyAlgorithm(digits);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = await Task.Run(() =>
            {
                return algorithm.CalculateResult(valueToCalculate);
            });
            watch.Stop();

            if (viewModel.Time) viewModel.ExeTimeGreedy = $"{watch.Elapsed.TotalMilliseconds.ToString()}ms";

            listGreedy.Items.Clear();
            if (result != null)
            {
                foreach (var r in result)
                {
                    var item = new ListBoxItem();
                    item.Content = $"{r.Value} x {r.Key}";
                    listGreedy.Items.Add(item);
                }
            }
            else
            {
                var item = new ListBoxItem();
                item.Content = "Cannot solve";
                listGreedy.Items.Add(item);
            }
        }



        private void ClearOutput()
        {
            listGreedy.Items.Clear();
            listDynamic.Items.Clear();
            viewModel.ExeTimeGreedy = string.Empty;
            viewModel.ExeTimeDynamic = string.Empty;
        }

        private void TextBoxAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }

        private void TextBoxCoin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d+");
        }
    }
}
