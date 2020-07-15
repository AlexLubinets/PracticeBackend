using System.Linq;
using System.Threading.Tasks;
using ProductApp.DAL.Entities;

namespace ProductApp.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetItem(int id);
        IQueryable<Product> GetAllItems();
        Task<Product> Create(Product item);
        Product Update(Product item);
        Task Delete(int id);
    }
}
