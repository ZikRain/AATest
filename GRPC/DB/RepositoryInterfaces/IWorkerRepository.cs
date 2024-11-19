using Shared.Entities;

namespace GRPC.DB.RepositoryInterfaces
{
    public interface IWorkerRepository
    {
        Task<IEnumerable<Worker>> GetAllAsync();
        Task<Worker> CreateAsync(Worker worker);
        Task<Worker> UpdateAsync(Worker worker);
        Task DeleteAsync(Worker worker);
    }
}
