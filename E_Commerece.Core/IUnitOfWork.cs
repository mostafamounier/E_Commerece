using E_Commerece.Core.Models;
using E_Commerece.Core.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core
{
    public interface IUnitOfWork 
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:EntityBase;
        Task <int> Compelete();
    }
}
