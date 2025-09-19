using ThinkBridge.Api.Interfaces;
using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Infrastructure.Services.InMemory;

public class InvoiceInMemoryRepository : IInMemoryRepository
{
    private readonly List<Invoice> _invoices = new();

    public InvoiceInMemoryRepository()
    {
        var invoice1 = new Invoice
        {
            InvoiceID = 1,
            CustomerName = "John Doe",
            Items = new List<InvoiceItem>
            {
                new InvoiceItem { ItemID = 1, InvoiceID = 1, Name = "Widget A", Price = 19.99m }
            }
        };

        var invoice2 = new Invoice
        {
            InvoiceID = 2,
            CustomerName = "Jane Smith"
        };

        _invoices.Add(invoice1);
        _invoices.Add(invoice2);
    }

    public IEnumerable<Invoice> GetAll() => _invoices;

    public Invoice? GetById(int id) =>
        _invoices.FirstOrDefault(i => i.InvoiceID == id);


    public void Add(Invoice invoice)
    {
        invoice.InvoiceID = _invoices.Count > 0 ? _invoices.Max(i => i.InvoiceID) + 1 : 1;

        foreach (var item in invoice.Items)
        {
            item.ItemID = invoice.Items.IndexOf(item) + 1;
            item.InvoiceID = invoice.InvoiceID;
        }

        _invoices.Add(invoice);
    }

    public void Update(Invoice invoice)
    {
        var existing = GetById(invoice.InvoiceID);
        if (existing == null) return;

        existing.CustomerName = invoice.CustomerName;

        existing.Items = invoice.Items.Select((item, index) =>
        {
            item.ItemID = index + 1;
            item.InvoiceID = existing.InvoiceID;
            return item;
        }).ToList();
    }

    public void Delete(int id)
    {
        var invoice = GetById(id);
        if (invoice != null)
            _invoices.Remove(invoice);
    }
}