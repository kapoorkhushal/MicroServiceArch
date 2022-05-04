using System;

namespace ServiceProviders.Domain.Event
{
    public class BaseEvent
    {
        public BaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public BaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        //Correlation id
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }
    }
}
