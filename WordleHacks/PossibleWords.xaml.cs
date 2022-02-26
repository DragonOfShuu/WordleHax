using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WordleHacks
{
    /// <summary>
    /// Interaction logic for PossibleWords.xaml
    /// </summary>
    public partial class PossibleWords : Window
    {
        public PossibleWords()
        {
            InitializeComponent();
            okButton.Focus();
            Trace.WriteLine($"Fontsize: {App.get_save().fontSize} on possible words");
            textLocation.FontSize = FontSizeSlider.Value = App.get_save().fontSize;
            FontSizeSlider.ValueChanged += Slider_ValueChanged;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            saveData value = App.get_save();
            textLocation.FontSize = e.NewValue;
            value.fontSize = (int) e.NewValue;
            Trace.WriteLine($"Slider Changed to: {value.fontSize}");
            App.set_save(value);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
