using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class DiscountDomain : IDiscountDomain
{
    private IDiscountData _discountData;
    
    public DiscountDomain(IDiscountData discountData)
    {
        _discountData = discountData;
    }
    
    public async Task<int> SaveAsync(Client data)
    {
        if(data.membershipStatus != "regular" && data.membershipStatus != "silver" && data.membershipStatus != "gold")
        {
            throw new Exception("Invalid membership status");
        }
        if (data.membershipStatus == "gold")
        {
            data.produtPrice *= 0.8m;
        }
        else if (data.productType == "electronic" && data.produtPrice > 1000)
        {
            data.produtPrice *= 0.9m;
        }
        else if (data.productType == "Book")
        {
            //No discount applied
        }
        return await _discountData.SaveAsync(data);
    }

    public async Task<bool> UpdateAsync(Client data, int id)
    {
        return await _discountData.UpdateAsync(data, id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _discountData.DeleteAsync(id);
    }
}