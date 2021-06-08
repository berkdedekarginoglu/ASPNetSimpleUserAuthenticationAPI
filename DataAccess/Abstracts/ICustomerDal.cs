using Core.DataAccess;
using Entities.Abstracts;
using Entities.Concretes;

namespace DataAccess.Abstracts
{
    public interface ICustomerDal
        : IEntityRepository<Customer>
    {
    }
}
