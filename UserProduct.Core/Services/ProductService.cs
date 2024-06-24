using UserProduct.Core.Abstractions;
using UserProduct.Core.DTOs;
using UserProduct.Core.Utilities;
using UserProduct.Domain.Entities;

namespace UserProduct.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result> CreateProduct(CreateProductDto createProductDto)
        {
            var product = new Product
            {
                ProductName = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                CreatedAt = DateTime.UtcNow
            };

            await _productRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }


        public async Task<Result<SingleProductDto>> GetProductById(string productId)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null)
                return new Error[] { new("Product.Error", "Product not found") };
            else
                return Result<SingleProductDto>.Success(new SingleProductDto { /* map fields */ });

        }


        public async Task<Result> UpdateProduct(string productId, UpdateProductDto updateProductDto)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null)

                return new Error[] { new("Product.Error", "Product not found") };

            product.ProductName = updateProductDto.ProductName;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }


        public async Task<PaginatorDto<IEnumerable<GetAllProductDto>>> SearchProducts(string searchTerm, PaginationFilter paginationFilter)
        {
            var products = await _productRepository.GetAll()
                .Where(c => c.ProductName.Contains(searchTerm) || c.Description.Contains(searchTerm))
                .OrderByDescending(c => c.CreatedAt)
                .Paginate(paginationFilter);

            return new PaginatorDto<IEnumerable<GetAllProductDto>>
            {
                PageItems = products.PageItems.Select(p => new GetAllProductDto
                {
                    // map fields from product entity to DTO
                    Id = p.Id,
                    Name = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CreatedAt = p.CreatedAt.ToString(),
                }),
                PageSize = paginationFilter.PageSize,
                CurrentPage = paginationFilter.PageNumber,
                NumberOfPages = products.NumberOfPages
            };
        }


        public async Task<PaginatorDto<IEnumerable<GetAllProductDto>>> GetAllProducts(PaginationFilter paginationFilter)
        {
            var products = await _productRepository.GetAll()
                .OrderByDescending(c => c.CreatedAt)
                .Paginate(paginationFilter);

            return new PaginatorDto<IEnumerable<GetAllProductDto>>
            {
                PageItems = products.PageItems.Select(p => new GetAllProductDto
                {

                    Id = p.Id,
                    Name = p.ProductName,
                    Description = p.Description,
                    Price = p.Price,
                    CreatedAt = p.CreatedAt.ToString()
                }),
                PageSize = paginationFilter.PageSize,
                CurrentPage = paginationFilter.PageNumber,
                NumberOfPages = products.NumberOfPages
            };
        }

        public async Task<Result> DeleteProduct(string productId)
        {
            var product = await _productRepository.FindById(productId);
            if (product is null)
                return new Error[] { new("Product.Error", "Product not found") };

            _productRepository.Remove(product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
