using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.AdminRepositories
{
    public class EmployeeRepository : Repository<employee>
    {
        public bool CheckUser(employee c)
        {
            log_in user = context.log_in.Where(x => x.LID == c.EmpID).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
            {
                if (user.LID == c.EmpID)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }

        public void InsertByObj(employee e)
        {
            this.context.employees.Add(e);
            this.context.SaveChanges();
        }

        public employee GetByID(string id)
        {
            return context.employees.Where(x => x.EmpID == id).FirstOrDefault();
        }
        public void UpdateUser(employee empToUpdate)
        {
            //employee emp = GetByID(empToUpdate.EmpID);

            //emp.E_NAME = empToUpdate.E_NAME;

            this.Update(empToUpdate);
        }

    }
}