// using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
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
using System.Windows.Resources;
using Newtonsoft.Json.Linq;

// dynamic magic = JsonConvert.DeserializeObject(jsonStr);

namespace WordleHacks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Pos_index_0_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pos_index_0.Text != "")
            {
                Pos_index_1.Focus();
            }
        }

        private void Pos_index_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pos_index_1.Text != "")
            {
                Pos_index_2.Focus();
            }
        }

        private void Pos_index_2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pos_index_2.Text != "")
            {
                Pos_index_3.Focus();
            }
        }

        private void Pos_index_3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pos_index_3.Text != "")
            {
                Pos_index_4.Focus();
            }
        }

        private void Pos_index_4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Pos_index_4.Text != "")
            {
                present_characters.Focus();
            }
        }

        private void calculate_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string text;
                using (var sr = new StreamReader("WordleWords.json"))
                {
                    text = sr.ReadToEnd();
                }
                Trace.WriteLine(text);

                /*Uri uri = new Uri("/WordleWords.json", UriKind.Relative);
                StreamResourceInfo info = Application.GetContentStream(uri);*/
                var listString = JsonSerializer.Deserialize<string[]>(text);
                Trace.WriteLine(listString[4]);
            } 
            catch (IOException error)
            {
                Trace.WriteLine("File not found...");
                Trace.WriteLine(error.ToString());
            }
        }
    }
}