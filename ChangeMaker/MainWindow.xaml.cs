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
        }

        private void ButtonCompute_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ExeTimeGreedy = string.Empty;

            var text = new TextRange(richTextBoxInput.Document.ContentStart, richTextBoxInput.Document.ContentEnd).Text;
            var digitsTxt = text.Split(' ');
            var digits = digitsTxt.Select(d => int.Parse(d));


            Algorithm algorithm = new GreedyAlgorithm(digits);


            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = algorithm.CalculateResult(float.Parse(viewModel.Amount));
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            viewModel.ExeTimeGreedy = $"{elapsedMs.ToString()}ms";

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

        private void TextBoxAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }
    }
}
