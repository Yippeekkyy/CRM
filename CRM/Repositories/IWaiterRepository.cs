using MyCRM.Model;

namespace MyCRM.Repositories;

public interface IWaiterRepository
{
    Task<Waiter?> CreateAsync(Waiter w);
    Task<IEnumerable<Waiter>> RetrieveAllAsync();
    Task<Waiter?> RetrieveAsync(int id);
    Task<Waiter?> UpdateAsync(int id, Waiter w);
    Task<bool?> DeleteAsync(int id);
}