using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data;

public class AppDbContext : DbContext
{
    public DbSet<ToDoModel> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
}