using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controller;

[ApiController]
public class HomeController : ControllerBase
{
    readonly AppDbContext _context;
    public HomeController(AppDbContext context) => _context = context;

    [HttpGet("/")]
    public IActionResult Get()
        => Ok(_context.Todos.AsNoTracking().ToList());
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var todo =_context.Todos
                   .AsNoTracking()
                   .FirstOrDefault(x => x.Id == id);
                   
        if(todo == null)
            NotFound();
    
        return Ok(todo);
    }
        
    [HttpPost("/")]
    public IActionResult Post([FromBody] ToDoModel todo)
    {
        _context.Todos.Add(todo);
        _context.SaveChanges();
        return Ok(todo);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put([FromRoute] int id, [FromBody] ToDoModel todo)
    {
        var model = _context.Todos
                            .AsNoTracking()
                            .FirstOrDefault(x => x.Id == id);
                        
        if(model == null || todo == null)
            NotFound();
        
        model.Title = todo.Title;
        model.Done = todo.Done;
        
        _context.Todos.Update(model);
        _context.SaveChanges();
        return Ok(model);
    }

    [HttpDelete("/{id:int}")]
    public IActionResult Remove([FromRoute] int id)
    {   
        var model = _context.Todos
                           .AsNoTracking()
                           .FirstOrDefault(x => x.Id == id);

        if(model == null)
            NotFound();

        _context.Todos.Remove(model);
        _context.SaveChanges();
        return NoContent();
    }
}