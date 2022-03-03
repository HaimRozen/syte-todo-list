using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ToDoList.Storage.StorageObjects
{
    public class TodoItemStorageObject
    {
        //[BsonElement("_id")]
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public TodoItemStorageObject(DateTime creationTime, string description, string status)
        {
            CreationTime = creationTime;
            Description = description;
            Status = status;
        }

        public TodoItemStorageObject(Guid id, DateTime creationTime, string description, string status)
        {
            Id = id;
            CreationTime = creationTime;
            Description = description;
            Status = status;
        }
    }
}
