using Microsoft.EntityFrameworkCore;
using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>()
            .HasKey(i => i.InvoiceID);

        modelBuilder.Entity<InvoiceItem>().HasKey(i => i.ItemID);

        modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Items)
            .WithOne(i => i.Invoice)
            .HasForeignKey(ii => ii.InvoiceID)
            .OnDelete(DeleteBehavior.Restrict); ;
        
        base.OnModelCreating(modelBuilder);
    }
}