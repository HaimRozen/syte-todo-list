using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Common;
using ToDoList.Storage.Repositories.Mock;
using ToDoList.Storage.Repository;

namespace UnitTests
{
    [TestClass]
    public class TodoListRepoitoryTests
    {
        private ITodoListRepoitory m_todoListRepoitory;

        public TodoListRepoitoryTests()
        {
            m_todoListRepoitory = new TodoListRepositoryMock();
        }

        [TestMethod]
        public async Task GetEmptyTodoList()
        {
            List<TodoItem> todoList = await m_todoListRepoitory.GetTodoList();
            Assert.AreEqual(todoList.Count, 0);
        }

        [TestMethod]
        public async Task SetTodoItem_GetAll_ItemExist()
        {
            //arrange
            string todoDecription = "Walk with my dog";
            var todoItemResponse = await AddTodoItem(todoDecription);

            //act
            List<TodoItem> todoList = await m_todoListRepoitory.GetTodoList();
            
            //assert that we have only single Item
            Assert.AreEqual(todoList.Single().Description, todoDecription);
        }

        [TestMethod]
        public async Task TodoItem_UpdateItem_ItemUpdated()
        {
            //arrange
            string originDescription = "Walk with my dog";
            var todoItem = await AddTodoItem(originDescription);

            //act
            string updatedDescription = "Go to the post office";
            todoItem.Description = updatedDescription;
            UpdateTodoItemRequest request = new UpdateTodoItemRequest(updatedDescription);
            await m_todoListRepoitory.Update(todoItem.Id, request);
            
            TodoItem todoItemFromStorage = await m_todoListRepoitory.GetTodoItem(todoItem.Id);
            Assert.AreEqual(updatedDescription, todoItemFromStorage.Description);
        }

        [TestMethod]
        public async Task DeleteNonExistItem()
        {
            //arrange
            Guid id = Guid.NewGuid();

            //act
            await Assert.ThrowsExceptionAsync<Exception>(() => m_todoListRepoitory.DeleteTodoItem(id));
            //Assert.IsFalse(await m_todoListRepoitory.DeleteTodoItem(id));
        }

        [TestMethod]
        public async Task AddTwoTodoItem_DeleteOne_ItemDeleted()
        {
            //arrange
            string description1 = "Walk with my dog";
            AddTodoItemResponse todoItem1 = await AddTodoItem(description1);
            string description2 = "Take NFT course";
            AddTodoItemResponse todoItem2 = await AddTodoItem(description2);

            //act
            bool result = await m_todoListRepoitory.DeleteTodoItem(todoItem2.Id);
            Assert.IsTrue(result);

            List<TodoItem> todoList = await m_todoListRepoitory.GetTodoList();
            Assert.AreEqual(1, todoList.Count);
            Assert.AreEqual(description1, todoList.Single().Description);
        }

        private async Task<AddTodoItemResponse> AddTodoItem(string description)
        {
            AddTodoItemRequest request = new AddTodoItemRequest(description);
            return await m_todoListRepoitory.AddTodoItem(request);
        }

    }
}
