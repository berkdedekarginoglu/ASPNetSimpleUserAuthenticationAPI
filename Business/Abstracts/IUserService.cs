using Core.Entities;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstracts
{
    public interface IUserService
    {
        IDataResult<User> Add(IUserForRegisterDto userForRegisterDto);
        IResult DeleteById(int userId);

        IResult UpdateEmailById(int userId,string email);
        IResult UpdatePasswordByEmail(string email, string newPassword);
        IResult UpdatePasswordById(int userId, string newPassword);
        IDataResult<User> GetById(int userId);
        IDataResult<User> GetByEmail(string email);
        IDataResult<List<User>> GetAll();
        IDataResult<List<User>> GetAllByCreatedDates(DateTime startingDate, DateTime endDate);
        IDataResult<List<User>> GetAllByModifiedDates(DateTime startingDate, DateTime endDate);
        IDataResult<List<User>> GetAllByActivationStatus(bool status);
        List<OperationClaim> GetClaims(User user);
        
    }
}
