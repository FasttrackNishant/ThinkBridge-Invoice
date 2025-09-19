using Microsoft.AspNetCore.Mvc;
using ThinkBridge.Api.Infrastructure;
using ThinkBridge.Api.Interfaces;
using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    // Implmented Both Implementation as SQL Server Stopped Working for Machine meanwhile
    // In Memory
    
    private readonly IInMemoryRepository _repository;
    
    public InvoiceController(IInMemoryRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Invoice>> GetInvoices()
    {
        return Ok(_repository.GetAll());
    }
    
    // GET: api/v1/invoice/5
    [HttpGet("{id}")]
    public ActionResult<Invoice> GetInvoice(int id)
    {
        var invoice = _repository.GetById(id);
    
        if (invoice == null) return NotFound();
    
        return Ok(invoice);
    }
    
    [HttpPost]
    public ActionResult<Invoice> CreateInvoice(Invoice invoice)
    {
        _repository.Add(invoice);
        return CreatedAtAction(nameof(GetInvoice), new { id = invoice.InvoiceID }, invoice);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateInvoice(int id, Invoice invoice)
    {
        if (id != invoice.InvoiceID) return BadRequest();
        _repository.Update(invoice);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteInvoice(int id)
    {
        _repository.Delete(id);
        return NoContent();
    }
}