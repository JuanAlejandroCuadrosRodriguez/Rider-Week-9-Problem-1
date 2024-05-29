using Microsoft.Build.Framework;

namespace _1.API.Request;

public class ClientRequest
{
    [Required]
    public string membershipStatus { get; set; }
    [Required]
    public string productType { get; set; }
    [Required]
    public decimal produtPrice { get; set; }

    
}