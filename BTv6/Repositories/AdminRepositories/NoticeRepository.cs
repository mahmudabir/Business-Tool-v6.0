using BTv6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTv6.Repositories.AdminRepositories
{
    public class NoticeRepository : Repository<notice>
    {
        public notice GetByID(int id)
        {
            return context.notices.Where(x => x.noticeID == id).FirstOrDefault();
        }
        public void DeleteNoticeByID(int id)
        {
            this.context.notices.Remove(GetByID(id));
            this.context.SaveChanges();
        }
    }
}