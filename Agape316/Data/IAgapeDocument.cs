using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface IAgapeDocument
    {
        AgapeDocument GetById(int id);
        IEnumerable<AgapeDocument> GetAll();
        Task Create(AgapeDocument agapeDocument);
        Task Delete(int id);
        void UpdateAgapeDocument(AgapeDocument agapeDocument);
        IEnumerable<AgapeDocument> GetFilteredAgapeDocuments(string searchQuery);
    }
}
