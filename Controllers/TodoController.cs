using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAppAPI.Data;
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

    }
}
