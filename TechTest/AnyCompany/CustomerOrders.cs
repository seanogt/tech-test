﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyCompany
{
   public  class CustomerOrders
    {
        public Customer customer { get; set; }

        public List<Order> orders { get; set; }
    }
}
