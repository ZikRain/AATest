using System;
using Shared;
using Shared.Helpers;

namespace Shared.Entities
{
    public class Worker
    {
        public long ID { get; set; }
        public string LastName {  get; set; }
        public string FirstName {  get; set; }
        public string MiddleName {  get; set; }
        public DateTime Birthday {  get; set; }
        public Sex Sex { get; set; }
        public bool HaveChildren {  get; set; }


  

        public Worker()
        {
            LastName = string.Empty;
            FirstName = string.Empty;
            MiddleName = string.Empty;
        }

        public Worker(WorkerMessage message)
        {
            LastName= message.LastName;
            FirstName= message.FirstName;
            MiddleName= message.MiddleName;
            Birthday = message.Birthday.ToDateTimeUnix();
            Sex = message.Sex;
            HaveChildren = message.HaveChildren;
        }

        
    }
}
