using Core.Entities;
using System;

namespace Entities.Concretes
{
    public class UserForgotPassword : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid Guid { get; set; }
        public byte[] ResetCodeHash { get; set; }
        public byte[] ResetCodeSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSuccess { get; set; }
    }
}
