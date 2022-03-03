namespace ToDoList.Common
{
    public class UpdateTodoItemRequest
    {
        public UpdateTodoItemRequest(string description)
        {
            Description = description;
        }

        public string Description { get; }
        public TodoStatus TooItemStatus { get; }
    }
}
