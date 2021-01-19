using MergedProductsCatalog.Services;
using System.Linq;
using Xunit;

namespace MergedProductsCatalog.Tests
{
    public class ProductItemsBuilderServiceTest
    {
        public ProductItemsBuilderService productItemsBuilderService;

        public ProductItemsBuilderServiceTest()
        {
            productItemsBuilderService = new ProductItemsBuilderService();
        }

        [Fact]
        public void WhenCatalogsSuppliersAndBarcodesAreSupplied_CountShouldReturnFiftyTwo()
        {
            var result = productItemsBuilderService.Construct(MockData.catalogsA, MockData.suppliersA, MockData.productsA, "A");

            var count = result.Count();

            Assert.Equal(52, count);
        }

        [Fact]
        public void WhenCatalogsSuppliersAndBarcodesAreSupplied_CountForSourceAShouldReturnFiftyTwo()
        {
            var result = productItemsBuilderService.Construct(MockData.catalogsA, MockData.suppliersA, MockData.productsA, "A");

            var count = result.Count(o => o.Source == "A");

            Assert.Equal(52, count);
        }
    }
}
