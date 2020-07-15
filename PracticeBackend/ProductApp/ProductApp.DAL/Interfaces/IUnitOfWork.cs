using System;
using System.Threading.Tasks;

namespace ProductApp.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IOperationRepository OperationRepository { get; }
        Task Save();
    }
}
