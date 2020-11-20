using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.CommonRepositories
{
    public class NotesRepository:Repository<note>
    {
        public List<note> GetNotice(string id)
        {
            return this.context.Set<note>().Where(x=>x.OwnerID==id).ToList();
        }

        public note searchNotice(int id)
        {
            return this.context.Set<note>().Find(id);
        }

        public void Delete(int id)
        {
            this.context.Set<note>().Remove(Get(id));
            this.context.SaveChanges();
        }

        public note Get(int id)
        {
            return this.context.Set<note>().Find(id);
        }
    }

}