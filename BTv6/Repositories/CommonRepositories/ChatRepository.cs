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
            return this.context.Set<chat>().Where(x => x.RECEIVER == id).ToList();
        }

        public List<chat> GetAllBySenderId(string id)
        {
            return this.context.Set<chat>().Where(x => x.SENDER == id).ToList();
        }
    }
}