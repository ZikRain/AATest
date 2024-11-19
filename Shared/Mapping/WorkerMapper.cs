using Shared.Entities;
using Shared.Helpers;

namespace Shared.Mapping
{
    public static class WorkerMapper
    {
        public static WorkerMessage ToMessage(this Worker worker)
        {
            return new WorkerMessage()
            {
                ID = worker.ID,
                LastName = worker.LastName,
                FirstName = worker.FirstName,
                MiddleName = worker.MiddleName,
                Birthday = worker.Birthday.ToUnix(),
                Sex = worker.Sex,
                HaveChildren = worker.HaveChildren
            };
        }

        public static Worker ToEntity(this WorkerMessage message)
        {
            return new Worker()
            {
                ID = message.ID,
                LastName = message.LastName,
                FirstName = message.FirstName,
                MiddleName = message.MiddleName,
                Birthday = message.Birthday.ToDateTimeUnix(),
                Sex = message.Sex,
                HaveChildren = message.HaveChildren
            };
        }
    }
}
