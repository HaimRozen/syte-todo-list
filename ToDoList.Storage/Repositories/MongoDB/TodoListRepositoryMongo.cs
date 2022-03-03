using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common;
using ToDoList.Storage.Configuration;
using ToDoList.Storage.Repository;
using ToDoList.Storage.StorageObjects;

namespace ToDoList.Storage.Repositories.MongoDB
{
    public class TodoListRepositoryMongo : ITodoListRepoitory
    {
        private MongoClient m_mongoClient;
        private IMongoDBConfiguration m_mongoDBConfiguration;
        private const string COLLECTION_NAME = "TodoList";

        public TodoListRepositoryMongo(IMongoDBConfiguration mongoDBConfiguration)
        {
            m_mongoDBConfiguration = mongoDBConfiguration;
            m_mongoClient = new MongoClient(m_mongoDBConfiguration.ConnectionString);
        }

        public async Task<List<TodoItem>> GetTodoList(string query = null)
        {
            List<TodoItemStorageObject> result = new List<TodoItemStorageObject>();
            var collection = GetTodoListCollection();
            if (query == null)
            {
                result = await collection.FindSync(FilterDefinition<TodoItemStorageObject>.Empty).ToListAsync();
                return ConvertToTodoItems(result);
            }
            result = await collection.FindSync(x => x.Description.Contains(query)).ToListAsync();
            return ConvertToTodoItems(result);
        }

        public async Task<TodoItem> GetTodoItem(Guid id)
        {
            var collection = GetTodoListCollection();
            var result =  await collection.FindAsync(x => x.Id == id);
            TodoItemStorageObject todoItem = await result.SingleAsync();
            return ConvertToTodoItem(todoItem);
        }

        public async Task<AddTodoItemResponse> AddTodoItem(AddTodoItemRequest request)
        {
            var collection = GetTodoListCollection();
            TodoItemStorageObject storageObject = new TodoItemStorageObject(DateTime.UtcNow, request.Description, TodoStatus.Pending.ToString());
            await collection.InsertOneAsync(storageObject);

            Enum.TryParse(storageObject.Status, out TodoStatus status);
            return new AddTodoItemResponse(storageObject.Id, storageObject.Description, storageObject.CreationTime,
                storageObject.Status.ToString(), status);
        }

        public async Task<long> DeleteAll()
        {
            var collection = GetTodoListCollection();
            var result = await collection.DeleteManyAsync(FilterDefinition<TodoItemStorageObject>.Empty);
            return result.DeletedCount;
        }

        public async Task<bool> DeleteTodoItem(Guid id)
        {
            var collection = GetTodoListCollection();
            var result = await collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> Update(Guid id, UpdateTodoItemRequest request)
        {
            var collection = GetTodoListCollection();

            var update = Builders<TodoItemStorageObject>.Update
                .Set(x => x.Status, request.TooItemStatus.ToString())
                .Set(x => x.Description, request.Description);
            var result = await collection.UpdateOneAsync(x => x.Id == id, update);
            
            if (!result.IsAcknowledged)
            {
                return false;
            }
            return result.ModifiedCount == 1;
        }

        public async Task<bool> UpdateStatus(Guid id, TodoStatus status)
        {
            var collection = GetTodoListCollection();
            var update = Builders<TodoItemStorageObject>.Update.Set(x => x.Status, status.ToString());
            var result = await collection.UpdateOneAsync(x => x.Id == id, update);
            if (!result.IsAcknowledged)
            {
                return false;
            }
            return result.ModifiedCount == 1;
        }

        private IMongoCollection<TodoItemStorageObject> GetTodoListCollection()
        {
            IMongoDatabase mongoDatabase = m_mongoClient.GetDatabase(m_mongoDBConfiguration.DatabaseName);
            return mongoDatabase.GetCollection<TodoItemStorageObject>(COLLECTION_NAME);
        }

        private List<TodoItem> ConvertToTodoItems(List<TodoItemStorageObject> todoItems)
        {
            List<TodoItem> result = new List<TodoItem>();

            foreach (var todoItem in todoItems)
            {
                result.Add(ConvertToTodoItem(todoItem));
            }
            return result;
        }

        private TodoItem ConvertToTodoItem(TodoItemStorageObject todoItem)
        {
            Enum.TryParse(todoItem.Status, out Common.TodoStatus status);
            return new TodoItem(todoItem.Id, todoItem.Description, todoItem.CreationTime, status);
        }
    }
}
