using Microsoft.EntityFrameworkCore;
using Agape316.Data;
using Microsoft.Extensions.Hosting;

namespace Agape316.Services
{
    public class AgapeDocumentService : IAgapeDocument
    {
        private readonly ApplicationDbContext _context;
        private readonly IAgapeDocument _agapeDocumentService;

        public AgapeDocumentService(ApplicationDbContext context, IAgapeDocument agapeDocumentService)
        {
            _context = context;
            _agapeDocumentService = agapeDocumentService;   
        }

        public async Task Create(AgapeDocument agapeDocument)
        {
            _context.Add(agapeDocument);
            await _context.SaveChangesAsync();
        }        

        public async Task Delete(int id)
        {
            var agapeDocument = GetById(id);
            if (agapeDocument != null)
            {
                _context.Remove(agapeDocument);
                await _context.SaveChangesAsync();
            }            
        }

        public IEnumerable<AgapeDocument> GetAll()
        {
            return _context.AgapeDocuments;
        }

        public AgapeDocument GetById(int id)
        {
            var agapeEvent = _context.AgapeDocuments.Where(x => x.Id == id)                
                .FirstOrDefault();

            return agapeEvent;
        }

        public void UpdateAgapeDocument(AgapeDocument agapeDocument)
        {
            _context.Update(agapeDocument);
            _context.SaveChanges();
        }

        public IEnumerable<AgapeDocument> GetFilteredAgapeDocuments(string searchQuery)
        {
            throw new NotImplementedException();
        }
    }
}