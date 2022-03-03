//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using ToDoList.Common;
//using ToDoList.Storage.Repository;
//using ToDoList.Storage.StorageObjects;

//namespace TestUtils.Mocks
//{
//    public class TodoListRepositoryMock : ITodoListRepoitory
//    {
//        private readonly Dictionary<Guid, TodoItem> m_todoListRepoMock;

//        public TodoListRepositoryMock()
//        {
//            m_todoListRepoMock = new Dictionary<Guid, TodoItem>();
//        }

//        public async Task<Guid> AddTodoItem(TodoItem todoItem)
//        {
//            Guid id = Guid.NewGuid();
//            m_todoListRepoMock.Add(id, todoItem);
//            await Task.Delay(1);
//            return id;
//        }

//        public async Task DeleteAll()
//        {
//            m_todoListRepoMock.Clear();
//            await Task.Delay(1);
//        }

//        public async Task DeleteTooItem(Guid id)
//        {
//            m_todoListRepoMock.Remove(id);
//            await Task.Delay(1);
//        }

//        public async Task<TodoItem> GetTodoItem(Guid id)
//        {
//            if (m_todoListRepoMock.TryGetValue(id, out TodoItem todoItem))
//            {
//                return todoItem;
//            }
//            return null;
//        }

//        public async Task<IEnumerable<TodoItem>> GetTodoList(string query = null)
//        {
//            var result = m_todoListRepoMock.Values.Where(x => x.Description.Contains(query));
//            return result;
//        }
//    }
//}
