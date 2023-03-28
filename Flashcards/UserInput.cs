namespace Flashcards
{
    internal class UserInput
    {
        public static string GetTextInput<T>(T[] validInputs)
        {
            Console.Write("Enter your choice: ");
            var userInput = Console.ReadLine();
           
            if (!ValidateInput(userInput, validInputs))
            {
                DisplayErrorMessage($"Input '{userInput}' is not valid in this context. Please select either of these inputs: {Formatter.FormatInputCollection(validInputs)}.\n");

                userInput = GetTextInput(validInputs);
            }

            return userInput;
        }

        public static int GetNumberInput(int[] validInputs)
        {
            Console.Write("Enter your choice: ");
            var userInput = Console.ReadLine();

            if (!ValidateInput(userInput, validInputs))
            {
                DisplayErrorMessage($"Input '{userInput}' is not valid in this context. Please select either of these inputs: {Formatter.FormatInputCollection(validInputs)}.\n");

                userInput = GetNumberInput(validInputs).ToString();
            }

            return Convert.ToInt32(userInput);
        }

        public static bool ValidateInput<T>(string input, T[] validInputs)
        {
            return validInputs.Select(item => item.ToString()).ToArray().Contains(input);
        }

        private static void DisplayErrorMessage(string message, bool canReturnToMainManu = false)
        {
            if (canReturnToMainManu) message += $"Alternatively, enter Q to return to the main menu\n";
            Console.WriteLine(message);
        }
    }
}
