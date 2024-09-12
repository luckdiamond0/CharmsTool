using System;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Win32;
using System.Reflection;
using System.Xml;

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

        static void SaveSettings()
        {
            string jsonFilePath = "Settings/Settings.json";

            string json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });

            // Wirte on Json
            File.WriteAllText(jsonFilePath, json);
        }

        static void RemoveFromStartup(string appName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            // Remove the key with appname
            if (key.GetValue(appName) != null)
            {
                key.DeleteValue(appName);
            }
        }

        static string GetBindForMultiAppPath()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string appPath = System.IO.Path.Combine(currentDirectory, "bindFormulti.exe");

            return appPath;
        }

        static void AddToStartup(string appName, string appPath)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            key.SetValue(appName, appPath);
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

            string[] options = { "Visit My Github: https://github.com/luckdiamond0\n", "Option (1): Edit Options", "Option (2): Edit Options json", "Option (3): Go back" };

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

        static int EditMenu(ref bool optionsoncmd, ref bool moreoptions)
        {
            LoadSettings();

            // Assuming you want to use the first Settings item
            Settingsfor currentSettings = Settings[0];

            Console.Title = "Edit Tab";

            //ASCII Draw
            Console.WriteLine();
            Console.WriteLine();
            if (currentSettings.Color == 1)
            {
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
            Console.Write(" ▓█████ ▓█████▄  ██▓▄▄▄█████▓   ▄▄▄█████▓ ▄▄▄       ▄▄▄▄   \r\n▓█   ▀ ▒██▀ ██▌▓██▒▓  ██▒ ▓▒   ▓  ██▒ ▓▒▒████▄    ▓█████▄ \r\n▒███   ░██   █▌▒██▒▒ ▓██░ ▒░   ▒ ▓██░ ▒░▒██  ▀█▄  ▒██▒ ▄██\r\n▒▓█  ▄ ░▓█▄   ▌░██░░ ▓██▓ ░    ░ ▓██▓ ░ ░██▄▄▄▄██ ▒██░█▀  \r\n░▒████▒░▒████▓ ░██░  ▒██▒ ░      ▒██▒ ░  ▓█   ▓██▒░▓█  ▀█▓\r\n░░ ▒░ ░ ▒▒▓  ▒ ░▓    ▒ ░░        ▒ ░░    ▒▒   ▓▒█░░▒▓███▀▒\r\n ░ ░  ░ ░ ▒  ▒  ▒ ░    ░           ░      ▒   ▒▒ ░▒░▒   ░ \r\n   ░    ░ ░  ░  ▒ ░  ░           ░        ░   ▒    ░    ░ \r\n   ░  ░   ░     ░                             ░  ░ ░      \r\n        ░                                               ░ ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();

            //Options

            string[] options = { 
                    $"Change Option (1): {currentSettings.Option1}",
                    $"Change Option (2): {currentSettings.Option2}",
                    $"Change Option (3): {currentSettings.Option3}",
                    $"Change Option (4): {currentSettings.Option4}",
                    $"Change Option (5): {currentSettings.Option5}",
                    $"Change Option (6): {currentSettings.Option6}",
                    $"Change Style (7): {currentSettings.Style}",
                    $"Change Color (8): {currentSettings.Color}",
                    $"Change Startup (9): {currentSettings.Startup}",
                    $"\nMore Configurations(10)",
                    $"Help (11)",
                    $"Go Back (12)"
            };

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }



            Console.Write(">");
            string answer = Console.ReadLine();
            int selection;

            if (int.TryParse(answer, out selection) && selection >= 1 && selection <= options.Length)
            {
                switch (selection)
                {
                    case 1:
                        Console.Write("Edit Option 1: ");
                        currentSettings.Option1 = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Edit Option 2: ");
                        currentSettings.Option2 = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Edit Option 3: ");
                        currentSettings.Option3 = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Edit Option 4: ");
                        currentSettings.Option4 = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Edit Option 5: ");
                        currentSettings.Option5 = Console.ReadLine();
                        break;
                    case 6:
                        Console.Write("Edit Option 6: ");
                        currentSettings.Option6 = Console.ReadLine();
                        break;
                    case 7:
                        Console.Write("Edit style: ");
                        currentSettings.Style = int.Parse(Console.ReadLine());
                        break;
                    case 8:
                        Console.Write("Edit color: ");
                        currentSettings.Color = int.Parse(Console.ReadLine());
                        break;
                    case 9:
                        Console.Write("Change Startup (true/false): ");
                        currentSettings.Startup = bool.Parse(Console.ReadLine());
                        break;
                    case 10:
                        Console.Clear();
                        optionsoncmd = false; // turn off this page
                        moreoptions = true; //turn on the more settings page
                        break;
                    case 11:
                        Console.Clear();
                        Helptab();
                        break;
                    case 12:
                        optionsoncmd = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        return -1;
                }

                // Save the updated settings
                SaveSettings();
                return selection;
            }
            else
            {
                Console.WriteLine("Enter a valid option");
                return -1;
            }
        }

        static int MoreConfig(ref bool optionsoncmd, ref bool moreoptions)
        {
            Console.Clear();

            LoadSettings();

            // Assuming you want to use the first Settings item
            Settingsfor currentSettings = Settings[0];

            Console.Title = "More Config Tab";

            //ASCII Draw
            Console.WriteLine();
            Console.WriteLine();
            if (currentSettings.Color == 1)
            {
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
            Console.Write(" ███▄ ▄███▓ ▒█████   ██▀███  ▓█████      ██████ ▓█████▄▄▄█████▓▄▄▄█████▓ ██▓ ███▄    █   ▄████   ██████ \r\n▓██▒▀█▀ ██▒▒██▒  ██▒▓██ ▒ ██▒▓█   ▀    ▒██    ▒ ▓█   ▀▓  ██▒ ▓▒▓  ██▒ ▓▒▓██▒ ██ ▀█   █  ██▒ ▀█▒▒██    ▒ \r\n▓██    ▓██░▒██░  ██▒▓██ ░▄█ ▒▒███      ░ ▓██▄   ▒███  ▒ ▓██░ ▒░▒ ▓██░ ▒░▒██▒▓██  ▀█ ██▒▒██░▄▄▄░░ ▓██▄   \r\n▒██    ▒██ ▒██   ██░▒██▀▀█▄  ▒▓█  ▄      ▒   ██▒▒▓█  ▄░ ▓██▓ ░ ░ ▓██▓ ░ ░██░▓██▒  ▐▌██▒░▓█  ██▓  ▒   ██▒\r\n▒██▒   ░██▒░ ████▓▒░░██▓ ▒██▒░▒████▒   ▒██████▒▒░▒████▒ ▒██▒ ░   ▒██▒ ░ ░██░▒██░   ▓██░░▒▓███▀▒▒██████▒▒\r\n░ ▒░   ░  ░░ ▒░▒░▒░ ░ ▒▓ ░▒▓░░░ ▒░ ░   ▒ ▒▓▒ ▒ ░░░ ▒░ ░ ▒ ░░     ▒ ░░   ░▓  ░ ▒░   ▒ ▒  ░▒   ▒ ▒ ▒▓▒ ▒ ░\r\n░  ░      ░  ░ ▒ ▒░   ░▒ ░ ▒░ ░ ░  ░   ░ ░▒  ░ ░ ░ ░  ░   ░        ░     ▒ ░░ ░░   ░ ▒░  ░   ░ ░ ░▒  ░ ░\r\n░      ░   ░ ░ ░ ▒    ░░   ░    ░      ░  ░  ░     ░    ░        ░       ▒ ░   ░   ░ ░ ░ ░   ░ ░  ░  ░  \r\n       ░       ░ ░     ░        ░  ░         ░     ░  ░                  ░           ░       ░       ░  \r\n                                                                                                        ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();

            //Options

            string[] options = {
                    $"Change Exe Location (1): {currentSettings.ExeLocation1}",
                    $"Change Exe Location (2): {currentSettings.ExeLocation2}",
                    $"Change Exe Location (3): {currentSettings.ExeLocation3}",
                    $"Change Exe Location (4): {currentSettings.ExeLocation4}",
                    $"Change Exe Location (5): {currentSettings.ExeLocation5}",
                    $"Change Exe Location (6): {currentSettings.ExeLocation6}",
                    "\n(To save is necessary to exit)",
                    $"Save(7)",
                    $"Go Back (8)"
            };

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine(options[i]);
            }



            Console.Write(">");
            string answer = Console.ReadLine();
            int selection;

            if (int.TryParse(answer, out selection) && selection >= 1 && selection <= options.Length)
            {
                switch (selection)
                {
                    case 1:
                        Console.Write("Edit Exe Location 1: ");
                        currentSettings.ExeLocation1 = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Edit Exe Location 2: ");
                        currentSettings.ExeLocation2 = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Edit Exe Location 3: ");
                        currentSettings.ExeLocation3 = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Edit Exe Location 4: ");
                        currentSettings.ExeLocation4 = Console.ReadLine();
                        break;
                    case 5:
                        Console.Write("Edit Exe Location 5: ");
                        currentSettings.ExeLocation5 = Console.ReadLine();
                        break;
                    case 6:
                        Console.Write("Edit Exe Location 6: ");
                        currentSettings.ExeLocation6 = Console.ReadLine();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    case 8:
                        optionsoncmd = true; //turn on the settings page
                        moreoptions = false; // turn off this page
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        return -1;
                }

                // Save the updated settings
                SaveSettings();
                return selection;
            }
            else
            {
                Console.WriteLine("Enter a valid option");
                return -1;
            }
        }

        static int Helptab()
        {
            Console.Title = "Help Tab";

            //ASCII Draw
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("  ██░ ██ ▓█████  ██▓     ██▓███  \r\n▓██░ ██▒▓█   ▀ ▓██▒    ▓██░  ██▒\r\n▒██▀▀██░▒███   ▒██░    ▓██░ ██▓▒\r\n░▓█ ░██ ▒▓█  ▄ ▒██░    ▒██▄█▓▒ ▒\r\n░▓█▒░██▓░▒████▒░██████▒▒██▒ ░  ░\r\n ▒ ░░▒░▒░░ ▒░ ░░ ▒░▓  ░▒▓▒░ ░  ░\r\n ▒ ░▒░ ░ ░ ░  ░░ ░ ▒  ░░▒ ░     \r\n ░  ░░ ░   ░     ░ ░   ░░       \r\n ░  ░  ░   ░  ░    ░  ░         \r\n                                 ");
            Console.WriteLine();
            Console.WriteLine();


            //Options

            string[] options = { 
                "If you want change app, change the ExeLocation not Options, Options is only for names of apps.",
                "\nStyle",
                "1 = 1,2,3,7 in left side and, 4,5,6 in right side",
                "2 = 1,2,3,4,5,6,7 alls together", "3 = none",
                "\nColor",
                "if you want to Change Color is 1 for Magenta, 2 for Cyan, 3 for Red, 4 for Green and 5 for White",
                "\n.Exe folder",
                "if you want to change a .exe folder or change app, you need to go to json or in more settings",
                "And Put your direct, follow this example: C:\\Program Files (x86)\\AppFolder\\App.exe",
                "\nGo Back(Enter)"};

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

        static int MainMenu()
        {
            LoadSettings();

            // Assuming you want to use the first Settings item
            Settingsfor currentSettings = Settings[0];

            string appName = "bindFormulti";
            string appPath = GetBindForMultiAppPath();

            if (currentSettings.Startup)
            {
                //Add app to startup
                AddToStartup(appName, appPath);
            }
            if (!currentSettings.Startup)
            {
                RemoveFromStartup(appName);
            }

            Console.Title = "Multi Tool Cmd";

            //ASCII Draw
            Console.WriteLine();
            Console.WriteLine();
            if (currentSettings.Color == 1)
            {
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
            Console.Write($" ███▄ ▄███▓ █    ██  ██▓  ▄▄▄█████▓ ██▓   ▄▄▄█████▓ ▒█████   ▒█████   ██▓    \r\n▓██▒▀█▀ ██▒ ██  ▓██▒▓██▒  ▓  ██▒ ▓▒▓██▒   ▓  ██▒ ▓▒▒██▒  ██▒▒██▒  ██▒▓██▒    \r\n▓██    ▓██░▓██  ▒██░▒██░  ▒ ▓██░ ▒░▒██▒   ▒ ▓██░ ▒░▒██░  ██▒▒██░  ██▒▒██░    \r\n▒██    ▒██ ▓▓█  ░██░▒██░  ░ ▓██▓ ░ ░██░   ░ ▓██▓ ░ ▒██   ██░▒██   ██░▒██░    \r\n▒██▒   ░██▒▒▒█████▓ ░██████▒▒██▒ ░ ░██░     ▒██▒ ░ ░ ████▓▒░░ ████▓▒░░██████▒\r\n░ ▒░   ░  ░░▒▓▒ ▒ ▒ ░ ▒░▓  ░▒ ░░   ░▓       ▒ ░░   ░ ▒░▒░▒░ ░ ▒░▒░▒░ ░ ▒░▓  ░\r\n░  ░      ░░░▒░ ░ ░ ░ ░ ▒  ░  ░     ▒ ░       ░      ░ ▒ ▒░   ░ ▒ ▒░ ░ ░ ▒  ░\r\n░      ░    ░░░ ░ ░   ░ ░   ░       ▒ ░     ░      ░ ░ ░ ▒  ░ ░ ░ ▒    ░ ░   \r\n       ░      ░         ░  ░        ░                  ░ ░      ░ ░      ░  ░\r\n                                                                             ");
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

        static void Main(string[] args) {
            //json settings 
            LoadSettings();

            // Assuming you want to use the first Settings item
            Settingsfor currentSettings = Settings[0];

            //Open Files
            string App1 = currentSettings.ExeLocation1;
            
            string App2 = currentSettings.ExeLocation2;

            string App3 = currentSettings.ExeLocation3;

            string App4 = currentSettings.ExeLocation4;

            string App5 = currentSettings.ExeLocation5;

            string App6 = currentSettings.ExeLocation6;

            //settingstab
            bool settingson = false; // Turn On or turn off mainmenu
            bool optionsoncmd = false; // Turn On or turn off settings and turn on cmd settings
            bool moreoptions = false; // Turn On or turn off more settings and turn on cmd more settings 

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
                if (settingson && !optionsoncmd && !moreoptions)
                {
                    Console.Clear();
                    int optionset = SettingsMenu();

                    string jsonedit = "Settings/Settings.json";

                    switch (optionset)
                    {
                        case 1:
                            Console.Clear();
                            optionsoncmd = true;
                            break;
                        case 2:
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
                        case 3:
                            settingson = false;
                            break;
                        default:
                            Console.WriteLine("Enter a valid option");
                            break;
                    }
                }
                if (optionsoncmd)
                {
                    Console.Clear();
                    EditMenu(ref optionsoncmd, ref moreoptions);
                }

                if (moreoptions)
                {
                    MoreConfig(ref optionsoncmd,ref moreoptions);
                }
            }
        }
    }
}
