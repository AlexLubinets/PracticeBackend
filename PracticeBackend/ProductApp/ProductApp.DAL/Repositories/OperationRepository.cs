using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.DAL.EF;
using ProductApp.DAL.Entities;
using ProductApp.DAL.Interfaces;

namespace ProductApp.DAL.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ApplicationContext dbContext;

        public OperationRepository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Operation> GetOperations(int id)
        {
            var operations = dbContext.Operations.Where(p => p.ProductId == id).Include(x => x.AppUser);
            return operations;
        }

        public async Task<Operation> Create(Operation model)
        {
            await dbContext.Operations.AddAsync(model);
            return model;
        }
    }
}
