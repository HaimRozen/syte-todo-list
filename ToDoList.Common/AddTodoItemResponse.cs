using System;

namespace ToDoList.Common
{
    public class AddTodoItemResponse
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string StatusDescription { get; set; }
        public TodoStatus StatusCode { get; set; }

        public AddTodoItemResponse(Guid id, string description, DateTime creationDate, string statusDescription, TodoStatus statusCode)
        {
            Id = id;
            Description = description;
            CreationDate = creationDate;
            StatusDescription = statusDescription;
            StatusCode = statusCode;
        }

    }
}
