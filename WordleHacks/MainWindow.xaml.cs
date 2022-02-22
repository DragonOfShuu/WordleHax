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
                Trace.WriteLine(listString[50]);
                // Trace.WriteLine(listString[4]);

                var pres_chars = present_characters.Text;
                var abs_chars = absent_characters.Text;
                string[] pos_chars = new string[] { Pos_index_0.Text, Pos_index_1.Text, Pos_index_2.Text, Pos_index_3.Text, Pos_index_4.Text };
                List<string> possible_words = new List<string>();

                // Containment Start

                for (var i = 0; i < listString.Length; i++)
                {
                    string str = listString[i].ToUpper();
                    // Trace.WriteLine(str);

                    // If absent chars found, continue to the next word.
                    bool nextWord = false;
                    if (abs_chars != "")
                    {
                        foreach (char c in abs_chars)
                        {
                            if (str.Contains(c))
                            {
                                nextWord = true;
                                break;
                            }
                        }
                        if (nextWord)
                        {
                            continue;
                        }
                    }

                    // If containing characters not found, continue to next word.
                    if (pres_chars != "")
                    {
                        nextWord = false;
                        foreach (char c in pres_chars)
                        {
                            if (!str.Contains(c))
                            {
                                nextWord=true;
                                break;
                            }
                        }
                        if (nextWord)
                        {
                            continue;
                        }
                    }

                    // If positional characters not found, continue to next word.
                    nextWord = false;
                    for (var f = 0; f < pos_chars.Length; f++)
                    {
                        string letter = pos_chars[f];
                        if (letter != "" && letter != str[f].ToString())
                        {
                            nextWord = true;
                            break;
                        }
                    }
                    if (nextWord)
                    {
                        continue;
                    }
                    else
                    {
                        possible_words.Add(str);
                        // Trace.WriteLine(possible_words.Count);
                    }
                }
                
                // Containment Area End

                PossibleWords PossibleWindow = new PossibleWords();
                // PossibleWindow.textLocation.Text 
                string newText = "";
                Trace.WriteLine(possible_words.Count);
                for (var i = 0; i < possible_words.Count; i++)
                {
                    newText = newText + possible_words[i] + "\n";
                }
                PossibleWindow.textLocation.Text = newText;
                PossibleWindow.Show();
            } 
            catch (IOException error)
            {
                Trace.WriteLine("File not found...");
                Trace.WriteLine(error.ToString());
            }
        }
    }
}