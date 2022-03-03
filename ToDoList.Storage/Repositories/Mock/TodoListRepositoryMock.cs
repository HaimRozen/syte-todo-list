using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Common;
using ToDoList.Storage.Repository;

namespace ToDoList.Storage.Repositories.Mock
{
    public class TodoListRepositoryMock : ITodoListRepoitory
    {
        private readonly Dictionary<Guid, TodoItem> m_todoListRepoMock;

        public TodoListRepositoryMock()
        {
            m_todoListRepoMock = new Dictionary<Guid, TodoItem>();
        }

        public async Task<TodoItem> GetTodoItem(Guid id)
        {
            if (!m_todoListRepoMock.TryGetValue(id, out TodoItem todoItem))
            {
                throw new Exception($"TodoItem with id: {id} doesn't exist");
            }
            return todoItem;
        }

        public async Task<List<TodoItem>> GetTodoList(string query = null)
        {
            if (query == null)
            {
                return m_todoListRepoMock.Values.ToList();
            }
            return m_todoListRepoMock.Values.Where(x => x.Description.Contains(query)).ToList();
        }

        public async Task<AddTodoItemResponse> AddTodoItem(AddTodoItemRequest request)
        {
            TodoItem todoItem = new TodoItem(Guid.NewGuid(), request.Description, DateTime.UtcNow, Common.TodoStatus.Pending);
            m_todoListRepoMock.Add(todoItem.Id, todoItem);
            AddTodoItemResponse response = new AddTodoItemResponse(todoItem.Id, todoItem.Description, todoItem.CreationDate,
                todoItem.Status.ToString(), todoItem.Status);
            return response;
        }

        public async Task<bool> Update(Guid id, UpdateTodoItemRequest request)
        {
            if (!m_todoListRepoMock.TryGetValue(id, out TodoItem todoItemStorage))
            {
                throw new Exception($"TodoItem with id: {id} doesn't exist");
            }
            todoItemStorage.Description = request.Description;
            todoItemStorage.Status = request.TooItemStatus;
            return true;
        }

        public async Task<bool> UpdateStatus(Guid id, TodoStatus status)
        {
            if (!m_todoListRepoMock.TryGetValue(id, out TodoItem todoItemStorage))
            {
                throw new Exception($"TodoItem with id: {id} doesn't exist");
            }
            todoItemStorage.Status = status;

            return true;
        }

        public async Task<long> DeleteAll()
        {
            long keysCount = m_todoListRepoMock.Keys.Count;
            m_todoListRepoMock.Clear();
            return keysCount;
        }

        public async Task<bool> DeleteTodoItem(Guid id)
        {
            if (!m_todoListRepoMock.ContainsKey(id))
            {
                throw new Exception($"TodoItem with id: {id} doesn't exist");
            }
            return m_todoListRepoMock.Remove(id);
        }

    }
}
