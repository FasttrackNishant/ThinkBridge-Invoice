using Microsoft.EntityFrameworkCore;
using ThinkBridge.Api.Infrastructure;
using ThinkBridge.Api.Interfaces;
using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Infrastructure.Services.DatabaseCollection;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Invoice>> GetAllAsync()
    {
        return await _context.Invoices
            .Include(i => i.Items)
            .ToListAsync();
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await _context.Invoices
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.InvoiceID == id);
    }

    public async Task<Invoice> AddInvoiceAsync(Invoice invoice)
    {
        _context.Invoices.Add(invoice);
        await SaveChangesAsync();
        return invoice;
    }

    public async Task DeleteAsync(int id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.InvoiceID == id);

        if (invoice != null)
        {
            _context.InvoiceItems.RemoveRange(invoice.Items);
            _context.Invoices.Remove(invoice);
            await SaveChangesAsync();
        }
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}