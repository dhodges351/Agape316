using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface IEventSlot
    {
        EventSlot GetById(string id);
        IEnumerable<EventSlot> GetAll();
        Task Create(EventSlot eventSlot);
        Task Delete(int Id);
        Task UpdateEventSlotName(int Id, string newName);
    }
}
