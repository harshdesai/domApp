﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleApp.Entity;
namespace SampleApp.BLL.Interface
{
    public interface ICategoryRepository
    {
        List<Catagory> GetCategory();
    }
}
