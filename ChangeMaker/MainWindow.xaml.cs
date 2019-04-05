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
            textBoxAmount.DataContext = viewModel;
        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            richTextOutput.Document.Blocks.Clear();

            var text = new TextRange(richTextBoxInput.Document.ContentStart, richTextBoxInput.Document.ContentEnd).Text;
            var digitsTxt = text.Split(' ');
            var digits = digitsTxt.Select(d => int.Parse(d));


            Algorithm algorithm = new GreedyAlgorithm(digits);
            var result = algorithm.CalculateResult(int.Parse(viewModel.Amount));
            if(result != null)
            {
                richTextOutput.AppendText(string.Join("-", result));
            }
            else
            {
                richTextOutput.AppendText("Cannot solve.");
            }

        }

        private void TextBoxAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }
    }
}
