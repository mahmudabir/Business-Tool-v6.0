using System;
using BTv6.Models;
using BTv6.Repositories.CommonRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BTv6.Repositories.CommonRepositories
{
    public class Profile_imagesRepository:Repository<profile_images>
    {
        public profile_images GetByID(string id)
        {
            return context.profile_images.Where(x => x.UID == id).FirstOrDefault();
        }

        public void InsertByObj(profile_images p)
        {
            this.context.profile_images.Add(p);
            this.context.SaveChanges();
        }
        /*public void UpdateImage(profile_images images)
        {
            this.context.Entry(images).State = EntityState.Modified;
            this.context.SaveChanges();
        }*/
    }
}