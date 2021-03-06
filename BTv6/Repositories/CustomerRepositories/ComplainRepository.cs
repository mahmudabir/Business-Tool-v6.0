﻿using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CustomerRepositories
{
    public class ComplainRepository : Repository<complain>
    {
        public complain GetByID(int id)
        {
            return context.complains.Where(x => x.cID == id).FirstOrDefault();
        }
    }
}