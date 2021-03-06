﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class AddSettingRequest
    {
        public Guid BaseEntityId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool SOAutoApproval { get; set; }
        public bool POAutoApproval { get; set; }
        public bool LotTracking { get; set; }
    }
}
