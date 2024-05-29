using _3.Data.Models;
namespace _3.Data;

public interface IDiscountData
{
    Task<int> SaveAsync(Client data);
    
    Task<Boolean> UpdateAsync(Client data, int id);
    
    Task<Boolean> DeleteAsync(int id);
    
    Task<List<Client>> getAllAsync();
    
    Task<Client> getByIdAsync(int Id);
    
}