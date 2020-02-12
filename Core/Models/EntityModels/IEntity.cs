using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.EntityModels
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime LastUpdated { get; set; }
        string Code { get; set; }
        //EntityStatus EntityStatus { get; set; }
    }
}
