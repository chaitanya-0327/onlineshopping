﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Foodcore.Models
{
    public partial class Cart
    {
        public int Cartid { get; set; }
        public string Email { get; set; }
        public string Itemid { get; set; }

        public virtual Register EmailNavigation { get; set; }
        public virtual Product Item { get; set; }
    }
}