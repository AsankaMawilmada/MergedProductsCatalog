using MergedProductsCatalog.Models;
using MergedProductsCatalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MergedProductsCatalog.Tests
{
    public class CatalogMergeServiceTest
    {
        public CatalogMergeService catalogMergeService;
        public ProductItemsBuilderService productItemsBuilderService;

        public List<ProductItem>  productItemsA;
        public List<ProductItem> productItemsB;
        public CatalogMergeServiceTest()
        {
            productItemsBuilderService = new ProductItemsBuilderService();
            catalogMergeService = new CatalogMergeService();

            productItemsA = productItemsBuilderService.Construct(MockData.catalogsA, MockData.suppliersA, MockData.productsA, "A");
            productItemsB = productItemsBuilderService.Construct(MockData.catalogsB, MockData.suppliersB, MockData.productsB, "B");
        }

        [Fact]
        public void WhenCatalogsSuppliersAndBarcodesAreSupplied_ForBothCompanyAandCompanyB_CountShouldReturnTen()
        {
            var result = catalogMergeService.Merge(productItemsA, productItemsB);

            var count = result.Count();

            Assert.Equal(10, count);
        }
             
        [Theory]
        [InlineData("Walkers Special Old Whiskey",  1)]
        [InlineData("Bread - Raisin",  2)]
        [InlineData("Tea - Decaf 1 Cup",  2)]
        [InlineData("Cheese - Grana Padano", 2)]
        [InlineData("Carbonated Water - Lemon Lime",  2)]
        [InlineData("Walkers Special Old Whiskey test", 1)]
        public void WhenCatalogsSuppliersAndBarcodesAreSupplied_ForBothCompanyAandCompanyB_ShouldReturnExpectedCountForEachProduct(string description, int expectedCount)
        {
            var result = catalogMergeService.Merge(productItemsA, productItemsB);

            var count = result.Count(o => o.Description == description);

            Assert.Equal(expectedCount, count);
        }

        [Theory]
        [InlineData("Walkers Special Old Whiskey", "A", 1)]
        [InlineData("Bread - Raisin", "A", 1)]
        [InlineData("Bread - Raisin", "B", 1)]
        [InlineData("Tea - Decaf 1 Cup", "A", 1)]
        [InlineData("Tea - Decaf 1 Cup", "B", 1)]
        [InlineData("Cheese - Grana Padano", "A", 1)]
        [InlineData("Cheese - Grana Padano", "B", 1)]
        [InlineData("Carbonated Water - Lemon Lime", "A", 1)]
        [InlineData("Carbonated Water - Lemon Lime", "B", 1)]
        [InlineData("Walkers Special Old Whiskey test", "B", 1)]
        public void WhenCatalogsSuppliersAndBarcodesAreSupplied_ForBothCompanyAandCompanyB_ShouldReturnExpectedCountForEachProductandSource(string description, string source, int expectedCount)
        {
            var result = catalogMergeService.Merge(productItemsA, productItemsB);

            var count = result.Count(o => o.Description == description && o.Source == source);

            Assert.Equal(expectedCount, count);
        }


        [Fact]
        public void WhenNewProductAddedToCatelogA_CountShouldReturnEleven()
        {
            var duplicateProductItemsA = productItemsA;

            duplicateProductItemsA.Add(new ProductItem { Barcode = "AAAAA", Description = "New Catalog A Product", SKU = "PAAAAA", SupplierID = "00001", SupplierName = "Twitterbridge", Source = "A" });

            var result = catalogMergeService.Merge(duplicateProductItemsA, productItemsB);

            var count = result.Count();

            Assert.Equal(11, count);
        }

        [Fact]
        public void WhenProductRemovedFromCatelogA_CountShouldReturnNine()
        {
            var duplicateProductItemsA  = productItemsA.Where(o => o.SKU != "647-vyk-317").ToList();           

            var result = catalogMergeService.Merge(duplicateProductItemsA, productItemsB);

            var count = result.Count();

            Assert.Equal(9, count);
        }

        [Fact]
        public void WhenCatelogBGetBarcodesForNewSupplier_CountShouldReturnTen()
        {
            var duplicateProductItemsB = productItemsB;

            duplicateProductItemsB.Add(new ProductItem { Barcode = "z2783613083817", Description = "Walkers Special Old Whiskey test", SKU = "999-vyk-317", SupplierID = "00006", SupplierName = "ABC Company", Source = "B" });
            duplicateProductItemsB.Add(new ProductItem { Barcode = "z2783613083818", Description = "Walkers Special Old Whiskey test", SKU = "999-vyk-317", SupplierID = "00006", SupplierName = "ABC Company", Source = "B" });

            var result = catalogMergeService.Merge(productItemsA, duplicateProductItemsB);

            var count = result.Count();

            Assert.Equal(10, count);
        }
    }
}
