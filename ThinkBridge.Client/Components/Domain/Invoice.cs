namespace ThinkBridge.Client.Components.Domain;

public class Invoice
{
    public int InvoiceID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<InvoiceItem> Items { get; set; } = new();
}