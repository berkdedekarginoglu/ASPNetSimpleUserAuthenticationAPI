using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;

namespace Business.Abstracts
{
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim operationClaim);
        IResult UpdateClaimNameById(int id,string claimName);
        IResult DeleteById(int id);

        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<List<OperationClaim>> GetAllCreatedDates(DateTime startingDate, DateTime endDate);
        IDataResult<List<OperationClaim>> GetAllModifiedDates(DateTime startingDate, DateTime endDate);
        IDataResult<List<OperationClaim>> GetAllActivationStatus(bool activationStatus);
        IDataResult<OperationClaim> GetById(int id);
        IDataResult<OperationClaim> GetByName(string name);
    }
}
