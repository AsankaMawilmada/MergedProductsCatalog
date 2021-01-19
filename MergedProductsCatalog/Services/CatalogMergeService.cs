using MergedProductsCatalog.Models;
using System.Collections.Generic;
using System.Linq;

namespace MergedProductsCatalog.Services
{
    public class CatalogMergeService
    {
        public List<MergedCatalog> Merge(List<ProductItem> productItemsA, List<ProductItem> productItemsB)
        {
            var productItems = new List<ProductItem>()
                                    .Concat(productItemsA)
                                    .Concat(productItemsB);         

           return productItems.GroupBy(g => new { g.Description, g.SKU, g.Source })
                .Select(o => new MergedCatalog
                {
                    SKU = o.Key.SKU,
                    Description = o.Key.Description,
                    Source = o.Key.Source
                })
                .ToList();
        }
    }
}
