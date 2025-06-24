using System;

namespace AIRAC_RELEASE_PREP.UI
{
    public static class UserInterface
    {
        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays a message to the user without a newline.
        /// </summary>
        public static void DisplayInlineMessage(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Prompts the user for input and returns their response.
        /// </summary>
        public static string PromptInput(string prompt)
        {
            DisplayInlineMessage(prompt + " ");
            return Console.ReadLine()?.Trim();
        }

        /// <summary>
        /// Prompts the user for a yes/no answer and returns true if the answer is yes.
        /// </summary>
        public static bool PromptYesNo(string prompt)
        {
            while (true)
            {
                string input = PromptInput(prompt + " (yes/no)").ToLower();
                if (input == "yes") return true;
                if (input == "no") return false;
                DisplayMessage("Invalid input. Please enter 'yes' or 'no'.");
            }
        }

        /// <summary>
        /// Prompts the user for a double and validates the input.
        /// </summary>
        public static double PromptDouble(string prompt)
        {
            while (true)
            {
                string input = PromptInput(prompt);
                if (double.TryParse(input, out double result))
                {
                    return result;
                }
                DisplayMessage("Invalid input. Please enter a valid number.");
            }
        }

        /// <summary>
        /// Prompts the user for a file path and validates its existence.
        /// </summary>
        public static string PromptFilePath(string prompt)
        {
            while (true)
            {
                string path = PromptInput(prompt);
                if (System.IO.File.Exists(path))
                {
                    return path;
                }
                DisplayMessage("The specified file does not exist. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the user for a directory path and validates its existence.
        /// </summary>
        public static string PromptDirectoryPath(string prompt)
        {
            while (true)
            {
                string path = PromptInput(prompt);
                if (System.IO.Directory.Exists(path))
                {
                    return path;
                }
                DisplayMessage("The specified directory does not exist. Please try again.");
            }
        }
    }
}
