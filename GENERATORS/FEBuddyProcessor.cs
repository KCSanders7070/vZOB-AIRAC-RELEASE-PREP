using AIRAC_RELEASE_PREP.HELPERS;
using System;
using System.IO;

namespace AIRAC_RELEASE_PREP.GENERATORS
{
    public static class FEBuddyProcessor
    {
        private static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string FEBuddyOutputPath = Path.Combine(DesktopPath, "FE-BUDDY_Output");

        public static void ProcessFEBuddyOutput()
        {
            Console.WriteLine("\nChecking for 'FE-BUDDY_Output' folder...");

            if (Directory.Exists(FEBuddyOutputPath))
            {
                Console.Write($"\n'FE-BUDDY_Output' folder found at {FEBuddyOutputPath}.\nDo you want to use this folder? (yes/no): ");
                string response = Console.ReadLine()?.Trim().ToLower();

                if (response != "yes")
                {
                    Console.Clear();
                    FEBuddyOutputPath = PromptForFEBuddyOutputFolder();
                }
            }
            else
            {
                Console.Clear();
                FEBuddyOutputPath = PromptForFEBuddyOutputFolder();
            }

            VerifyRequiredFiles();
            HandleUploadToVNASFolder();
        }

        private static string PromptForFEBuddyOutputFolder()
        {
            Console.Write("\nPlease provide the full path to the 'FE-BUDDY_Output' folder: ");
            string path = Console.ReadLine()?.Trim();

            while (!Directory.Exists(path))
            {
                Console.WriteLine("\n\tThe specified folder does not exist. Please try again.");
                path = Console.ReadLine()?.Trim();
            }

            Console.Clear();
            return path;
        }

        private static void VerifyRequiredFiles()
        {
            Console.WriteLine("\nVerifying required files in 'FE-BUDDY_Output\\ALIAS'...");

            string aliasPath = Path.Combine(FEBuddyOutputPath, "ALIAS");
            if (!Directory.Exists(aliasPath))
            {
                throw new DirectoryNotFoundException("\t'FE-BUDDY_Output\\ALIAS' directory not found.");
            }

            string[] requiredFiles = {
                "AWY_ALIAS.txt",
                "FAA_CHART_RECALL.txt",
                "ISR_APT.txt",
                "ISR_NAVAID.txt",
                "STAR_DP_Fixes_Alias.txt",
                "TELEPHONY.txt"
            };

            foreach (var file in requiredFiles)
            {
                string filePath = Path.Combine(aliasPath, file);
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"\n\tRequired file not found: {file}");
                }
            }

            Console.WriteLine("\nAll required files are present.");
        }

        public static void ProcessAliasFiles()
        {
            Console.WriteLine("\nProcessing alias files...");

            string uploadToVNASPath = Path.Combine(FEBuddyOutputPath, "UPLOAD_TO_vNAS");
            string aliasPath = Path.Combine(FEBuddyOutputPath, "ALIAS");
            string combinedAliasFilePath = Path.Combine(uploadToVNASPath, "Combined_ZOB_Alias.txt");

            // Ensure upload directory exists
            if (!Directory.Exists(uploadToVNASPath))
            {
                throw new DirectoryNotFoundException($"\n\tUPLOAD_TO_vNAS folder not found at {uploadToVNASPath}.");
            }

            // Step 1: Copy/Download the custom alias file
            if (PreferencesManager.Preferences.AliasFileAccessMethod == "URL")
            {
                Console.WriteLine("\n\tDownloading custom alias file from URL...");
                DownloadFile(PreferencesManager.Preferences.AliasFileUrl, combinedAliasFilePath, PreferencesManager.Preferences.AliasFileUrlToken);
                Console.WriteLine("\tCustom alias file downloaded successfully.\n");
            }
            else if (PreferencesManager.Preferences.AliasFileAccessMethod == "LOCAL")
            {
                Console.WriteLine("\n\tCopying custom alias file...");
                File.Copy(PreferencesManager.Preferences.AliasFilePath, combinedAliasFilePath, true);
                Console.WriteLine("\tCustom alias file copied successfully.\n");

            }

            // Step 2: Append alias files from ALIAS folder
            string[] aliasFiles = {
                "AWY_ALIAS.txt",
                "FAA_CHART_RECALL.txt",
                "ISR_APT.txt",
                "ISR_NAVAID.txt",
                "STAR_DP_Fixes_Alias.txt",
                "TELEPHONY.txt"
            };

            using (var writer = new StreamWriter(combinedAliasFilePath, true))
            {
                foreach (string aliasFile in aliasFiles)
                {
                    string aliasFilePath = Path.Combine(aliasPath, aliasFile);
                    if (File.Exists(aliasFilePath))
                    {
                        Console.WriteLine($"\tAppending data from {aliasFile}...");
                        string[] lines = File.ReadAllLines(aliasFilePath);
                        foreach (string line in lines)
                        {
                            writer.WriteLine(line);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\tWarning: Alias file not found: {aliasFile}");
                    }
                }
            }

            Console.WriteLine($"\nCombined alias file created at:\n\t{combinedAliasFilePath}");
        }

        private static void DownloadFile(string url, string destinationPath, string token = null)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    }

                    var response = client.GetAsync(url).Result;
                    response.EnsureSuccessStatusCode();

                    var content = response.Content.ReadAsByteArrayAsync().Result;
                    File.WriteAllBytes(destinationPath, content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error downloading file from URL: {ex.Message}");
                throw;
            }
        }

        private static void HandleUploadToVNASFolder()
        {
            string uploadToVNASPath = Path.Combine(FEBuddyOutputPath, "UPLOAD_TO_vNAS");

            if (Directory.Exists(uploadToVNASPath))
            {
                Console.WriteLine($"\n'UPLOAD_TO_vNAS' folder already exists at {uploadToVNASPath}.");
                Console.Write("This folder will be deleted and recreated. Do you want to continue? (yes/no): ");
                string response = Console.ReadLine()?.Trim().ToLower();

                if (response != "yes")
                {
                    Console.WriteLine("Operation canceled. Exiting program.");
                    Environment.Exit(0);
                }

                Directory.Delete(uploadToVNASPath, true);
            }

            Directory.CreateDirectory(uploadToVNASPath);
            Console.WriteLine("\n'UPLOAD_TO_vNAS' folder created successfully.");

            string videoMapsPath = Path.Combine(uploadToVNASPath, "VIDEO_MAPS");
            Directory.CreateDirectory(videoMapsPath);
            Console.WriteLine("\n'VIDEO_MAPS' subfolder created successfully.\n");
        }
    }
}
