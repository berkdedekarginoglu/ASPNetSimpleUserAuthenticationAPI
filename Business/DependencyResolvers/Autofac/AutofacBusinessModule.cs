using Autofac;
using Business.Abstracts;
using Business.Adapters.MailService;
using Business.Concretes;
using Core.CrossCuttingConcerns.Mail;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Security.JWT;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concretes;
using Entities.DTOs;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<UserAuthManager>().As<IUserAuthService>();
            builder.RegisterType<JwtHelper>().As<IJwtHelper>();
            builder.RegisterType<MailServiceManager>().As<IMailService>();
            builder.RegisterType<UserLoginValidationManager>().As<IUserLoginValidationService>();
            builder.RegisterType<EfUserLoginValidationDal>().As<IUserLoginValidationDal>();
            builder.RegisterType<EfUserForgotPasswordDal>().As<IUserForgotPasswordDal>();
            builder.RegisterType<UserForgotPasswordManager>().As<IUserForgotPasswordService>();
        }
    }
}
