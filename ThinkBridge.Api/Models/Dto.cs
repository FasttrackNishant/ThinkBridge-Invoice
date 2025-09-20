namespace ThinkBridge.Api.Models;

// For fetching
public class InvoiceDto
{
    public int InvoiceID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public List<InvoiceItemDto> Items { get; set; } = new();
}

public class InvoiceItemDto
{
    public int ItemID { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}


// For Adding
public class AddInvoiceDto
{
    public string CustomerName { get; set; } = string.Empty;
    public List<AddInvoiceItemDto> Items { get; set; } = new();
}

public class AddInvoiceItemDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}