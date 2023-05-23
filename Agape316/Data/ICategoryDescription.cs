using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agape316.Models;

namespace Agape316.Data
{
    public interface ICategoryDescription
    {
        CategoryDescription GetById(string id);
        IEnumerable<Category> GetAll();
        Task Create(CategoryDescription category);
        Task Delete(int Id);       
        Task UpdateCategoryDescription(int Id, string newDescription);
    }
}
