using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ToDoList.Common;
using ToDoList.Storage.Repository;

namespace ToDoList.API.Controllers
{
    //[Authorize]
    [ApiController]
    //[Route("[controller]")]
    [Route("todo")]
    public class TodoListController : ControllerBase
    {
        private readonly ILogger<TodoListController> m_logger;
        private ITodoListRepoitory m_todoListRepoitory;

        public TodoListController(ILogger<TodoListController> logger, ITodoListRepoitory todoListRepoitory)
        {
            m_logger = logger;
            m_todoListRepoitory = todoListRepoitory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var todoList = await m_todoListRepoitory.GetTodoList();
            return new JsonResult(todoList);
        }

        [HttpGet]
        [Route("filter/{query?}")]
        public async Task<IActionResult> Get(string query)  
        {
            var todoList = await m_todoListRepoitory.GetTodoList(query);
            return new JsonResult(todoList);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var todoItem = await m_todoListRepoitory.GetTodoItem(id);
                if (todoItem != null)
                {
                    return new JsonResult(todoItem);
                }
                return NotFound($"Todo item not found. id: {id}");
            }
            catch (Exception ex)
            {
                m_logger.LogError($"Todo item not found. id: {id}, Error: {ex.Message}");
                return NotFound($"Todo item not found. id: {id}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoItemRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return BadRequest("Description is invalid");
            }
            AddTodoItemResponse todoItemResponse = await m_todoListRepoitory.AddTodoItem(request);
            return new JsonResult(todoItemResponse);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool result = await m_todoListRepoitory.DeleteTodoItem(id);
                if (result)
                {
                    return Ok();
                }
                return NotFound($"Todo item not found. id: {id}");
            }
            catch (Exception ex)
            {
                m_logger.LogError($"Todo item not found. id: {id}, Errors: {ex.Message}");
                return NotFound($"Todo item not found. id: {id}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            long count = await m_todoListRepoitory.DeleteAll();
            return Ok($"Number of deleted items: {count}");
        }

        [HttpPatch]
        [Route("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, TodoStatus status)
        {
            try
            {
                bool result = await m_todoListRepoitory.UpdateStatus(id, status);
                if (result)
                {
                    return Ok();
                }
                return NotFound($"Todo item not found. id: {id}");
            }
            catch (Exception ex)
            {
                m_logger.LogError($"Todo item not found. id: {id}, Error: ex.Message");
                return NotFound($"Todo item not found. id: {id}");
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTodoItemRequest request)
        {
            try
            {
                bool result = await m_todoListRepoitory.Update(id, request);
                if (result)
                {
                    return Ok();
                }
                return NotFound($"Todo item not found. id: {id}");
            }
            catch (Exception ex)
            {
                m_logger.LogError($"Todo item not found. id: {id}, Error: {ex.Message}");
                return NotFound($"Todo item not found. id: {id}");
            }
        }
    }
}
