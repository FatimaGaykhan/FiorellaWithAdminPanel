using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.Models;
using Fiorella.Services;
using Fiorella.Services.Interfaces;
using Fiorella.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;


namespace Fiorella.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        } 

        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existBlog = await _blogService.ExistAsync(blog.Title);
            if (existBlog)
            {
                ModelState.AddModelError("Title", "This blog name already exist");

                return View();
            }
            await _blogService.CreateAsync(new Blog { Title = blog.Title, Description = blog.Description, Image = "blog-feature-img-1.jpg" });
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            return View(await _blogService.DetailAsync((int)id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var blog = await _blogService.GetByIdAsync((int)id);
            if (blog is null) return NotFound();
            await _blogService.DeleteAsync(blog);
            return RedirectToAction(nameof(Index));
        }
    }
}

