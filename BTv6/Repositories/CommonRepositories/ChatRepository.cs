using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class ChatRepository : Repository<chat>
    {
        public List<chat> GetAllByReceiverId(string id)
        {
            return this.context.Set<chat>().OrderByDescending(y=>y.MSG_ID).Where(x => x.RECEIVER == id && x.STATUS == 0).ToList();
        }

        public chat GetChatByID(int id)
        {
            return this.GetAll().Where(x => x.MSG_ID == id).FirstOrDefault();
        }
        
       public List<chat> GetAllSeenById(string id)
        {
            return this.context.Set<chat>().OrderByDescending(y => y.MSG_ID).Where(x => x.RECEIVER == id && x.STATUS == 1).ToList();
        }
    }
}