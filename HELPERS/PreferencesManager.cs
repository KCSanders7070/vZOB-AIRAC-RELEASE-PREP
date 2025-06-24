using System;
using System.IO;
using System.Text.Json;

namespace AIRAC_RELEASE_PREP.HELPERS
{
    public static class PreferencesManager
    {
        private static readonly string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AIRAC_RELEASE_PREP");
        private static readonly string PreferencesFilePath = Path.Combine(AppDataPath, "AIRAC_RELEASE_PREP_Pref.json");

        public static Preferences Preferences { get; private set; }

        public static void Initialize()
        {
            Console.WriteLine("Checking for preferences...");

            if (File.Exists(PreferencesFilePath))
            {
                Console.WriteLine("\tPreferences file found. Reading preferences...");
                ReadPreferences();
                DisplayPreferences();

                if (!VerifyPreferences())
                {
                    ResetPreferences();
                }

                Console.Clear();
            }
            else
            {
                Console.WriteLine("\tPreferences file not found. Creating new preferences...");
                Preferences = GatherPreferencesFromUser();
                SavePreferences();
            }
        }

        private static void ReadPreferences()
        {
            try
            {
                string json = File.ReadAllText(PreferencesFilePath);
                Preferences = JsonSerializer.Deserialize<Preferences>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading preferences: {ex.Message}");
                Preferences = null;
            }
        }

        private static void DisplayPreferences()
        {
            Console.WriteLine("\n\nCurrent Preferences:");

            // Display Alias File Information
            if (Preferences.AliasFileAccessMethod == "LOCAL")
            {
                Console.WriteLine("\n\tYour custom facility Alias file will be copied from here:");
                Console.WriteLine($"\t\t\"{Preferences.AliasFilePath}\"");
            }
            else if (Preferences.AliasFileAccessMethod == "URL")
            {
                Console.WriteLine($"\n\tYour custom facility Alias file will be downloaded from here:\"{Preferences.AliasFileUrl}\"");

                if (string.IsNullOrWhiteSpace(Preferences.AliasFileUrlToken))
                {
                    Console.WriteLine("\t\tNote: A token is not required for this download.");
                }
                else
                {
                    Console.WriteLine($"\t\tToken: \"{Preferences.AliasFileUrlToken}\"");
                }
            }

            // Display Bounding Box Information
            Console.WriteLine("\n\tBounding Box data...");
            Console.WriteLine("\t\tSouthwestCorner:");
            Console.WriteLine($"\t\t\tLatitude: {Preferences.BoundingBox.SouthwestCorner.Latitude},");
            Console.WriteLine($"\t\t\tLongitude: {Preferences.BoundingBox.SouthwestCorner.Longitude}");
            Console.WriteLine("\n\t\tNortheastCorner:");
            Console.WriteLine($"\t\t\tLatitude: {Preferences.BoundingBox.NortheastCorner.Latitude},");
            Console.WriteLine($"\t\t\tLongitude: {Preferences.BoundingBox.NortheastCorner.Longitude}");

            // Prompt the user to confirm the preferences
            Console.Write("\nDo these preferences still apply? (yes/no): ");
            string response = Console.ReadLine()?.Trim().ToLower();

            if (response != "yes")
            {
                ResetPreferences();
            }
        }

        private static bool VerifyPreferences()
        {
            if (Preferences == null || Preferences.BoundingBox == null)
                return false;

            // TODO: Add specific validation checks for preferences here
            return true;
        }

        private static void ResetPreferences()
        {
            Console.WriteLine("\nResetting preferences...");
            Console.WriteLine("\tCopy/paste the current preferences somewhere if needed.");
            Console.WriteLine("\tType 'reset' to confirm reset: ");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "reset")
            {
                File.Delete(PreferencesFilePath);
                Console.Clear();
                Preferences = GatherPreferencesFromUser();
                SavePreferences();
            }
            else
            {
                Console.WriteLine("\tReset canceled. Exiting program.");
                Environment.Exit(0);
            }
        }

        private static Preferences GatherPreferencesFromUser()
        {
            var preferences = new Preferences();

            Console.Write("\nWill the alias file be accessed via URL or locally (URL/LOCAL)? ");
            preferences.AliasFileAccessMethod = Console.ReadLine()?.Trim().ToUpper();

            if (preferences.AliasFileAccessMethod == "URL")
            {
                Console.Write("Enter the alias file URL: ");
                preferences.AliasFileUrl = SanitizePath(Console.ReadLine()?.Trim());

                Console.Write("\nDoes the URL require a token? (yes/no): ");
                string needsToken = Console.ReadLine()?.Trim().ToLower();
                if (needsToken == "yes")
                {
                    Console.Write("Enter the token: ");
                    preferences.AliasFileUrlToken = Console.ReadLine()?.Trim();
                }
            }
            else if (preferences.AliasFileAccessMethod == "LOCAL")
            {
                Console.Write("Enter the full path to the alias file: ");
                preferences.AliasFilePath = SanitizePath(Console.ReadLine()?.Trim());

                while (!File.Exists(preferences.AliasFilePath))
                {
                    Console.WriteLine("\tFile not found. Please try again.");
                    preferences.AliasFilePath = SanitizePath(Console.ReadLine()?.Trim());
                }
            }

            Console.WriteLine("\nEnter the bounding box coordinates:");
            Console.Write("\tSouthwest Corner Latitude: ");
            preferences.BoundingBox.SouthwestCorner.Latitude = Convert.ToDouble(Console.ReadLine()?.Trim());

            Console.Write("\tSouthwest Corner Longitude: ");
            preferences.BoundingBox.SouthwestCorner.Longitude = Convert.ToDouble(Console.ReadLine()?.Trim());

            Console.Write("\tNortheast Corner Latitude: ");
            preferences.BoundingBox.NortheastCorner.Latitude = Convert.ToDouble(Console.ReadLine()?.Trim());

            Console.Write("\tNortheast Corner Longitude: ");
            preferences.BoundingBox.NortheastCorner.Longitude = Convert.ToDouble(Console.ReadLine()?.Trim());

            return preferences;
        }

        private static void SavePreferences()
        {
            try
            {
                if (!Directory.Exists(AppDataPath))
                {
                    Directory.CreateDirectory(AppDataPath);
                }

                string json = JsonSerializer.Serialize(Preferences, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(PreferencesFilePath, json);
                Console.WriteLine("Preferences saved successfully.");
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving preferences: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes surrounding quotes from a file path, if present.
        /// </summary>
        private static string SanitizePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return path;

            // Remove surrounding quotes, if they exist
            if (path.StartsWith("\"") && path.EndsWith("\""))
            {
                path = path.Substring(1, path.Length - 2);
            }

            return path.Trim();
        }
    }

    public class Preferences
    {
        public string AliasFileAccessMethod { get; set; }
        public string AliasFileUrl { get; set; }
        public string AliasFileUrlToken { get; set; }
        public string AliasFilePath { get; set; }
        public BoundingBox BoundingBox { get; set; } = new BoundingBox(); // Initialize BoundingBox
    }

    public class BoundingBox
    {
        public Coordinates SouthwestCorner { get; set; } = new Coordinates(); // Initialize SouthwestCorner
        public Coordinates NortheastCorner { get; set; } = new Coordinates(); // Initialize NortheastCorner
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}
