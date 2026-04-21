using E_Commerece.Core.Models;
using E_Commerece.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : EntityBase
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrderBy != null) { 
            
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDesnc != null) {

                query = query.OrderByDescending(spec.OrderByDesnc);
            }
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
