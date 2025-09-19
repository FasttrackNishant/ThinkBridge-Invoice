namespace ThinkBridge.Client.Components.Domain;

public class InvoiceItem
{
    public int ItemID { get; set; }
    public int InvoiceID { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}