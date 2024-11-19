using Grpc.Net.Client;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shared.Mapping;
using Shared;
using Shared.Entities;
using System.Net.Http;
using Grpc.Net.Client.Web;

namespace WPF.Services
{
    public static class WorkerServiceClient
    {
        private const string _server = "https://localhost:44301/";

        public async static Task<IEnumerable<Worker>> GetWorkers()
        {
            using (var channel = GrpcChannel.ForAddress(_server, new GrpcChannelOptions
            {
                HttpHandler = new GrpcWebHandler(new HttpClientHandler())
            }))
            {

                var client = new WorkerIntegration.WorkerIntegrationClient(channel);
                var serverData = client.GetWorkerStream(new EmptyMessage());
                var responseStream = serverData.ResponseStream;

                var workers = new List<Worker>();

                while (await responseStream.MoveNext(new CancellationToken()))
                {
                    workers.Add(responseStream.Current.ToEntity());
                }

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
