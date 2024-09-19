using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppAPI.Data;
using TodoAppAPI.DTOs;
using TodoAppAPI.Models;

namespace TodoAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoDbcontext;

        //inject dbcontext into controller an assign it to local variable
        public TodoController(TodoDbContext todoDbcontext)
        {
            _todoDbcontext = todoDbcontext;
        }

        // GET: api/Todo
        [HttpGet]
        [Route("get-all-todos")]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoDbcontext.Todos.ToListAsync();
            return Ok(todos);
        }

        // alternative get method that returns an explicit type of Todo
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        //{
        //    return await _todoDbcontext.Todos.ToListAsync();
        //}

        [HttpPost]
        [Route("add-todo")]
        public async Task<IActionResult> CreateTodo(TodoDto todo)
        {
            try
            {
                if (todo == null)
                {
                    return BadRequest("You cannot create an invalid task");
                }
                var newTodo = new Todo
                {
                    Description = todo.Description,
                    IsCompleted = todo.IsCompleted,
                    CategoryId = todo.CategoryId,
                    StatusId = todo.StatusId,
                    CreatedDate = DateTime.Now,
                    PriorityId = todo.PriorityId,
                    DueDate = todo.DueDate != null ? todo.DueDate : null
                };
                _todoDbcontext.Todos.Add(newTodo);
                await _todoDbcontext.SaveChangesAsync();

                return Ok("You successfully created a new task");
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
