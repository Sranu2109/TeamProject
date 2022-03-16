

using Core.Entities.SellerIdentity;

namespace Core.Interfaces
{
    public interface ISellerTokenService
    {
         string CreateToken(AppSeller seller);
    }
}