using UseCases.DataStorePluginInterfaces;
using UseCases.TransactionsUseCases;

namespace UseCases.ProductsUseCases
{
    public class SellProductUseCase : ISellProductUseCase
    {
        private readonly IProductsRepository productRepository;
        private readonly IRecordTransactionUseCase recordTransactionUseCase;

        public SellProductUseCase(
            IProductsRepository productsRepository,
            IRecordTransactionUseCase recordTransactionUseCase
        )
        {
            this.productRepository = productsRepository;
            this.recordTransactionUseCase = recordTransactionUseCase;
        }
        public void Execute(string cashierName, int productId, int qtyToSell)
        {
            var product = productRepository.GetProductById(productId);

            if (product == null)
                return;

            recordTransactionUseCase.Execute(cashierName, productId, qtyToSell);
            product.Quantity -= qtyToSell;
            productRepository.UpdateProduct(productId, product);

        }
    }
}