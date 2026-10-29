using Microsoft.EntityFrameworkCore;

public class Pitut_Ana_Lab2Context(DbContextOptions<Pitut_Ana_Lab2Context> options) : DbContext(options)
{
    public DbSet<Pitut_Ana_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Pitut_Ana_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Pitut_Ana_Lab2.Models.Author> Author { get; set; } = default!; 
}
