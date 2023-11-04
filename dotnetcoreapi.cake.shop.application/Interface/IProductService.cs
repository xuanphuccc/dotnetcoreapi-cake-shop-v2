
using dotnetcoreapi.cake.shop.domain;

namespace dotnetcoreapi.cake.shop.application
{
    public interface IProductService : IBaseService<ProductDto, ProductRequestDto, ProductRequestDto>
    {

        /// <summary>
        /// Tìm kiếm, phân trang, sắp xếp
        /// </summary>
        /// <param name="category">ID của danh mục</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="page">Trang hiện tại</param>
        /// <param name="sort">Kiểu sắp xếp</param>
        /// <param name="search">Tìm kiếm</param>
        /// <returns></returns>
        Task<ResponseDto> FilterAsync(
            int? category = null,
            int? pageSize = null,
            int? page = null,
            string? sort = null,
            string? search = null);
    }
}
