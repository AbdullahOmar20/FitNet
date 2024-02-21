using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class BaseSpecificcation<T> : ISpecification<T>
    {
        public BaseSpecificcation()
        {
            
        }
        public BaseSpecificcation(Expression<Func<T,bool>> criteria)
        {
            Criteria=criteria;
        }

        public Expression<Func<T, bool>> Criteria{get;}

        public List<Expression<Func<T, object>>> Includes {get;} = 
            new List<Expression<Func<T, object>>>();
        protected void AddInclude (Expression<Func<T,object>> includeExpression){
            Includes.Add(includeExpression);
        }
    }
}