﻿using Caramel.DbModel.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Caramel.Models
{
    public partial class Customer
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int AddressId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int Archived { get; set; }
        public string Phone { get; set; }
        public int? RateId { get; set; }
        public int? OrderId { get; set; }
        public bool EmailConfirmed { get; set; }
        public string ConfirmationLink { get; set; }


        public virtual Address Address { get; set; }
        public virtual Order Order { get; set; }
        public virtual Rate Rate { get; set; }
        public int RoleId { get; set; }

    }
}
