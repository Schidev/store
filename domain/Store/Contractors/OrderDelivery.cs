﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderDelivery
    { 
        public string UniqueCode { get;}
        public string Description { get; }
        public decimal Price { get; }
        public IReadOnlyDictionary<string, string> Parameters { get; }

        public OrderDelivery(string uniqueCode,
                             string description,
                             decimal price,
                             IReadOnlyDictionary<string, string> parameters)
        {
            if (string.IsNullOrWhiteSpace(uniqueCode))
            {
                throw new ArgumentException(nameof(uniqueCode));
            }

            if (string.IsNullOrWhiteSpace(description)) 
            {
                throw new ArgumentException(nameof(description));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            this.UniqueCode = uniqueCode;
            this.Description = description;
            this.Price = price;
            this.Parameters = parameters;

        }
    }
}
