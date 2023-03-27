namespace Flashcards
{
    internal class UserInput
    {
        public static string GetTextInput<T>(T[] validInputs)
        {
            Console.WriteLine("Enter your choice: ");
            var userInput = Console.ReadLine();
           
            if (!ValidateInput(userInput, validInputs))
            {
                DisplayErrorMessage($"Input '{userInput}' is not valid in this context. Please select either of these inputs: {Formatter.FormatInputCollection(validInputs)}.\n" +
                $"Alternatively, enter Q to return to the main menu");

                userInput = GetTextInput(validInputs);
            }

            return userInput;
        }

        public static int GetNumberInput<T>(T[] validInputs)
        {
            Console.WriteLine("Enter your choice: ");
            var userInput = Console.ReadLine();

            if (!ValidateInput(userInput, validInputs))
            {
                DisplayErrorMessage($"Input '{userInput}' is not valid in this context. Please select either of these inputs: {Formatter.FormatInputCollection(validInputs)}.\n" +
                $"Alternatively, enter Q to return to the main menu");

                userInput = GetTextInput(validInputs);
            }

            return Convert.ToInt32(userInput);
        }

        public static bool ValidateInput<T>(string input, T[] validInputs)
        {
            return validInputs.Select(item => item.ToString()).ToArray().Contains(input);
        }

        private static void DisplayErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
