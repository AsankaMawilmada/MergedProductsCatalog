using MergedProductsCatalog.Models;
using System.Collections.Generic;
using System.Linq;

namespace MergedProductsCatalog.Services
{
    public class ProductItemsBuilderService
    {
        public List<ProductItem> Construct(List<Catalog> catalogs, List<Supplier> suppliers, List<Product> products, string source)
        {
            var items = new List<ProductItem>();
            foreach (var barcode in products)
            {
                var supplier = suppliers.FirstOrDefault(o => o.ID == barcode.SupplierID);
                var catalog = catalogs.FirstOrDefault(o => o.SKU == barcode.SKU);

                items.Add(new ProductItem
                {
                    SupplierID = barcode.SupplierID,
                    SupplierName = supplier != null ? supplier.Name : "",
                    SKU = barcode.SKU,
                    Barcode = barcode.Barcode,
                    Description = catalog != null ? catalog.Description : "",
                    Source = source
                });
            }

            return items;
        }
    }


}
