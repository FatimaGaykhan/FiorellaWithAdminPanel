using System;
using Fiorella.Models;
using Fiorella.ViewModels.Blog;

namespace Fiorella.Services.Interfaces
{
	public interface IBlogService
	{
		Task<IEnumerable<BlogVM>> GetAllAsync(int? take=null);
		Task<Blog> GetByIdAsync(int id );
        Task CreateAsync(Blog blog);
        Task<bool> ExistAsync(string name);
        Task<Blog> DetailAsync(int id);
        Task DeleteAsync(Blog blog);



    }
}

