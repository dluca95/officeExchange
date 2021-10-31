using System;

namespace ExchangeOffice.Persistence.Entities
{
    public abstract class EntityModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}