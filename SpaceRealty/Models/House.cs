using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceRealty.Models
{
    public class House
    {
        public int MLSNum { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public decimal SalesPrice { get; set; }
        public DateTime DateListed { get; set; }
        public int Bedrooms { get; set; }
        public decimal Bathrooms { get; set; }
        public int GarageSize { get; set; }
        public int SquareFeet { get; set; }
        public int LotSize { get; set; }
        public string Description { get; set; }
        public bool Edit { get; set; }
    }
}
