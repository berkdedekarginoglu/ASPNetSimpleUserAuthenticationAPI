using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfUserForgotPasswordDal :EfEntityRepositoryBase<UserForgotPassword,EfUserAuthenticationSimpleContext> , IUserForgotPasswordDal
    {
    }
}
