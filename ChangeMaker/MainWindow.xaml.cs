using ChangeMaker.Helpers;
using ChangeMaker.Logic;
using ChangeMaker.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = new TextRange(richTextBoxInput.Document.ContentStart, richTextBoxInput.Document.ContentEnd);
            if(text.Text.Length > 5)
            {
                var text1 = new TextRange(richTextBoxInput.Document.ContentStart.GetNextContextPosition(LogicalDirection.Forward), richTextBoxInput.Document.ContentEnd);
                text1.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            }
        }


        private void ButtonCompute_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();

            viewModel.ExeTimeGreedy = string.Empty;

            var text = new TextRange(richTextBoxInput.Document.ContentStart, richTextBoxInput.Document.ContentEnd).Text;
            var digits = text.ParseCoins();

            var valueToCalculate = viewModel.Amount.ParseSingeValue();

            if (valueToCalculate == null) return;


            Algorithm algorithm = new GreedyAlgorithm(digits);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = algorithm.CalculateResult((float)valueToCalculate);
            watch.Stop();
            viewModel.ExeTimeGreedy = watch.Elapsed.TotalMilliseconds.ToString();

            listGreedy.Items.Clear();
            if (result != null)
            {
                var grResult = result.GroupBy(l => l).Select(g => new { coin = g.Key, count = g.Count() });
                foreach (var r in grResult)
                {
                    var item = new ListBoxItem();
                    item.Content = $"{r.count} x {r.coin}";
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
    }
}
