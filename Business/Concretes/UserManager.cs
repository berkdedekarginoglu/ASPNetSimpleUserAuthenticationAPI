using Business.Abstracts;
using Core.Constants;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstracts;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Concretes
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IDataResult<User> Add(IUserForRegisterDto userDto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.GeneratedHash(out passwordHash, out passwordSalt, userDto.Password);

            User createdUser = new User()
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
            };
            _userDal.Add(createdUser);
            return new SuccessDataResult<User>(createdUser, Messages.UserAdded);
        }
        public IResult DeleteById(int userId)
        {
            var selectedentity = _userDal.Get(u => u.Id == userId);
            if (selectedentity == null)
                return new ErrorResult(Messages.UserNotFound);
            selectedentity.IsActive = false;
            _userDal.Update(selectedentity);
            return new SuccessResult(Messages.UserDeleted);
        }
        public IResult UpdateEmailById(int userId, string email)
        {
            var selectedEntity = _userDal.Get(u => u.Id == userId);
            if (selectedEntity == null)
                return new ErrorResult(Messages.UserNotFound);

            selectedEntity.Email = email;
            selectedEntity.ModifiedDate = DateTime.Now;
            return new SuccessResult(Messages.UserUpdated);
        }
        public IResult UpdatePasswordByEmail(string email, string newPassword)
        {
            var selectedUser = _userDal.Get(u => u.Email == email);

            byte[] passwordHash, passwordSalt;
            HashingHelper.GeneratedHash(out passwordHash, out passwordSalt, newPassword);
            selectedUser.PasswordHash = passwordHash;
            selectedUser.PasswordSalt = passwordSalt;
            _userDal.Update(selectedUser);
            return new SuccessResult();
        }
        public IResult UpdatePasswordById(int userId, string newPassword)
        {
            var selectedUser = _userDal.Get(u => u.Id == userId);

            byte[] passwordHash, passwordSalt;
            HashingHelper.GeneratedHash(out passwordHash, out passwordSalt, newPassword);
            selectedUser.PasswordHash = passwordHash;
            selectedUser.PasswordSalt = passwordSalt;
            _userDal.Update(selectedUser);
            return new SuccessResult();
        }
        public IDataResult<List<User>> GetAll()
        {
            var selectedEntities = _userDal.GetAll();
            if (selectedEntities.Count < 1)
                return new ErrorDataResult<List<User>>(Messages.UserNotFound);
            return new SuccessDataResult<List<User>>(selectedEntities);
        }
        public IDataResult<List<User>> GetAllByActivationStatus(bool status)
        {
            var selectedEntities = _userDal.GetAll(u => u.IsActive == status);
            if (selectedEntities.Count < 1)
                return new ErrorDataResult<List<User>>(Messages.UserNotFound);
            return new SuccessDataResult<List<User>>(selectedEntities);
        }
        public IDataResult<List<User>> GetAllByCreatedDates(DateTime startingDate, DateTime endDate)
        {
            var selectedEntities = _userDal.GetAll(u => u.CreatedDate >= startingDate && u.CreatedDate <= endDate);
            if (selectedEntities.Count < 1)
                return new ErrorDataResult<List<User>>(Messages.UserNotFound);
            return new SuccessDataResult<List<User>>(selectedEntities);
        }
        public IDataResult<List<User>> GetAllByModifiedDates(DateTime startingDate, DateTime endDate)
        {
            var selectedEntities = _userDal.GetAll(u => u.ModifiedDate >= startingDate && u.ModifiedDate <= endDate);
            if (selectedEntities.Count < 1)
                return new ErrorDataResult<List<User>>(Messages.UserNotFound);
            return new SuccessDataResult<List<User>>(selectedEntities);
        }
        public IDataResult<User> GetByEmail(string email)
        {
            var selectedEntity = _userDal.Get(u => u.Email == email);
            if (selectedEntity == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);
            return new SuccessDataResult<User>(selectedEntity);
        }
        public IDataResult<User> GetById(int userId)
        {
            var selectedEntity = _userDal.Get(u => u.Id == userId);
            if (selectedEntity == null)
                return new ErrorDataResult<User>(Messages.UserNotFound);
            return new SuccessDataResult<User>(selectedEntity);
        }
        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);

        }

      
    }
}