using Microsoft.AspNetCore.Mvc;
using ThinkBridge.Api.Interfaces;
using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class InvoiceDbController : ControllerBase
{
    private readonly IInvoiceRepository _repository;

    public InvoiceDbController(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
    {
        var invoices = await _repository.GetAllAsync();
        
        var invoiceDtos = invoices.Select(invoice => new InvoiceDto
        {
            InvoiceID = invoice.InvoiceID,
            CustomerName = invoice.CustomerName,
            Items = invoice.Items.Select(ii => new InvoiceItemDto
            {
                ItemID = ii.ItemID,
                Name = ii.Name,
                Price = ii.Price
            }).ToList()
        });

        return Ok(invoiceDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoice(int id)
    {
        var invoice = await _repository.GetByIdAsync(id);
        
        if (invoice == null) return NotFound();
        
        var invoiceDto = new InvoiceDto
        {
            InvoiceID = invoice.InvoiceID,
            CustomerName = invoice.CustomerName,
            Items = invoice.Items.Select(ii => new InvoiceItemDto
            {
                ItemID = ii.ItemID,
                Name = ii.Name,
                Price = ii.Price
            }).ToList()
        };

        return Ok(invoiceDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddInvoiceDto dto)
    {
        var invoice = new Invoice
        {
            CustomerName = dto.CustomerName,
            Items = dto.Items.Select(i => new InvoiceItem
            {
                Name = i.Name,
                Price = i.Price
            }).ToList()
        };

        var added = await _repository.AddInvoiceAsync(invoice);

        var invoiceDto = new InvoiceDto
        {
            InvoiceID = added.InvoiceID,
            CustomerName = added.CustomerName,
            Items = added.Items.Select(ii => new InvoiceItemDto
            {
                ItemID = ii.ItemID,
                Name = ii.Name,
                Price = ii.Price
            }).ToList()
        };

        return CreatedAtAction(nameof(GetInvoice), new { id = added.InvoiceID }, invoiceDto);
    }
    
}