using Core.Entities;
using Entities.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concretes
{
    public class Customer : ICustomer
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentityNumber { get; set; }
        public string DateOfBirth { get; set; }
        public decimal Balance { get; set; }
        public bool IsIdentityNumberVerified { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsPhoneNumberVerified { get; set; }
    }
}
