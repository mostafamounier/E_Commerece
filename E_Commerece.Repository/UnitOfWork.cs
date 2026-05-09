using E_Commerece.Core;
using E_Commerece.Core.Models;
using E_Commerece.Core.Repositories;
using E_Commerece.Repository.Data;
using E_Commerece.Repository.Repositoriees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Repository
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly StoreContext context;
        private Hashtable _repositores;

        public UnitOfWork(StoreContext Context)
        {
            context = Context;
            _repositores = new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            var type = typeof(TEntity).Name;
            if (!_repositores.ContainsKey(type))
            {
               var repo =new GenericRepository<TEntity>(context);
                _repositores.Add(type, repo);
            }
            return _repositores[type] as IGenericRepository<TEntity>;
        }

        public async Task<int> Compelete()
        {
           return await context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            context.Dispose();
        }
    }
}
