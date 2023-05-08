namespace Agape316.Data
{
    public interface IEvent
    {
        Event GetById(int id);
        IEnumerable<Event> GetAll();
        Task Create(Event eventObj);
        Task Delete(int Id);
        Task UpdateEventTitle(int eventId, string newTitle);
        Task UpdateEventDescription(int eventId, string newDescription);
        IEnumerable<ApplicationUser> GetActiveUsers(int id);        
    }
}
