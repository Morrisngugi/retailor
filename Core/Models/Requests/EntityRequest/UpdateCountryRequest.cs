﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Requests.EntityRequest
{
    public class UpdateCountryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
