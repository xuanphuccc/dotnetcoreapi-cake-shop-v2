using AutoMapper;
using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.application
{
    public class ProductService : BaseService<Product, ProductDto, ProductRequestDto, ProductRequestDto>, IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository, IMapper mapper)
            : base(productRepository, mapper)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Tìm kiếm, phân trang, sắp xếp
        /// </summary>
        /// <param name="category">ID của danh mục</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="page">Trang hiện tại</param>
        /// <param name="sort">Kiểu sắp xếp</param>
        /// <param name="search">Tìm kiếm</param>
        /// <returns></returns>
        public async Task<ResponseDto> FilterAsync(
            int? category = null,
            int? pageSize = null,
            int? page = null,
            string? sort = null,
            string? search = null)
        {
            var allProductsQuery = _productRepository.GetAllEntities();

            // Lọc theo danh mục
            if (category.HasValue)
            {
                allProductsQuery = allProductsQuery.Where(p => p.CategoryId == category.Value);
            }

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                allProductsQuery = allProductsQuery.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            // Sắp xếp
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "nameAsc":
                        allProductsQuery = allProductsQuery.OrderBy(p => p.Name);
                        break;
                    case "nameDesc":
                        allProductsQuery = allProductsQuery.OrderByDescending(p => p.Name);
                        break;
                    case "creationTimeAsc":
                        allProductsQuery = allProductsQuery.OrderBy(p => p.CreateAt);
                        break;
                    case "creationTimeDesc":
                        allProductsQuery = allProductsQuery.OrderByDescending(p => p.CreateAt);
                        break;
                    default:
                        break;
                }
            }

            // Phân trang
            int totalPage = 1;
            if (pageSize.HasValue && page.HasValue)
            {
                int totalItems = await allProductsQuery.CountAsync();
                totalPage = (int)Math.Ceiling((double)totalItems / pageSize.Value);

                allProductsQuery = allProductsQuery.Skip((page.Value - 1) * pageSize.Value)
                                                   .Take(pageSize.Value);
            }

            var allProducts = await allProductsQuery.ToListAsync();
            var allProductResponseDtos = _mapper.Map<List<ProductDto>>(allProducts);

            // Lấy số đơn hàng đã bán của sản phẩm
            foreach (var productResponse in allProductResponseDtos)
            {
                productResponse.HasOrders = await _productRepository.HasOrders(productResponse.ProductId);
            }

            return new ResponseDto()
            {
                Data = allProductResponseDtos,
                TotalPage = totalPage
            };
        }

        /// <summary>
        /// Map DTO sang entity để thêm bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<Product> MapCreateAsync(ProductRequestDto entityCreateDto)
        {
            var newProduct = _mapper.Map<Product>(entityCreateDto);
            newProduct.CreateAt = DateTime.UtcNow;

            return await Task.FromResult(newProduct);
        }

        /// <summary>
        /// Map DTO sang entity để cập nhật bản ghi
        /// </summary>
        /// <param name="entityUpdateDto">Đối tượng cần map</param>
        /// <returns></returns>
        protected override async Task<Product> MapUpdateAsync(int entityId, ProductRequestDto entityUpdateDto)
        {
            var existProduct = await _productRepository.GetEntityByIdAsync(entityId);

            _mapper.Map(entityUpdateDto, existProduct);

            return existProduct;
        }

        /// <summary>
        /// Thực hiện hành động trước khi xoá
        /// </summary>
        /// <param name="deletedEntity">Đối tượng đã xoá</param>
        /// <returns></returns>
        protected override async Task BeforeDeleteAsync(Product product)
        {
            // Check sản phẩm đã được đặt hàng chưa
            var hasOrders = await _productRepository.HasOrders(product.ProductId);
            if (hasOrders > 0)
            {
                throw new ConstraintException("Không thể xoá sản phẩm đã được đặt hàng");
            }
        }
    }
}
