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
            (!productparams.brandId.HasValue || x.ProductBrandId==productparams.brandId) &&
            (!productparams.typeId.HasValue || x.ProductTypeId==productparams.typeId)
        )
        {
            
        }
    }
}