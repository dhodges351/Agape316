using Microsoft.EntityFrameworkCore;
using Agape316.Data;
using Microsoft.Extensions.Hosting;

namespace Agape316.Services
{
    public class AgapeDocumentService : IAgapeDocument
    {
        private readonly ApplicationDbContext _context;

        public AgapeDocumentService(ApplicationDbContext context)
        {
            _context = context;
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
            return _context.AgapeDocument;
        }

        public AgapeDocument GetById(int id)
        {
            var agapeDocument = _context.AgapeDocument.Where(x => x.Id == id)                
                .FirstOrDefault();

            return agapeDocument;
        }

        public void UpdateAgapeDocument(AgapeDocument agapeDocument)
        {
            _context.Update(agapeDocument);
            _context.SaveChanges();
        }
    }
}