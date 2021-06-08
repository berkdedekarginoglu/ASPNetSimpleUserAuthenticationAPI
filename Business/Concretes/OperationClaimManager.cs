using Business.Abstracts;
using Core.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concretes
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }
        public IResult Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }
        public IResult DeleteById(int id)
        {
            var selectedEntity = _operationClaimDal.Get(opc => opc.Id == id);
            if (selectedEntity == null)
            {
                selectedEntity.IsActive = false;
                _operationClaimDal.Update(selectedEntity);
                return new SuccessResult(Messages.OperationClaimDeleted);
            }
            return new ErrorResult(Messages.OperationClaimNotFound);
        }
        public IResult UpdateClaimNameById(int id,string claimName)
        {
            var selectedEntity = _operationClaimDal.Get(opc => opc.Id == id);
            if (selectedEntity == null)
            {
                selectedEntity.ModifieddDate = DateTime.Now;
                selectedEntity.Name = claimName;

                _operationClaimDal.Update(selectedEntity);
                return new SuccessResult(Messages.OperationClaimUpdated);
            }
            return new ErrorResult(Messages.OperationClaimNotFound);
        }
        public IDataResult<OperationClaim> GetById(int id)
        {
            var selectedEntity = _operationClaimDal.Get(opc => opc.Id == id);
            if (selectedEntity == null)
                return new ErrorDataResult<OperationClaim>(selectedEntity);
            return new SuccessDataResult<OperationClaim>(selectedEntity);
        }
        public IDataResult<OperationClaim> GetByName(string name)
        {
            var selectedEntity = _operationClaimDal.Get(opc => opc.Name == name);
            if (selectedEntity == null)
                return new ErrorDataResult<OperationClaim>(selectedEntity);
            return new SuccessDataResult<OperationClaim>(selectedEntity);
        }
        public IDataResult<List<OperationClaim>> GetAll()
        {
            var selectedEntity = _operationClaimDal.GetAll();
            if (selectedEntity.Count < 1)
                return new SuccessDataResult<List<OperationClaim>>(selectedEntity);
            return new ErrorDataResult<List<OperationClaim>>(Messages.OperationClaimNotFound);
        }
        public IDataResult<List<OperationClaim>> GetAllActivationStatus(bool activationStatus)
        {
            var selectedEntity = _operationClaimDal.GetAll(opc => opc.IsActive == activationStatus);
            if (selectedEntity.Count < 1)
                return new SuccessDataResult<List<OperationClaim>>(selectedEntity);
            return new ErrorDataResult<List<OperationClaim>>(Messages.OperationClaimNotFound);
        }
        public IDataResult<List<OperationClaim>> GetAllCreatedDates(DateTime startingDate, DateTime endDate)
        {
            var selectedEntity = _operationClaimDal.GetAll(opc => opc.CreatedDate >= startingDate && opc.CreatedDate <= endDate);
            if (selectedEntity.Count < 1)
                return new SuccessDataResult<List<OperationClaim>>(selectedEntity);
            return new ErrorDataResult<List<OperationClaim>>(Messages.OperationClaimNotFound);
        }
        public IDataResult<List<OperationClaim>> GetAllModifiedDates(DateTime startingDate, DateTime endDate)
        {
            var selectedEntity = _operationClaimDal.GetAll(opc => opc.ModifieddDate >= startingDate && opc.ModifieddDate <= endDate);
            if (selectedEntity.Count < 1)
                return new SuccessDataResult<List<OperationClaim>>(selectedEntity);
            return new ErrorDataResult<List<OperationClaim>>(Messages.OperationClaimNotFound);
        }
    }
}
