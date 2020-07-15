using System.Threading.Tasks;
using ProductApp.BLL.Models;

namespace ProductApp.BLL.Interfaces
{
    public interface IProductService
    {
        PagedList<ProductDTO> GetProductsSegment(ProductPagingParams pagingParams);
        Task<ProductDTO> GetProductById(int id);
        Task<ProductDTO> AddProduct(ProductDTO model);
        Task DeleteProductById(int id);
        Task<ProductDTO> ChangeProduct(ProductDTO modelChanges);
    }
}
