namespace Flashcards
{
    class StackDTO
    {
        public int StackId { get; set; }
        public string StackName { get; set; }

        public StackDTO(int stackId, string stackName)
        {
            StackName = stackName;
            StackId = stackId;
        }
    }
}
