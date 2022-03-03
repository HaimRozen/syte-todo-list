namespace ToDoList.Storage.Configuration
{
    public class MongoDBConfiguration : IMongoDBConfiguration
    {
        public string ConnectionString => "mongodb://mongoadmin:secret@localhost:27888/?authSource=admin";
        //public string ConnectionString => "mongodb://localhost:27017";
        //public string ConnctionStringTemp => "mongodb://{userName}:{password}@{host}:{port}/?authSource=admin";

        public string DatabaseName => "TodoListService";
    }
}
