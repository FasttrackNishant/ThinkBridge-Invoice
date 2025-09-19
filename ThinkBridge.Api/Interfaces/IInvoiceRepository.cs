using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Interfaces;

public interface IInvoiceRepository
{
    Task<IEnumerable<Invoice>> GetAllAsync();
    Task<Invoice?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}