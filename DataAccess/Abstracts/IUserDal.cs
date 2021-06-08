﻿using Core.DataAccess;
using Core.Entities;
using System.Collections.Generic;

namespace DataAccess.Abstracts
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
    }
}
