namespace ToDoList.Storage.Configuration
{
    public interface IMongoDBConfiguration
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
    }
}
