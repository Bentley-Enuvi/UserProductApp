using UserProduct.Core.DTOs;

namespace UserProduct.Core.Abstractions
{
    public interface IProductService
    {
        Task<Result> CreateProduct(CreateProductDto createProductDto);

        Task<Result<SingleProductDto>> GetProductById(string productId);

        Task<PaginatorDto<IEnumerable<GetAllProductDto>>> GetAllProducts(PaginationFilter paginationFilter);

        Task<PaginatorDto<IEnumerable<GetAllProductDto>>> SearchProducts(string searchTerm, PaginationFilter paginationFilter);

        Task<Result> UpdateProduct(string productId, UpdateProductDto updateProductDto);

        Task<Result> DeleteProduct(string productId);
    }
}
