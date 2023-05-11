using ConsoleTableExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards
{
    internal class FlashcardManager
    {
        private MainMenu _mainMenu;
        private Formatter _formatter;
        private UserInput _userInput;

        public FlashcardManager(MainMenu mainMenu)
        {
            _formatter = new Formatter();
            _userInput = new UserInput();
            _mainMenu = mainMenu;
        }

        public void ManageStacks()
        {
            var dataAccessLayer = new DataAccessLayer(); 
            var stacks = dataAccessLayer.GetStacks();

            if (stacks.Count < 1)
            {
                Console.WriteLine("There are currently no stacks of flashcards to manage.");
                Console.WriteLine("Would you like to create a new stack? Y/N");
                var userInput = _userInput.GetTextInput<string>(new string[] { "Y", "N" });

                if (userInput == "Y")
                {
                    CreateStack();
                    return;
                }

                _mainMenu.Display();
            }

            Console.WriteLine("Which stack would you like to interact with?: ");
            _formatter.FormatStackDTO(stacks);
            var stackNames = stacks.Select(stack => stack.StackName).ToArray();
            var stackName = _userInput.GetTextInput(stackNames);
            var stack = dataAccessLayer.GetStack(stackName);
            StackInteraction(stack);
        }

        public void ManageCards()
        {
            var dataAccessLayer = new DataAccessLayer();
            var cards = dataAccessLayer.GetCards();

            if (cards.Count < 1)
            {
                Console.WriteLine("There are currently no flashcards to manage.");
                _mainMenu.Display();
            }

            Console.WriteLine("Which flashcard would you like to interact with?: ");
            _formatter.FormatFlashcardDTO(cards);
            var cardIDs = cards.Select(card => card.CardId).ToArray();
            var cardID = _userInput.GetNumberInput(cardIDs);
            var card = cards.Where(card => card.CardId == cardID).FirstOrDefault();
            CardInteraction(card);
        }

        private void CardInteraction(FlashcardDTO card)
        {
            var message = "---------------\n"
                + $"What would you like to do with this card?\n\t Front: {card.CardFront})\n\t Back: {card.CardBack}?\n\n"
                + "R to return to the main menu\n"
                + "X to change card\n"
                + "E to edit the flashcard\n"
                + "D to delete the flashcard\n"
                + "---------------";

            Console.WriteLine(message);
            var userInput = _userInput.GetTextInput(new string[] { "R", "X", "E", "D" });

            switch (userInput)
            {
                case "R":
                    _mainMenu.Display();
                    break;
                case "X":
                    ManageCards();
                    break;
                case "E":
                    EditFlashcard(card);
                    break;
                case "D":
                    DeleteFlashcard(card);
                    break;
            }
        }

        public void Study()
        {
            Console.WriteLine("Study Called");
        }

        private void CreateStack()
        {
            var stackName = _userInput.GetTextInput("\nEnter a name for the new stack, or enter Q to return to the main menu: ");
            if (stackName == "Q")
            {
                _mainMenu.Display();
                return;
            }
            var dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.CreateStack(stackName);
            var stack = dataAccessLayer.GetStack(stackName);

            Console.WriteLine($"The stack '{stackName}' has been created.");

            StackInteraction(stack);
        }

        private void StackInteraction(StackDTO stack)
        {
            var message = "---------------\n"
                + $"What would you like to do with this stack ({stack.StackName})?\n\n"
                + "R to return to the main menu\n"
                + "X to change stack\n"
                + "V to view all flashcards in the stack\n"
                + "C to create a new flashcard in the stack\n"
                + "E to edit a flashcard in the stack\n"
                + "D to delete a flashcard in the stack\n"
                + "A to delete the stack\n"
                + "---------------";

            Console.WriteLine(message);
            var userInput = _userInput.GetTextInput<string>(new string[] { "R", "X", "V", "C", "E", "D", "A" });

            switch (userInput)
            {
                case "R":
                    _mainMenu.Display();
                    break;
                case "X":
                    ManageStacks();
                    break;
                case "V":
                    ViewStack(stack);
                    break;
                case "C":
                    CreateFlashcard(stack);
                    break;
                case "E":
                    EditFlashcard(stack);
                    break;
                case "D":
                    DeleteFlashcard(stack);
                    break;
                case "A":
                    DeleteStack(stack);
                    break;
            }
        }

        private void DeleteStack(StackDTO stack)
        {
            Console.WriteLine($"Are you sure you want to delete this stack ({stack.StackName})? Y/N");
            var confirmDelete = _userInput.GetTextInput(new string[] { "Y", "N" }) == "Y";

            if (!confirmDelete)
            {
                StackInteraction(stack);
                return;
            }

            var dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.DeleteStack(stack.StackId);
            Console.WriteLine("Stack deleted successfully.");
            _mainMenu.Display();
        }

        private void DeleteFlashcard(StackDTO stack)
        {
            var cards = GetFlashcards(stack);
            var cardIDs = cards.Select(card => card.CardId).ToArray();
            Console.WriteLine("Which flashcard (ID) would you like to delete?");
            var cardID = Convert.ToInt32(_userInput.GetTextInput(cardIDs));

            Console.WriteLine("Are you sure you want to delete this flashcard? Y/N");
            var confirmDelete = _userInput.GetTextInput(new string[] { "Y", "N" }) == "Y";

            if (!confirmDelete)
            {
                StackInteraction(stack);
                return;
            }

            var dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.DeleteFlashcard(cardID);
            Console.WriteLine("Flashcard deleted successfully.");
            StackInteraction(stack);
        }

        private void DeleteFlashcard(FlashcardDTO card)
        {
            Console.WriteLine("Are you sure you want to delete this flashcard? Y/N");
            var confirmDelete = _userInput.GetTextInput(new string[] { "Y", "N" }) == "Y";
            if (!confirmDelete)
            {
                CardInteraction(card);
                return;
            }
            var dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.DeleteFlashcard(card.CardId);
            Console.WriteLine("Flashcard deleted successfully.");
            _mainMenu.Display();
        }

        private void EditFlashcard(StackDTO stack)
        {
            var cards = GetFlashcards(stack);
            var cardIDs = cards.Select(card => card.CardId).ToArray();
            Console.WriteLine("Which flashcard (ID) would you like to edit?");
            var cardID = Convert.ToInt32(_userInput.GetTextInput(cardIDs));

            var card = CreateFlashcardData(cardID, stack);

            var dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.EditFlashcard(card);

            StackInteraction(stack);
        }

        private void EditFlashcard(FlashcardDTO card)
        {
            var newCard = CreateFlashcardData(card);
            var dataAccessLayer = new DataAccessLayer();
            dataAccessLayer.EditFlashcard(newCard);
            CardInteraction(newCard);
        }

        private void CreateFlashcard(StackDTO stack)
        {
            FlashcardDTO flashCard = CreateFlashcardData(stack);

            var dataAccessLayer = new DataAccessLayer();

            if (dataAccessLayer.CreateFlashcard(flashCard))
            {
                Console.WriteLine("Flashcard created successfully.");
            }
            else
            {
                Console.WriteLine("Flashcard creation failed.");
            }

            StackInteraction(stack);
        }

        private FlashcardDTO CreateFlashcardData(StackDTO stack)
        {
            var cardFront = _userInput.GetTextInput("\nEnter the front of the flashcard: ");
            var cardBack = _userInput.GetTextInput("Enter the back of the flashcard: ");
            var flashCard = new FlashcardDTO(cardFront, cardBack, stack.StackId);
            return flashCard;
        }

        private FlashcardDTO CreateFlashcardData(int cardID, StackDTO stack)
        {
            var cardFront = _userInput.GetTextInput("\nEnter the front of the flashcard: ");
            var cardBack = _userInput.GetTextInput("Enter the back of the flashcard: ");
            var flashCard = new FlashcardDTO(cardID, cardFront, cardBack, stack.StackId);
            return flashCard;
        }

        private FlashcardDTO CreateFlashcardData(FlashcardDTO card)
        {
            var cardFront = _userInput.GetTextInput("\nEnter the front of the flashcard: ");
            var cardBack = _userInput.GetTextInput("Enter the back of the flashcard: ");
            var flashCard = new FlashcardDTO(card.CardId, cardFront, cardBack, card.StackId);
            return flashCard;
        }

        private void ViewStack(StackDTO stack)
        {
            var dataAccessLayer = new DataAccessLayer();
            var cards = dataAccessLayer.GetCardsInStack(stack);

            if (cards.Count < 1)
            {
                Console.WriteLine("There are currently no flashcards in this stack.");
                StackInteraction(stack);
                return;
            }

            _formatter.FormatFlashcardDTO(cards);
            StackInteraction(stack);
        }

        private List<FlashcardDTO> GetFlashcards(StackDTO stack)
        {
            var dataAccessLayer = new DataAccessLayer();
            var cards = dataAccessLayer.GetCardsInStack(stack);

            if (cards.Count < 1)
            {
                Console.WriteLine("There are currently no flashcards in this stack.");
                StackInteraction(stack);
                return null;
            }

            _formatter.FormatFlashcardDTO(cards);
            return cards;
        }
    }
}
