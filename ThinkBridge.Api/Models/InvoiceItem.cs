using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ThinkBridge.Api.Models;

public class InvoiceItem
{
    [Key] public int ItemID { get; set; }
    public int InvoiceID { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Invoice Invoice { get; set; }
}