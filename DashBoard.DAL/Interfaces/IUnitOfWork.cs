﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.DAL.Identity;

namespace DashBoard.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
