﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.CertainValueCertainProductDiscountItem
{
    public class EditCertainValueCertainProductDiscountItemViewModel
    {
        public Guid CertainValueCertainProductDiscountId { get; set; }
        public Guid Id { get; set; }
        [DisplayName("Product Name")]
        public Guid ProductId { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("Value")]
        public decimal Value { get; set; } = 0M;
        [DisplayName("Quantity")]
        public decimal Quantity { get; set; } = 0M;
        [DisplayName("Effective Date")]
        public DateTime EffectiveDate { get; set; }
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }
        [DisplayName("Last Updated")]
        public DateTime DateLastUpdated { get; set; }
    }
}
