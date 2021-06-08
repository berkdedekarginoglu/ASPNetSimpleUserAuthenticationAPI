using Business.Abstracts;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.DTOs;

namespace Business.Concretes
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(CustomerForRegisterDto customerForRegisterDto,int userId)
        {
            var newCustomer = new Customer
            {
                Balance = 0,
                FirstName = customerForRegisterDto.FirstName,
                LastName = customerForRegisterDto.LastName,
                IdentityNumber = string.Empty,
                PhoneNumber = string.Empty,
                DateOfBirth = customerForRegisterDto.DateOfBirth.ToShortDateString(),
                IsEmailVerified = true,
                IsIdentityNumberVerified = false,
                IsPhoneNumberVerified = false,
                UserId = userId
            };

            _customerDal.Add(newCustomer);
            return new SuccessResult(Messages.CustomerAdded);
        }
    }
}
