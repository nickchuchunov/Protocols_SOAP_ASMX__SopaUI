﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Services
{
    public interface IRepository<T>
    {
        int? Add(T item);
        int Update(T item);
        int Delete(T item);
        IList<T> GetAll();
        T GetById(string id);
    }
}
