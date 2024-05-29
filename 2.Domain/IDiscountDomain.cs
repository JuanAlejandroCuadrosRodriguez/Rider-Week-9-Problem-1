using _3.Data.Models;

namespace _2.Domain;

public interface IDiscountDomain
{
    Task<int> SaveAsync(Client data);
    
    Task<Boolean> UpdateAsync(Client data, int id);
    
    Task<Boolean> DeleteAsync(int id);

}