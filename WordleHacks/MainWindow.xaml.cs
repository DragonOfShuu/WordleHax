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
            update_pin();
        }

        private void calculate_button_Click(object sender, RoutedEventArgs e)
        {
            calculate();
        }
        private void calculate()
        {
            if (
                Pos_index_0.Text != string.Empty ||
                Pos_index_2.Text != string.Empty ||
                Pos_index_3.Text != string.Empty ||
                Pos_index_4.Text != string.Empty ||
                present_characters.Text != string.Empty ||
                absent_characters.Text != string.Empty
                )
            {
                string text;
                using (var sr = new StreamReader("WordleWords.json"))
                {
                    text = sr.ReadToEnd();
                }

                /*Uri uri = new Uri("/WordleWords.json", UriKind.Relative);
                StreamResourceInfo info = Application.GetContentStream(uri);*/

                var listString = JsonSerializer.Deserialize<string[]>(text);

                var pres_chars = present_characters.Text;
                var abs_chars = absent_characters.Text;
                string[] pos_chars = new string[] { 
                    Pos_index_0.Text, 
                    Pos_index_1.Text, 
                    Pos_index_2.Text, 
                    Pos_index_3.Text, 
                    Pos_index_4.Text 
                };
                List<string> possible_words = new List<string>();

                // Containment Start

                for (var i = 0; i < listString.Length; i++)
                {
                    string str = listString[i].ToUpper();

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
                    }
                }
                
                // Containment Area End

                PossibleWords PossibleWindow = new PossibleWords();
                // PossibleWindow.textLocation.Text 
                string newText = "";
                for (var i = 0; i < possible_words.Count; i++)
                {
                    newText = newText + possible_words[i] + "\n";
                }
                PossibleWindow.textLocation.Text = newText;
                PossibleWindow.percentage.Text = string.Concat(Math.Round(((float)possible_words.Count / (float)listString.Length)*100, 2).ToString(), "%");

                PossibleWindow.Topmost = App.get_save().isPinned;
                PossibleWindow.ShowDialog();
            } 
            else
            {
                MessageBox.Show("At least one box must contain content.", "More Information required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void clearPos_Click(object sender, RoutedEventArgs e)
        {
            clear_pos();
        }

        private void clear_pos()
        {
            Pos_index_0.Text = string.Empty;
            Pos_index_1.Text = string.Empty;
            Pos_index_2.Text = string.Empty;
            Pos_index_3.Text = string.Empty;
            Pos_index_4.Text = string.Empty;
        }

        private void clearPres_Click(object sender, RoutedEventArgs e)
        {
            present_characters.Text = string.Empty;
        }

        private void clearAbs_Click(object sender, RoutedEventArgs e)
        {
            absent_characters.Text = string.Empty;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                calculate();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit the program?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                this.Close();
            }
            
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
            Trace.WriteLine("Text Changed.");
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

        private void clearAll_Click(object sender, RoutedEventArgs e)
        {
            clear_pos();
            present_characters.Text = string.Empty;
            absent_characters.Text = string.Empty;
        }

        private void update_pin()
        {
            if (App.get_save().isPinned)
            {
                this.Topmost = true;
                pinWindow.Background = Brushes.White;
                pinWindow.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222233"));
            }
            else
            {
                this.Topmost = false;
                pinWindow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#222233"));
                pinWindow.Foreground = Brushes.White;
            }
        }

        private void pinWindow_Click(object sender, RoutedEventArgs e)
        {
            if (!App.get_save().isPinned)
            {
                saveData save = App.get_save();
                save.isPinned = true;
                App.set_save(save);

                update_pin();
            } 
            else
            {
                saveData save = App.get_save();
                save.isPinned= false;
                App.set_save(save);

                update_pin();
            }
        }

        private void if_back_send_back(TextBox textBox, UIElement focusObject, KeyEventArgs e)
        {
            if (
                e.Key == Key.Back &&
                textBox.Text == ""
                )
            {
                focusObject.Focus();
            }
        }

        private void Pos_index_1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if_back_send_back(sender as TextBox, Pos_index_0, e);
        }

        private void Pos_index_2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if_back_send_back(sender as TextBox, Pos_index_1, e);
        }

        private void Pos_index_3_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if_back_send_back(sender as TextBox, Pos_index_2, e);
        }

        private void Pos_index_4_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if_back_send_back(sender as TextBox, Pos_index_3, e);
        }

        private void present_characters_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if_back_send_back(sender as TextBox, Pos_index_4, e);
        }

        private void absent_characters_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if_back_send_back(sender as TextBox, present_characters, e);
        }
    }
}