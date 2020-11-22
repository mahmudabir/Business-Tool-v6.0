using System;
using BTv6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class SalesRepository:Repository<sale>
    {
        public List<sale> GetSaleProductByUser(string LID)
        {
            List<sale> saleList = this.GetAll().Where(x => x.SOLD_BY == LID).ToList();

            return saleList;
        }
    }
}