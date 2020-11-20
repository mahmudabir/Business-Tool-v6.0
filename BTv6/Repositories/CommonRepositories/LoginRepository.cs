using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class LoginRepository : Repository<log_in>
    {
        public log_in GetByID(string id)
        {
            return context.log_in.Where(x => x.LID == id).FirstOrDefault();
        }

        public void InsertByObj(log_in l)
        {
            this.context.log_in.Add(l);
            this.context.SaveChanges();
        }
    }
}