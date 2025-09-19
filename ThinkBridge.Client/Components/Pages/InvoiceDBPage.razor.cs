
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using ThinkBridge.Client.Components.Domain;

namespace ThinkBridge.Client.Components.Pages;

public partial class InvoiceDBPage : ComponentBase
{
    private List<Invoice>? invoices;

    private bool showForm = false;
    private Invoice formInvoice = new();
    private Invoice? editInvoice;

    [Inject] private HttpClient _Http { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadInvoices();
    }

    private async Task LoadInvoices()
    {
        invoices = await _Http.GetFromJsonAsync<List<Invoice>>("https://localhost:7283/api/v1/invoice");
    }

    private void ShowAddForm()
    {
        showForm = true;
        editInvoice = null;
        formInvoice = new Invoice { Items = new List<InvoiceItem>() };
    }

    private void AddNewItem()
    {
        formInvoice.Items.Add(new InvoiceItem());
    }

    private void RemoveItem(InvoiceItem item)
    {
        formInvoice.Items.Remove(item);
    }

    private void EditInvoice(Invoice invoice)
    {
        showForm = true;
        editInvoice = invoice;

        // Clone invoice to avoid editing the original list until save
        formInvoice = new Invoice
        {
            InvoiceID = invoice.InvoiceID,
            CustomerName = invoice.CustomerName,
            Items = invoice.Items.Select(i => new InvoiceItem
            {
                ItemID = i.ItemID,
                InvoiceID = i.InvoiceID,
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

    private async Task SaveInvoice()
    {
        if (editInvoice != null)
        {
            await _Http.PutAsJsonAsync($"https://localhost:7283/api/v1/invoice/{formInvoice.InvoiceID}", formInvoice);
        }
        else
        {
            await _Http.PostAsJsonAsync("https://localhost:7283/api/v1/invoice", formInvoice);
        }

        showForm = false;
        await LoadInvoices();
    }

    private async Task DeleteInvoice(int id)
    {
        await _Http.DeleteAsync($"https://localhost:7283/api/v1/invoice/{id}");
        await LoadInvoices();
    }
}