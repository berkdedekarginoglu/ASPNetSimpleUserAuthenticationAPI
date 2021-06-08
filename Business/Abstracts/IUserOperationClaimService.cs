using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;

namespace Business.Abstracts
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim userOperationClaim);
        IResult UpdateUserIdById(int id,int userId);
        IResult UpdateOperationClaimIdById(int id, int operationClaimId);
        IResult DeleteById(int id);
        IDataResult<UserOperationClaim> GetById(int id);
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<List<UserOperationClaim>> GetAllByOperationClaimId(int operationClaimId);
        IDataResult<List<UserOperationClaim>> GetAllByUserId(int userId);
        IDataResult<List<UserOperationClaim>> GetAllByCreatedDates(DateTime startingDate, DateTime endDate);
        IDataResult<List<UserOperationClaim>> GetAllByModifiedDates(DateTime startingDate, DateTime endDate);
        IDataResult<List<UserOperationClaim>> GetAllByActivationStatus(bool activationStatus);
    }
}
