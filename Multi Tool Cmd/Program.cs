using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Win32;
using System.Reflection;

namespace Multi_Tool_Cmd
{
    internal class Program
    {
        private static List<Settingsfor> Settings;

        static void LoadSettings()
        {
            string jsonFilePath = "Settings/Settings.json"; // Path to JSON

            try
            {
                string jsonString = File.ReadAllText(jsonFilePath);
                Settings = JsonSerializer.Deserialize<List<Settingsfor>>(jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding user code: {ex.Message}");
                Settings = new List<Settingsfor>(); // Initialize an empty list if there's an error
            }
        }

        static void RemoveFromStartup(string appName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            // Remove the key with appname
            if (key.GetValue(appName) != null)
            {
                key.DeleteValue(appName);
            }
            else
            {
                Console.WriteLine($"O aplicativo '{appName}' não está no Startup.");
            }
        }

        static void AddToStartup(string appName, string appPath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            key.SetValue(appName, appPath);
        }

        static int MainMenu()
        {
            LoadSettings();

            // Assuming you want to use the first Settings item
            Settingsfor currentSettings = Settings[0];

            if (currentSettings.Startup)
            {
                string appName = "bindFormulti";
                string appPath = Assembly.GetExecutingAssembly().Location;

                //Add app to startup
                AddToStartup(appName, appPath);
            }
            if (!currentSettings.Startup)
            {
                RemoveFromStartup("bindFormulti");
            }

            Console.Title = "Multi Tool Cmd";

            //ASCII Draw
            Console.WriteLine();
            Console.WriteLine();
            if (currentSettings.Color == 1) { 
                Console.ForegroundColor = ConsoleColor.Magenta;
            } 
            else if (currentSettings.Color == 2)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (currentSettings.Color == 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (currentSettings.Color == 4)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (currentSettings.Color == 5)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write($" ███▄ ▄███▓ █    ██  ██▓  ▄▄▄█████▓   ▄▄▄█████▓ ▒█████   ▒█████   ██▓       \r\n▓██▒▀█▀ ██▒ ██  ▓██▒▓██▒  ▓  ██▒ ▓▒   ▓  ██▒ ▓▒▒██▒  ██▒▒██▒  ██▒▓██▒       \r\n▓██    ▓██░▓██  ▒██░▒██░  ▒ ▓██░ ▒░   ▒ ▓██░ ▒░▒██░  ██▒▒██░  ██▒▒██░       \r\n▒██    ▒██ ▓▓█  ░██░▒██░  ░ ▓██▓ ░    ░ ▓██▓ ░ ▒██   ██░▒██   ██░▒██░       \r\n▒██▒   ░██▒▒▒█████▓ ░██████▒▒██▒ ░      ▒██▒ ░ ░ ████▓▒░░ ████▓▒░░██████▒   \r\n░ ▒░   ░  ░░▒▓▒ ▒ ▒ ░ ▒░▓  ░▒ ░░        ▒ ░░   ░ ▒░▒░▒░ ░ ▒░▒░▒░ ░ ▒░▓  ░   \r\n░  ░      ░░░▒░ ░ ░ ░ ░ ▒  ░  ░           ░      ░ ▒ ▒░   ░ ▒ ▒░ ░ ░ ▒  ░   \r\n░      ░    ░░░ ░ ░   ░ ░   ░           ░      ░ ░ ░ ▒  ░ ░ ░ ▒    ░ ░      \r\n       ░      ░         ░  ░                       ░ ░      ░ ░      ░  ░   \r\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();

            //Options

            if (Settings.Count == 0)
            {
                Console.WriteLine("No settings available.");
                return -1;
            }

            string[] options;

            if (currentSettings.Style == 1)
            {
                options = new string[]
                {                                                                  //idk why 4 is not aligned but is work
                    $"Option (1): {currentSettings.Option1}                        Option (4): {currentSettings.Option4}",
                    $"Option (2): {currentSettings.Option2}                            Option (5): {currentSettings.Option5}",
                    $"Option (3): {currentSettings.Option3}                            Option (6): {currentSettings.Option6}",
                    $"Option (7): Settings"
                };
            }
            else if (currentSettings.Style == 2)
            {
                options = new string[]
                {                                                                  
                    $"Option (1): {currentSettings.Option1}",
                    $"Option (2): {currentSettings.Option2}",
                    $"Option (3): {currentSettings.Option3}",
                    $"Option (4): {currentSettings.Option4}",
                    $"Option (5): {currentSettings.Option5}",
                    $"Option (3): {currentSettings.Option3}",
                    $"Option (7): Settings"
                };
            }
            else if (currentSettings.Style == 3)
            {
                options = new string[]
                {
                };
            }
            else
            {
                options = new string[]
                {                                                                  //idk why 4 is not aligned but is work
                    $"Option (1): {currentSettings.Option1}                        Option (4): {currentSettings.Option4}",
                    $"Option (2): {currentSettings.Option2}                            Option (5): {currentSettings.Option5}",
                    $"Option (3): {currentSettings.Option3}                            Option (6): {currentSettings.Option6}",
                    $"Option (7): Settings"
                };
            }

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }

            Console.Write(">");
            string awnser = Console.ReadLine();
            int a;
            if (int.TryParse(awnser, out a) && a >= 1 && a <= 7) // this <= 7 you can change for options.Length 
            {
                return a;
            }
            else
            {
                Console.WriteLine("Enter a valid option");
                return -1;
            }
        }

        static int SettingsMenu()
        {
            Console.Title = "Settings Tab";

            //ASCII Draw
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("  ██████ ▓█████▄▄▄█████▓▄▄▄█████▓ ██▓ ███▄    █   ▄████   ██████ \r\n▒██    ▒ ▓█   ▀▓  ██▒ ▓▒▓  ██▒ ▓▒▓██▒ ██ ▀█   █  ██▒ ▀█▒▒██    ▒ \r\n░ ▓██▄   ▒███  ▒ ▓██░ ▒░▒ ▓██░ ▒░▒██▒▓██  ▀█ ██▒▒██░▄▄▄░░ ▓██▄   \r\n  ▒   ██▒▒▓█  ▄░ ▓██▓ ░ ░ ▓██▓ ░ ░██░▓██▒  ▐▌██▒░▓█  ██▓  ▒   ██▒\r\n▒██████▒▒░▒████▒ ▒██▒ ░   ▒██▒ ░ ░██░▒██░   ▓██░░▒▓███▀▒▒██████▒▒\r\n▒ ▒▓▒ ▒ ░░░ ▒░ ░ ▒ ░░     ▒ ░░   ░▓  ░ ▒░   ▒ ▒  ░▒   ▒ ▒ ▒▓▒ ▒ ░\r\n░ ░▒  ░ ░ ░ ░  ░   ░        ░     ▒ ░░ ░░   ░ ▒░  ░   ░ ░ ░▒  ░ ░\r\n░  ░  ░     ░    ░        ░       ▒ ░   ░   ░ ░ ░ ░   ░ ░  ░  ░  \r\n      ░     ░  ░                  ░           ░       ░       ░  \r\n                                                                 ");
            Console.WriteLine();
            Console.WriteLine();


            //Options

            string[] options = { "Visit My Github: https://github.com/luckdiamond0\n", "Option (1): Edit Options Name", "Option (2): Go back" };

              for (int i = 0; i < options.Length; i++)
              {
                  Console.WriteLine(options[i]);
              }

              Console.Write(">");
              string awnser = Console.ReadLine();
              int a;
              if (int.TryParse(awnser, out a) && a >= 1 && a <= options.Length)
              {
                  return a;
              }
              else
              {
                  Console.WriteLine("Enter a valid option");
                  return -1;
              }
        }

        static void Main(string[] args) {
            //Open Files
            string App1 = "Files\\App1\\App1.exe";
            
            string App2 = "Files\\App2\\App2.exe";

            string App3 = "Files\\App3\\App3.exe";

            string App4 = "Files\\App4\\App4.exe";

            string App5 = "Files\\App5\\App5.exe";

            string App6 = "Files\\App6\\App6.exe";

            //settingstab
            bool settingson = false; // Turn On or turn off mainmenu

            while (true)
            { 
                if (!settingson)
                {
                    Console.Clear();
                    int option = MainMenu();

                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            Process.Start(App1);
                            break;
                        case 2:
                            Process.Start(App2);
                            break;
                        case 3:
                            Console.Clear();
                            Process.Start(App3);
                            break;
                        case 4:
                            Console.Clear();
                            Process.Start(App4);
                            break;
                        case 5:
                            Console.Clear();
                            Process.Start(App5);
                            break;
                        case 6:
                            Console.Clear();
                            Process.Start(App6);
                            break;
                        case 7:
                            Console.Clear();
                            settingson = true;
                            break;
                        default:
                            Console.WriteLine("Enter a valid option");
                            break;
                    }
                }
                if (settingson)
                {
                    Console.Clear();
                    int optionset = SettingsMenu();

                    string jsonedit = "Settings/Settings.json";

                    switch (optionset)
                    {
                        case 1:
                            Console.Clear();
                            try
                            {
                                if (File.Exists(jsonedit))
                                {
                                    // Open Json in notepad or charmpad lmao 
                                    Process.Start(new ProcessStartInfo
                                    {
                                        FileName = "notepad.exe",
                                        Arguments = jsonedit,
                                        UseShellExecute = true
                                    });
                                }
                                else
                                {
                                    Console.WriteLine("Doest Find: " + jsonedit);
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Error to open " + e.Message);
                            }
                            break;
                        case 2:
                            settingson = false;
                            break;
                        default:
                            Console.WriteLine("Enter a valid option");
                            break;
                    }
                }
            }
        }
    }
}
