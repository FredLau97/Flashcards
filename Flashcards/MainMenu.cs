using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class MainMenu
    {
        public static void Display()
        {
            var prompt = 
                "---------------\n"
                + "1. Manage Stacks\n"
                + "2. Manage Flashcards\n"
                + "3. Study\n"
                + "4. Exit\n"
                + "---------------";

            Console.WriteLine(prompt);
            var userInput = UserInput.GetNumberInput(new int[]{ 1, 2, 3, 4 });

            switch(userInput)
            {
                case 1:
                    FlashcardManager.ManageStacks();
                    break;
                case 2:
                    FlashcardManager.ManageCards();
                    break;
                case 3:
                    FlashcardManager.Study();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
