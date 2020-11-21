using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class OrderRepository : Repository<order>
    {
        public List<order> GetConfirmedOrderByUser(string LID)
        {
            List<order> confirmedOrderList = this.GetAll().Where(x => x.orderby == LID && x.stat == "1").ToList();

            return confirmedOrderList;
        }

        public List<order> GetRecievedOrderByUser(string LID)
        {
            List<order> recievedOrderList = this.GetAll().Where(x => x.orderby == LID && x.stat == "2").ToList();

            return recievedOrderList;
        }

        public List<order> GetPendingOrderByUser(string LID)
        {
            List<order> recievedOrderList = this.GetAll().Where(x => x.orderby == LID && x.stat == "0").ToList();

            return recievedOrderList;
        }

        public List<order>GetPendingOrder(string stat)
        {
            return context.orders.Where(o => o.stat == stat).ToList();
            
        }

        public order GetOrderByID(int ID)
        {
            order ordr = this.GetAll().Where(x => x.orderid == ID).FirstOrDefault();

            return ordr;
        }
        
        public void DeleteOrderByID(int id)
        {
            this.context.orders.Remove(GetOrderByID(id));
            this.context.SaveChanges();
        }

        public List<order> GetPendingDeliveryList(string LID)
        {
            List<order> pendingList = this.GetAll().Where(x => x.deliveryby == LID && x.stat == "1").ToList();

            return pendingList;
        }

        public List<order> GetAcceptedList(string LID)
        {
            List<order> acceptedList = this.GetAll().Where(x => x.deliveryby == LID && x.stat == "2").ToList();

            return acceptedList;
        }
        public void UpdateOrderByID(order order)
        {
            this.context.Entry(order).State = EntityState.Modified;
            this.context.SaveChanges();
        }
        
    }
}