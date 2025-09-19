using Microsoft.AspNetCore.Components;
using ThinkBridge.Client.Components.Domain;

namespace ThinkBridge.Client.Components.Pages;

public partial class InvoicePage : ComponentBase
{
    private List<Invoice> invoices = new();
    private bool showForm = false;
    private Invoice formInvoice = new();
    private Invoice? editInvoice;
    private int nextInvoiceID = 1;
    private int nextItemID = 1;

    protected override void OnInitialized()
    {
        // Initialize with some sample data
        invoices = new List<Invoice>
        {
            new Invoice
            {
                InvoiceID = nextInvoiceID++,
                CustomerName = "John Doe",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { ItemID = nextItemID++, Name = "Widget A", Price = 19.99m }
                }
            },
            new Invoice
            {
                InvoiceID = nextInvoiceID++,
                CustomerName = "Jane Smith",
                Items = new List<InvoiceItem>()
            }
        };
    }

    private void ShowAddForm()
    {
        showForm = true;
        editInvoice = null;
        formInvoice = new Invoice { Items = new List<InvoiceItem>() };
    }

    private void AddNewItem()
    {
        formInvoice.Items.Add(new InvoiceItem { ItemID = nextItemID++ });
    }

    private void RemoveItem(InvoiceItem item)
    {
        formInvoice.Items.Remove(item);
    }

    private void EditInvoice(Invoice invoice)
    {
        showForm = true;
        editInvoice = invoice;

        formInvoice = new Invoice
        {
            InvoiceID = invoice.InvoiceID,
            CustomerName = invoice.CustomerName,
            Items = invoice.Items.Select(i => new InvoiceItem
            {
                ItemID = i.ItemID,
                Name = i.Name,
                Price = i.Price
            }).ToList()
        };
    }

    private void CancelForm()
    {
        showForm = false;
        formInvoice = new Invoice();
        editInvoice = null;
    }

    private void SaveInvoice()
    {
        if (editInvoice != null)
        {
            // Update existing invoice
            editInvoice.CustomerName = formInvoice.CustomerName;
            editInvoice.Items = formInvoice.Items;
        }
        else
        {
            // Add new invoice
            formInvoice.InvoiceID = nextInvoiceID++;
            invoices.Add(formInvoice);
        }

        showForm = false;
    }

    private void DeleteInvoice(int id)
    {
        var invoice = invoices.FirstOrDefault(i => i.InvoiceID == id);
        if (invoice != null)
            invoices.Remove(invoice);
    }
}