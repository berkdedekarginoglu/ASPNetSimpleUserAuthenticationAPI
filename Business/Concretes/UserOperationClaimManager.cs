using Business.Abstracts;
using Core.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;

namespace Business.Concretes
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }
        public IResult Add(UserOperationClaim userOperationClaim)
        {
            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult();
        }
        public IResult DeleteById(int id)
        {
            var selectedEntity = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            if (selectedEntity == null)
                return new ErrorResult(Messages.UserOperationClaimNotFound);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }
        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            var selectedEntity = _userOperationClaimDal.GetAll();
            if (selectedEntity.Count<1)
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(selectedEntity);
        }
        public IDataResult<List<UserOperationClaim>> GetAllByActivationStatus(bool activationStatus)
        {
            var selectedEntity = _userOperationClaimDal.GetAll(uoc => uoc.IsActive == activationStatus);
            if (selectedEntity.Count < 1)
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(selectedEntity);
        }
        public IDataResult<List<UserOperationClaim>> GetAllByCreatedDates(DateTime startingDate, DateTime endDate)
        {
            var selectedEntity = _userOperationClaimDal.GetAll(uoc => uoc.CreatedDate >= startingDate && uoc.CreatedDate <= endDate);
            if (selectedEntity.Count < 1)
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(selectedEntity);
        }
        public IDataResult<List<UserOperationClaim>> GetAllByModifiedDates(DateTime startingDate, DateTime endDate)
        {
            var selectedEntity = _userOperationClaimDal.GetAll(uoc => uoc.ModifiedDate >= startingDate && uoc.ModifiedDate <= endDate);
            if (selectedEntity.Count < 1)
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(selectedEntity);
        }
        public IDataResult<List<UserOperationClaim>> GetAllByOperationClaimId(int operationClaimId)
        {
            var selectedEntity = _userOperationClaimDal.GetAll(uoc => uoc.OperationClaimId == operationClaimId);
            if (selectedEntity.Count < 1)
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(selectedEntity);
        }
        public IDataResult<List<UserOperationClaim>> GetAllByUserId(int userId)
        {
            var selectedEntity = _userOperationClaimDal.GetAll(uoc => uoc.UserId == userId);
            if (selectedEntity.Count < 1)
                return new ErrorDataResult<List<UserOperationClaim>>(Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<List<UserOperationClaim>>(selectedEntity);
        }
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            var selectedEntity = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            if (selectedEntity==null)
                return new ErrorDataResult<UserOperationClaim>      (Messages.UserOperationClaimNotFound);
            return new SuccessDataResult<UserOperationClaim>(selectedEntity);
        }
        public IResult UpdateOperationClaimIdById(int id, int operationClaimId)
        {
            var selectedEntity = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            if (selectedEntity == null)
                return new ErrorDataResult<UserOperationClaim>(Messages.UserOperationClaimNotFound);

            selectedEntity.OperationClaimId = operationClaimId;
            return new SuccessDataResult<UserOperationClaim>(selectedEntity);
        }
        public IResult UpdateUserIdById(int id, int userId)
        {
            var selectedEntity = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            if (selectedEntity == null)
                return new ErrorDataResult<UserOperationClaim>(Messages.UserOperationClaimNotFound);

            selectedEntity.UserId = userId;
            return new SuccessDataResult<UserOperationClaim>(selectedEntity);
        }
    }
}
