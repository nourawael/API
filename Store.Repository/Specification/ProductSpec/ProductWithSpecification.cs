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
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(specs.Sort))
            {
                switch (specs.Sort) 
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithSpecification(int? id) : base(product => product.Id ==id )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Type);
            
        }
    }
}
