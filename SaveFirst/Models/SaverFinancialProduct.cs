﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class SaverFinancialProduct
    {
        public int Id { get; set; }
        public int SaverId { get; set; }
        public int FinancialProductId { get; set; }
        public string Recurrence { get; set; } = String.Empty;
        public DateOnly PurchaseDate { get; set; }
        public string Reason { get; set; } = String.Empty;
        public int NumberOfShares { get; set; }
    }
}
