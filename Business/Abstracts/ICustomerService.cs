using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstracts
{
    public interface ICustomerService
    {
        IResult Add(CustomerForRegisterDto customerForRegisterDto, int userId);
    }
}
