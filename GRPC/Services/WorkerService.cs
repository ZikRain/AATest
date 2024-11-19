using Grpc.Core;
using Shared;
using GRPC.DB.RepositoryInterfaces;
using Shared.Mapping;
using Action = Shared.Action;

namespace GRPC.Services
{
    public class WorkerService : WorkerIntegration.WorkerIntegrationBase
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IWorkerRepository _rep;
        public WorkerService(ILogger<WorkerService> logger,IWorkerRepository repository)
        {
            _logger = logger;
            _rep = repository;

        }

        public async override Task GetWorkerStream(EmptyMessage request, IServerStreamWriter<WorkerMessage> responseStream, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("START: GetWorkerStream");
                var workers = await _rep.GetAllAsync();

                foreach (var worker in workers)
                {
                    await responseStream.WriteAsync(worker.ToMessage());
                }
            
            }catch(RpcException ex)
            {
                _logger.LogError($"ERROR: GetWorkerStream; Code:{ex.StatusCode}; Message:{ex.Message}");
                throw ex;

            }
            finally
            {
                _logger.LogInformation("END: GetWorkerStream");
            }
        }

        public async override Task<WorkerAction> ChangeWorker(WorkerAction action, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation($"START: ChangeWorker {action.ActionType}");

                switch (action.ActionType)
                {
                    case Action.Delete:
                        {
                            await _rep.DeleteAsync(action.Worker.ToEntity());
                            break;
                        }
                    case Action.Update:
                        {
                            await _rep.UpdateAsync(action.Worker.ToEntity());
                            break;
                        }
                    case Action.Create:
                        {
                            action.Worker = (await _rep.CreateAsync(action.Worker.ToEntity())).ToMessage();
                            break;
                        }
                }

                return action;
            }
            catch (RpcException ex)
            {
                _logger.LogError($"ERROR: ChangeWorker; Code:{ex.StatusCode}; Message:{ex.Message}");
                throw ex;

            }
            finally
            {
                _logger.LogInformation("END: ChangeWorker");
            }

        }

        public async override Task<EmptyMessage> FillWorkerStream(IAsyncStreamReader<WorkerMessage> requestStream, ServerCallContext context)
        {
            try
            {
                _logger.LogInformation("START: FillWorkerStream");

                await foreach (WorkerMessage worker in requestStream.ReadAllAsync())
                {
                    await _rep.CreateAsync(worker.ToEntity());
                }

                return new EmptyMessage();

            }
            catch (RpcException ex)
            {
                _logger.LogError($"ERROR: FillWorkerStream; Code:{ex.StatusCode}; Message:{ex.Message}");
                throw ex;

            }
            finally
            {
                _logger.LogInformation("END: FillWorkerStream");
            }
        }
    }
}
