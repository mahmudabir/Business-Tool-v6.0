using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class ProductRepository : Repository<product>
    {
        product prod = new product();
        public product GetProductByID(string PID)
        {
            return this.GetAll().Where(x => x.PID == PID).FirstOrDefault();
        }
        public void UpdateQuantity(product p, string PID)
        {

            prod = GetProductByID(PID);


            prod.QUANTITY = p.QUANTITY;
            this.Update(prod);


        }

        public void UpdateQuantityById(string PID,int quantity)
        {
            prod = GetProductByID(PID);
            prod.QUANTITY = quantity;
            this.Update(prod);
        }

    }
}