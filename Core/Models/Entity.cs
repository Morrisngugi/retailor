using Core.Models.EntityModels;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public string Code { get; set; }
        public EntityStatus EntityStatus { get; set; } = EntityStatus.ACTIVE;

    }
}
