using Edukate.Contexts;
using Edukate.Models;
using Edukate.ViewModels.TeacherViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Edukate.Areas.Admin.Controllers;
[Area("Admin")]
public class TeacherController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var teachers = await _context.Teachers.Select(x=>new TeacherGetVM()
        {
            Id = x.Id,
            Fullname = x.Fullname,
            CourseName = x.Course.Title
        }).ToListAsync();

        return View(teachers);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await _sendCoursesWithViewBag();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TeacherCreateVM vm)
    {
        await _sendCoursesWithViewBag();
        if (!ModelState.IsValid)
            return NotFound();

        var isExistCourse = await _context.Courses.AnyAsync(x => x.Id == vm.CourseId);
        if (!isExistCourse)
        {
            ModelState.AddModelError("CourseId", "This course doesn't exist");
            return View(vm);
        }

        Teacher teacher = new()
        {
           CourseId = vm.CourseId,
           Fullname = vm.Fullname
        };

        await _context.Teachers.AddAsync(teacher);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null)
            return NotFound();

        _context.Teachers.Remove(teacher);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));        
    }


    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        await _sendCoursesWithViewBag();
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null)
            return NotFound();

        TeacherUpdateVM vm = new()
        {
            Fullname = teacher.Fullname,
            CourseId =teacher.CourseId
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TeacherUpdateVM vm)
    {
        await _sendCoursesWithViewBag();
        if (!ModelState.IsValid)
            return View(vm);

        var isExistCourse = await _context.Courses.AnyAsync(x => x.Id == vm.CourseId);
        if (!isExistCourse)
        {
            ModelState.AddModelError("CourseId", "This course doesn't exist");
            return View(vm);
        }

        var existTeacher = await _context.Teachers.FindAsync(vm.Id);
        if (existTeacher is null)
            return BadRequest();
        existTeacher.Fullname = vm.Fullname;
        existTeacher.CourseId = vm.CourseId;

        _context.Teachers.Update(existTeacher);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }


    private async Task _sendCoursesWithViewBag()
    {
        var courses = await _context.Courses.ToListAsync();
        ViewBag.Courses = courses;
    }
}
