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
        public void UpdateUser(customer cusToUpdate)
        {
            //employee emp = GetByID(empToUpdate.EmpID);

            //emp.E_NAME = empToUpdate.E_NAME;

            this.Update(cusToUpdate);
        }
    }
}