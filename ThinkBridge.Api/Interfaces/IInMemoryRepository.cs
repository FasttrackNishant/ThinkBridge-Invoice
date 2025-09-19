using ThinkBridge.Api.Models;

namespace ThinkBridge.Api.Interfaces;

public interface IInMemoryRepository
{
    IEnumerable<Invoice> GetAll();
    Invoice GetById(int id);
    void Add(Invoice invoice);
    void Update(Invoice invoice);
    void Delete(int id);
}
