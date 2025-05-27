using Microsoft.EntityFrameworkCore;
using UserAuth.Domain.Entry;
using UserAuth.Domain.Abstractions;

namespace UserAuth.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Entry> Entries { get; set; }
}