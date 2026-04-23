using E_Commerece.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerece.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : EntityBase
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get; set ; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesnc { get ; set ; }
        public int Skip { get ; set; }
        public int Take { get ; set ; }
        public bool Pagniation_Enabled { get; set ; }

        public BaseSpecification(Expression<Func<T, bool>> Criteria)
            {
                this.Criteria = Criteria;
            }
        public BaseSpecification() { }
        public void ApplyPagniation(int skip, int take)
        {
            this.Pagniation_Enabled=true;
            this.Skip = skip;
            this.Take = take;
        }
    }
}
