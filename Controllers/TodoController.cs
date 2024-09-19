using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("update-todo/{id}")]
        public async Task<IActionResult> UpdateTodo(int id, TodoDto todo)
        {
            try
            {
                if (todo == null || todo.Id == null || todo.Id <= 0 || id != todo.Id)
                {
                    return BadRequest("You cannot update an invalid task");
                }
                //check if todo exist in db
                var dbTodo = _todoDbcontext.Todos.Where(x => x.Id == todo.Id).FirstOrDefault();
                if (dbTodo == null)
                {
                    return BadRequest("You cannot update a task that does not exist");
                }
                //if todo exists the update it
                dbTodo.Description = todo.Description;
                dbTodo.IsCompleted = todo.IsCompleted;
                dbTodo.CategoryId = todo.CategoryId;
                dbTodo.StatusId = todo.StatusId;
                dbTodo.PriorityId = todo.PriorityId;
                dbTodo.DueDate = todo.DueDate != null ? todo.DueDate : null;

                //check if entity state is modified
                _todoDbcontext.Entry(dbTodo).State = EntityState.Modified;
                await _todoDbcontext.SaveChangesAsync();

                return Ok("You successfully updated a task");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // GET: api/Todo/5
        [HttpGet]
        [Route("get-todo-by-id/{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            try
            {
                if(id <= 0 || id == null)
                {
                    return NotFound();
                }
                var todo = await _todoDbcontext.Todos.FindAsync(id);

                if (todo == null)
                {
                    return NotFound();
                }

                return Ok(todo);

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
