using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Common;

namespace ToDoList.Storage.Repository
{
    public interface ITodoListRepoitory
    {
        Task<List<TodoItem>> GetTodoList(string query = null);
        Task<TodoItem> GetTodoItem(Guid id);
        Task<AddTodoItemResponse> AddTodoItem(AddTodoItemRequest request);
        Task<bool> UpdateStatus(Guid id, TodoStatus status);
        Task<bool> Update(Guid id, UpdateTodoItemRequest request);
        Task<bool> DeleteTodoItem(Guid id);
        Task<long> DeleteAll();
    }
}
