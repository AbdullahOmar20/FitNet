using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductsWithFilterForCountSpecification(ProductSpecParams productparams):
         base(x=>
            (string.IsNullOrEmpty(productparams.Search) || (x.Name.ToLower().Contains(productparams.Search))) &&
            (productparams.brands == null || productparams.brands.Count <= 0 || productparams.brands.Contains(x.ProductBrandId)) &&
            (productparams.types == null || productparams.types.Count <= 0 || productparams.types.Contains(x.ProductTypeId))
        )
        {
            
        }
    }
}