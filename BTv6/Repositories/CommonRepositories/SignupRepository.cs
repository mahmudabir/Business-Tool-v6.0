using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class SignupRepository : Repository<customer>
    {
        public bool CheckUser(customer c)
        {
            customer user = context.customers.Where(x => x.cusid == c.cusid).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                if (user.cusid == c.cusid)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}