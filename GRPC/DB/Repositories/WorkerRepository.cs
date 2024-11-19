using Grpc.Core;
using GRPC.DB.Contexts;
using GRPC.DB.RepositoryInterfaces;
using Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace GRPC.DB.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly TestDbContext _testDbContext;

        public WorkerRepository(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            try
            {
                return await _testDbContext.Workers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Aborted,$"При получении данных произошла ошибка\n{ex.Message}"));
            }
        }

        public async Task<Worker> CreateAsync(Worker worker)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(worker.FirstName)) throw new Exception($"Имя не должно быть пустым");
                if(string.IsNullOrWhiteSpace(worker.LastName)) throw new Exception($"Фамилия не должна быть пустой");
                if(string.IsNullOrWhiteSpace(worker.MiddleName)) throw new Exception($"Отчество не должно быть пустым");


                var a = await _testDbContext.Workers.AddAsync(worker);
                await _testDbContext.SaveChangesAsync();
                return a.Entity;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Aborted, $"При добавлении данных произошла ошибка\n{ ex.Message }"));
            }
        }

        public async Task<Worker> UpdateAsync(Worker worker)
        {
            try
            {
                var w = await _testDbContext.Workers.FirstOrDefaultAsync(x => x.ID == worker.ID);
                if (w == null) throw new Exception("Не найдена запись");

                w.FirstName = worker.FirstName;
                w.LastName = worker.LastName;
                w.MiddleName = worker.MiddleName;
                w.Birthday = worker.Birthday;
                w.Sex = worker.Sex;
                w.HaveChildren = worker.HaveChildren;

                await _testDbContext.SaveChangesAsync();
                return worker;

            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"При обновлении данных произошла ошибка\n{ex.Message}"));
            }
        }

        public async Task DeleteAsync(Worker worker)
        {
            try
            {
                var w = await _testDbContext.Workers.FirstOrDefaultAsync(x => x.ID == worker.ID);
                if (w == null) throw new Exception("Не найдена запись");

                _testDbContext.Workers.Remove(w);
                await _testDbContext.SaveChangesAsync();
            } 
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"При удалении данных произошла ошибка\n{ex.Message}"));
            }
        }
    }
}
