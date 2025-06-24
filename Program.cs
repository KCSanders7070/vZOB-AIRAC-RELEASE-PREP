using System;

namespace AIRAC_RELEASE_PREP
{
    internal class Program
    {
        // TODO: Update version number
        private const string CurrentVersion = "1.0";
        static void Main(string[] args)
        {
            Console.Title = $"vZOB AIRAC RLS Prep (v{CurrentVersion})";

            try
            {
                // Step 1: Initialize and load preferences
                HELPERS.PreferencesManager.Initialize();

                // Step 2: Process FE-Buddy Output Directory
                GENERATORS.FEBuddyProcessor.ProcessFEBuddyOutput();

                // Step 3: Process alias files
                GENERATORS.FEBuddyProcessor.ProcessAliasFiles();

                // Step 4: Handle .geojson files
                PARSERS.GeoJsonProcessor.ProcessGeoJsonFiles();

                Console.WriteLine("\nAIRAC RELEASE PREP completed successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
