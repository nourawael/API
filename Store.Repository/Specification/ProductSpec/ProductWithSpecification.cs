using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification.ProductSpec
{
    public class ProductWithSpecification: BaseSpecification<Product>
    {
        public ProductWithSpecification(ProductSpecification specs)
            : base(product => (!specs.BrandId.HasValue || product.BrandId==specs.BrandId.Value) &&
                              (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
            )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
        }
    }
}
