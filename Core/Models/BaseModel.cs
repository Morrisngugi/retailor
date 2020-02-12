using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public EntityStatus EntityStatus { get; set; } = EntityStatus.ACTIVE;
    }
}
