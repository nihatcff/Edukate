using Edukate.Models;
using Microsoft.EntityFrameworkCore;

namespace Edukate.ViewModels.CourseViewModels
{
    public class CourseGetVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public ICollection<Teacher> Teachers = [];
    }
}
