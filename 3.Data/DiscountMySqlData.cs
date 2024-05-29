using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class DiscountMySqlData : IDiscountData
{
    private DiscountDBContext _discountDbContext;
    
    public DiscountMySqlData(DiscountDBContext discountDbContext)
    {
        _discountDbContext = discountDbContext;
    }
    
    public async Task<int> SaveAsync(Client data)
    {
        data.IsActive = true;
        
        using (var transaction = await _discountDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                _discountDbContext.Clients.Add(data);
                await _discountDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        return data.Id;
    }

    public async Task<Boolean> UpdateAsync(Client data, int id)
    {
        using (var transaction = await _discountDbContext.Database.BeginTransactionAsync())
        {
            var clientToUpdate = _discountDbContext.Clients.Where(t => t.Id == id).FirstOrDefault();
            clientToUpdate.membershipStatus = data.membershipStatus;
            clientToUpdate.productType = data.productType;
            clientToUpdate.produtPrice = data.produtPrice;
            

            _discountDbContext.Clients.Update(clientToUpdate);
            await _discountDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        return true;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _discountDbContext.Database.BeginTransactionAsync())
        {
            var clientToDelete = _discountDbContext.Clients.Where(t => t.Id == id).FirstOrDefault();
            clientToDelete.IsActive = false;

            await _discountDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        return true;
    }

    public async Task<List<Client>> getAllAsync()
    {
        return await _discountDbContext.Clients.Where(t => t.IsActive)
            .ToListAsync();
    }

    public async Task<Client> getByIdAsync(int Id)
    {
        return await _discountDbContext.Clients.Where(t => t.Id == Id)
            .FirstOrDefaultAsync();
    }
}