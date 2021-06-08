using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstracts;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim,EfUserAuthenticationSimpleContext> , IOperationClaimDal
    {
    }
}
