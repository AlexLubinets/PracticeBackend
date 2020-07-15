using System.Linq;
using System.Threading.Tasks;
using ProductApp.DAL.Entities;

namespace ProductApp.DAL.Interfaces
{
    public interface IOperationRepository
    {
        IQueryable<Operation> GetOperations(int id);
        Task<Operation> Create(Operation item);
    }
}
