namespace Flashcards
{
    internal class UserInput
    {
        private Formatter _formatter;

        public UserInput()
        {
            _formatter = new Formatter();
        }

        public string GetTextInput<T>(T[] validInputs)
        {
            Console.Write("Enter your choice: ");
            var userInput = Console.ReadLine();
           
            if (!ValidateInput(userInput, validInputs))
            {
                DisplayErrorMessage($"Input '{userInput}' is not valid in this context. Please select either of these inputs: {_formatter.FormatInputCollection(validInputs)}.\n");

                userInput = GetTextInput(validInputs);
            }

            return userInput;
        }

        public string GetTextInput(string message)
        {
            Console.Write(message);
            var userInput = Console.ReadLine();

            if (userInput == null || userInput == "")
            {
                DisplayErrorMessage("Input cannot be null. Please try again.\n", true);
                userInput = GetTextInput(message);
            }

            if (userInput == "q") userInput = userInput.ToUpper();

            return userInput;
        }

        public int GetNumberInput(int[] validInputs)
        {
            Console.Write("Enter your choice: ");
            var userInput = Console.ReadLine();

            if (!ValidateInput(userInput, validInputs))
            {
                DisplayErrorMessage($"Input '{userInput}' is not valid in this context. Please select either of these inputs: {_formatter.FormatInputCollection(validInputs)}.\n");

                userInput = GetNumberInput(validInputs).ToString();
            }

            return Convert.ToInt32(userInput);
        }

        public bool ValidateInput<T>(string input, T[] validInputs)
        {
            return validInputs.Select(item => item.ToString()).ToArray().Contains(input);
        }

        private void DisplayErrorMessage(string message, bool canReturnToMainManu = false)
        {
            if (canReturnToMainManu) message += $"Enter Q to return to the main menu\n";
            Console.WriteLine(message);
        }
    }
}
