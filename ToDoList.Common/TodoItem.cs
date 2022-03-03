using System;

namespace ToDoList.Common
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; }
        public TodoStatus Status { get; set; }
        public string StatusDescription { get => Status.ToString(); }

        public TodoItem(string description)
        {
            Status = TodoStatus.Pending;
            Description = description;
            CreationDate = DateTime.UtcNow;
        }

        public TodoItem(Guid id, string description, DateTime creationDate, TodoStatus statusCode)
        {
            Id = id;
            Description = description;
            CreationDate = creationDate;
            Status = statusCode;
        }

        public TodoItem()
        {
            // Must for deserialize from MongoDB
        }
    }
}
