using Grpc.Net.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Grpc.Net.Client.Web;
using Shared.Entities;
using Shared;
using Shared.Mapping;
using System.Linq;

namespace WPF.Services
{
    public static class WorkerServiceClient
    {
        private const string _server = "https://localhost:44301";

        public async static Task<IEnumerable<Worker>> GetWorkers()
        {
            using (var channel = GrpcChannel.ForAddress(_server, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            }))
            {

                var client = new WorkerIntegration.WorkerIntegrationClient(channel);
                var response = client.GetWorkersList(new EmptyMessage());

                var workers = new List<Worker>(response.Workers.Select(x=>x.ToEntity()));

                return workers;
            }
        }

        public async static Task<Worker> ChangeWorker(Worker worker, Action action)
        {
            using (var channel = GrpcChannel.ForAddress(_server, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            }))
            {
                var client = new WorkerIntegration.WorkerIntegrationClient(channel);
                var response = await client.ChangeWorkerAsync(new WorkerAction() { Worker = worker.ToMessage(), ActionType = action });
                return response.Worker.ToEntity();
            }
        }
    }
}
