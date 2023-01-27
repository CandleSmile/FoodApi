﻿using DataLayer.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Interfaces
{
    public interface ICategoryRepository:IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(string name);
    }
}