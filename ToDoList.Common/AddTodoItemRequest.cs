namespace ToDoList.Common
{
    public class AddTodoItemRequest
    {
        public string Description { get; set; }

        public AddTodoItemRequest(string description)
        {
            Description = description;
        }
    }
}
