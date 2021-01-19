using MergedProductsCatalog.Models;
using MergedProductsCatalog.Services;

namespace MergedProductsCatalog
{
    class Program
    {
        const string folderPath = "C:\\Repos\\MergedProductsCatalog\\Files\\";
        static void Main(string[] args)
        {
  
            var productItemsBuilderService = new ProductItemsBuilderService();
            var catalogMergeService = new CatalogMergeService();

            var productItemsA = productItemsBuilderService.Construct(
                CSVReaderHelper.ReadItems<Catalog>($"{folderPath}catalogA.csv"),
                CSVReaderHelper.ReadItems<Supplier>($"{folderPath}suppliersA.csv"),
                CSVReaderHelper.ReadItems<Product>($"{folderPath}barcodesA.csv"),
                Source.A);

            var productItemsB = productItemsBuilderService.Construct(
                CSVReaderHelper.ReadItems<Catalog>($"{folderPath}catalogB.csv"),
                CSVReaderHelper.ReadItems<Supplier>($"{folderPath}suppliersB.csv"),
                CSVReaderHelper.ReadItems<Product>($"{folderPath}barcodesB.csv"),
                Source.B);

            var items = catalogMergeService.Merge(productItemsA, productItemsB);

            CSVExportHelper.ExportItems($"{folderPath}result_output.csv", items);
        }
    }
}
