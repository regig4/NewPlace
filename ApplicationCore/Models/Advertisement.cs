using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Advertisement
    {
        public int? Id { get; set; }
        public User User { get; set; }
        public Apartment Apartment { get; set; }
        public decimal Price { get; set; }
        public decimal? Provision { get; set; }

        public string TotalCost
        {
            get
            {
                var total = Price + Provision ?? 0;
                return total + UtilitesCost.Cost + UtilitesCost.Additionaly;
            }
        }

        private (decimal Cost, string Additionaly) UtilitesCost
        {
            get
            {
                decimal utilitiesCost = 0;
                string additionaly = String.Empty;
                foreach (var utility in Apartment.Utilities)
                    if (utility.Cost == null)
                        additionaly += " + " + utility.Name;
                    else
                        utilitiesCost += utility.Cost.Value;
                return (utilitiesCost, additionaly);
            }
        }
    }
}
