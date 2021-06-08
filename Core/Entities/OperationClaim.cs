using System;

namespace Core.Entities
{
    public class OperationClaim: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifieddDate { get; set; }
        public bool IsActive { get; set; }
    }
}
