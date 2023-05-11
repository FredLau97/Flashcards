namespace Flashcards
{
    class FlashcardDTO
    {
        public int CardId { get; set; }
        public string CardFront { get; set; }
        public string CardBack { get; set; }
        public int StackId { get; set; }
        
        public FlashcardDTO(string cardFront, string cardBack, int stackId)
        {
            CardFront = cardFront;
            CardBack = cardBack;
            StackId = stackId;
        }

        public FlashcardDTO(int cardId, string cardFront, string cardBack, int stackId)
        {
            CardId = cardId;
            CardFront = cardFront;
            CardBack = cardBack;
            StackId = stackId;
        }
    }
}
