using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models
{
    public class Advertisement
    {
        public Advertisement()
        {
            // For generating test data via Bogus
        }

        public Advertisement(int? id, string title, string? description, Category category, DateTime createDate, 
            DateTime validTo, User user, Estate estate, decimal price, decimal? provision)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            CreateDate = createDate;
            ValidTo = validTo;
            User = user;
            Estate = estate;
            Price = price;
            Provision = provision;
        }

        private Advertisement(int? id, string title, string? description, DateTime createDate,
            DateTime validTo, decimal price, decimal? provision)
        {
            Id = id;
            Title = title;
            Description = description;
            CreateDate = createDate;
            ValidTo = validTo;
            Price = price;
            Provision = provision;
        }

        public int? Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Category Category { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ValidTo { get; set; }
        public User User { get; set; }
        public Estate Estate { get; set; }
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
                if (Estate?.Utilities == null)
                    return (utilitiesCost, additionaly);
                foreach (var utility in Estate.Utilities)
                    if (utility.Cost == null)
                        additionaly += " + " + utility.Name;
                    else
                        utilitiesCost += utility.Cost.Value;
                return (utilitiesCost, additionaly);
            }
        }
    }
}
