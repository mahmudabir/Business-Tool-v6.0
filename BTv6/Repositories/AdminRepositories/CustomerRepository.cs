using System;
using BTv6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.AdminRepositories
{
    public class CustomerRepository:Repository<customer>
    {
        public customer GetByID(string id)
        {
            return context.customers.Where(x => x.cusid == id).FirstOrDefault();
        }

        public List<customer> GetByStatus(int status)
        {
            return context.customers.Where(x => x.status == status).ToList();
        }

        public List<customer> GetByNotStatus(int status)
        {
            return context.customers.Where(x => x.status != status).ToList();
        }
        public void UpdateUser(customer cusToUpdate)
        {
            
            this.Update(cusToUpdate);
        }

        public void DeleteCustomerByID(string id)
        {
            this.context.customers.Remove(GetByID(id));
            this.context.SaveChanges();
        }
    }
}