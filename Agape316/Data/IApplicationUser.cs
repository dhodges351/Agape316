using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        ApplicationUser GetByUsername(string userName);
        IEnumerable<ApplicationUser> GetAll();
        Task SetProfileImage(string id, Uri uri);        
    }
}
