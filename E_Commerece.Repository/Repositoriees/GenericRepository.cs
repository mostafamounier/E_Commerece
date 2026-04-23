using E_Commerece.Core.Models;
using E_Commerece.Core.Repositories;
using E_Commerece.Core.Specifications;
using E_Commerece.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Repository.Repositoriees
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext _Context)
        {
            context = _Context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>)await context.Set<T>().Include("ProductBrand").Include("ProductType").ToListAsync();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public Task<int> GetCountWithSpecFilteration(ISpecification<T> spec)
        {
            return ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutor<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        }


    }
}
