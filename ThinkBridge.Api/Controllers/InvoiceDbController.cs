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
        return Ok(invoices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> GetInvoice(int id)
    {
        var invoice = await _repository.GetByIdAsync(id);
        if (invoice == null) return NotFound();
        return Ok(invoice);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInvoice(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}