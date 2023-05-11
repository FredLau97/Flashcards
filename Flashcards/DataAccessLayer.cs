namespace Flashcards
{
    class DataAccessLayer
    {
        private DatabaseConnection _connection;

        public DataAccessLayer()
        {
            _connection = new DatabaseConnection();
        }

        internal bool CreateFlashcard(FlashcardDTO card)
        {
            return _connection.CreateFlashcard(card);
        }

        internal void CreateStack(string stackName)
        {
            _connection.CreateStack(stackName);
        }

        internal List<StackDTO>? GetStacks()
        {
            var stacksDTO = _connection.GetStacks();
            return stacksDTO;
        }

        internal StackDTO GetStack(string stackName)
        {
            var stack = _connection.GetStack(stackName);
            return stack;
        }

        internal List<FlashcardDTO> GetCardsInStack(StackDTO stack)
        {
            var cards = _connection.GetCardsInStack(stack);
            return cards;
        }

        internal void EditFlashcard(FlashcardDTO card)
        {
            _connection.EditFlashcard(card);
        }

        internal void DeleteFlashcard(int cardID)
        {
            _connection.DeleteFlashcard(cardID);
        }

        internal void DeleteStack(int stackId)
        {
            _connection.DeleteStack(stackId);
        }

        internal List<FlashcardDTO> GetCards()
        {
            return _connection.GetAllCards();
        }
    }
}
