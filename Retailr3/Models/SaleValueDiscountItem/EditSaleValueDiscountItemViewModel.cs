﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Retailr3.Models.SaleValueDiscountItem
{
    public class EditSaleValueDiscountItemViewModel
    {
        public Guid Id { get; set; }
        public Guid SaleValueDiscountId { get; set; }
        [DisplayName("Tier Name")]
        public string Tier { get; set; }
        [DisplayName("Sale Value")]
        public decimal SaleValue { get; set; } = 0M;
        [DisplayName("Discount Rate")]
        public decimal DiscountRate { get; set; } = 0M;
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
