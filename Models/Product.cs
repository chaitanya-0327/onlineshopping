// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Foodcore.Models
{
    public partial class Product
    {
        public Product()
        {
            Carts = new HashSet<Cart>();
            Feedbacks = new HashSet<Feedback>();
            Myorders = new HashSet<Myorder>();
        }

        public string Productid { get; set; }
        public string Productname { get; set; }
        public string Productdesc { get; set; }
        public string Category { get; set; }
        public int? Price { get; set; }
        public string Images { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Myorder> Myorders { get; set; }
    }
}