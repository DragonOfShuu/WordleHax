using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace WordleHacks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public class saveData
    {
        public bool isPinned { get; set; }
        public int fontSize { get; set; }
    }

    public partial class App : Application
    {
        public static bool set_save(saveData values)
        {
            using (StreamWriter sw = File.CreateText("saveData.json"))
            {
                Trace.WriteLine($"Saving to file: {JsonSerializer.Serialize(values)}");
                sw.Write(JsonSerializer.Serialize(values));
            }
            return true;
        }

        public static saveData get_save()
        {
            saveData? value;
            using (StreamReader sr = File.OpenText("saveData.json"))
            {
                string text = sr.ReadToEnd();
                Trace.WriteLine($"Read from file: {text}");
                value = JsonSerializer.Deserialize<saveData>(text);
            }
            return value;
        }
    }
}
