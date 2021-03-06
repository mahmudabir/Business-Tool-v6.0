﻿using BTv6.Models;
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

        public List<product> GetAllProductsByName(string P_NAME)
        {
            return this.GetAll().Where(x => x.P_NAME == P_NAME).ToList();
        }


        public void UpdateQuantity(product p, string PID)
        {

            prod = GetProductByID(PID);

            prod.QUANTITY = p.QUANTITY;
            this.Update(prod);
        }

        public bool CheckProduct(product c)
        {
            product prod = context.products.Where(x => x.PID == c.PID).FirstOrDefault();

            if (prod == null)
            {
                return false;
            }
            else
            {
                if (prod.PID == c.PID)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }

        public void InsertByObj(product p)
        {
            this.context.products.Add(p);
            this.context.SaveChanges();
        }

        public void UpdateQuantityById(string PID, int quantity)
        {
            prod = GetProductByID(PID);
            prod.QUANTITY = quantity;
            this.Update(prod);
        }

        public List<product> GetAvailableProduct()
        {
            return this.context.Set<product>().Where(x => x.QUANTITY > 0).ToList();
        }

        public List<product> SearchProduct(string search)
        {

            return this.GetAll().Where(x => x.AVAILABILITY == "AVAILABLE" && x.QUANTITY > 0 && (x.P_NAME.ToLower().Contains(search.ToLower()) || (x.TYPE.ToLower().Contains(search.ToLower())))).ToList();
        }


    }
}