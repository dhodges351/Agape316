using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface ICategory
    {
        Category GetById(string id);
        IEnumerable<Category> GetAll();
        Task Create(Category category);
        Task Delete(int categoryId);
        Task UpdateCategoryName(int categoryId, string newName);
    }
}
