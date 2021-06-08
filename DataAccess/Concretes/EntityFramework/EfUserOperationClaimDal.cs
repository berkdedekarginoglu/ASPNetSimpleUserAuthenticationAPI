using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstracts;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim,EfUserAuthenticationSimpleContext> , IUserOperationClaimDal
    {
        
    }
}
