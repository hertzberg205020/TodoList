using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.WebService.Dtos;
using TodoList.WebService.Models;

namespace TodoList.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _context.TodoItems.ToList();
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(Guid id)
        {
            var result = await _context.Set<TodoItem>().FindAsync(id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post(TodoItem item)
        {
            await _context.Set<TodoItem>().AddAsync(item);
            await _context.SaveChangesAsync();
            // 201 Created
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> Put(Guid id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest("資源識別碼不符");
            }
            _context.Entry(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound("找不到資源");
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _context.Set<TodoItem>().FindAsync(id);
            if (item == null)
            {
                return NotFound("找不到資源");
            }
            _context.Set<TodoItem>().Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        private bool TodoItemExists(Guid id)
        {
            // 這裡的寫法是為了避免使用 _context.Set<TodoItem>().FindAsync(id) 這個方法
            // 因為這個方法會導致 EF Core 產生 SQL 語法，而這個語法會導致 SQL Server 產生錯誤
            // 這個錯誤是因為 SQL Server 會將 Guid 轉成 binary(16) 來儲存，而 EF Core 會將 Guid 轉成 uniqueidentifier
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
