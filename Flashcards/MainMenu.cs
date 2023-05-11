using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class MainMenu
    {
        private FlashcardManager _flashcardManager;
        private UserInput _userInput;

        public MainMenu()
        {
            _flashcardManager = new FlashcardManager(this);
            _userInput = new UserInput();
        }   

        public void Display()
        {
            var prompt = 
                "---------------\n"
                + "1. Manage Stacks\n"
                + "2. Manage Flashcards\n"
                + "3. Study\n"
                + "4. Exit\n"
                + "---------------";

            Console.WriteLine(prompt);
            var userInput = _userInput.GetNumberInput(new int[]{ 1, 2, 3, 4 });

            switch(userInput)
            {
                case 1:
                    _flashcardManager.ManageStacks();
                    break;
                case 2:
                    _flashcardManager.ManageCards();
                    break;
                case 3:
                    _flashcardManager.Study();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
