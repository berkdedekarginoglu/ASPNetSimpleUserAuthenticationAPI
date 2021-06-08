using Core.Entities;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes.EntityFramework
{
    public class EfUserAuthenticationSimpleContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=UserAuthenticationSimpleDB;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CountryCode> CountryCodes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<UserLoginValidation> UserLoginValidations { get; set; }
        public DbSet<UserForgotPassword> UserForgotPasswords { get; set; }
    }
}
