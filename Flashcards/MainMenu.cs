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
            Console.WriteLine(UserInput.GetNumberInput(new int[] { 2, 3, 4 }));
        }
    }
}
