using Edukate.Contexts;
using Edukate.Helpers;
using Edukate.Models;
using Edukate.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Edukate.Areas.Admin.Controllers;
[Area("Admin")]
public class CourseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public CourseController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "assets", "img");
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.Select(x=> new CourseGetVM
        {
            Id = x.Id,
            Title = x.Title,
            Rating = x.Rating,
            ImagePath = x.ImagePath
        }).ToListAsync();

        return View(courses);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateVM vm) 
    {
        if (!ModelState.IsValid)
            return View(vm);

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("Image", "File size exceeds");
            return View(vm);
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("Image", "File must be Image");
            return View(vm);
        }

        string uniqueFileName = await vm.Image.UploadFileAsync(_folderPath);

        Course course = new()
        {
            Title = vm.Title,
            Rating = vm.Rating,
            ImagePath = uniqueFileName
        };

        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();


        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null)
            return NotFound();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        string deletedImagePath = Path.Combine(_folderPath, course.ImagePath);

        ExtensionMethods.DeleteFile(deletedImagePath);

        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course is null)
            return NotFound();

        CourseUpdateVM vm = new()
        {
            Id = course.Id,
            Title = course.Title,
            Rating = course.Rating
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CourseUpdateVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        if (!vm.Image?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("Image", "File size exceeds");
            return View(vm);
        }

        if (!vm.Image?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("Image", "File must be Image");
            return View(vm);
        }

        var existCourse = await _context.Courses.FindAsync(vm.Id);

        if (existCourse is null)
            return BadRequest();

        existCourse.Title = vm.Title;
        existCourse.Rating = vm.Rating;

        if(vm.Image is { })
        {
            var oldImagePath = Path.Combine(_folderPath, existCourse.ImagePath);
            var newImagePath = await vm.Image.UploadFileAsync(_folderPath);
            ExtensionMethods.DeleteFile(oldImagePath);
            existCourse.ImagePath = newImagePath;
        }

        _context.Courses.Update(existCourse);
        await _context.SaveChangesAsync();


        return RedirectToAction(nameof(Index));
    }

    



}
