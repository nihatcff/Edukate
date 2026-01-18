using Edukate.Contexts;
using Edukate.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Edukate.Controllers
{
    public class CourseController(AppDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Select(x=>new CourseGetVM()
            {
                Id = x.Id,
                Title = x.Title,
                Rating = x.Rating,
                ImagePath = x.ImagePath
            }).ToListAsync();

            return View(courses);
        }
    }
}
