using Core.Entities;
using System;

namespace Entities.Concretes
{
    public class CountryCode : IEntity
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public int Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
