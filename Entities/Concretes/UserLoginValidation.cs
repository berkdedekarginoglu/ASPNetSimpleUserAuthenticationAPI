using Core.Entities;
using System;

namespace Entities.Concretes
{
    public class UserLoginValidation : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
        public byte[] AuthenticationCodeHash { get; set; }
        public byte[] AuthenticationCodeSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSuccess { get; set; }
    }
}
