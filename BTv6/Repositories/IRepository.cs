﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTv6.Repositories
{
    interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
    }
}
