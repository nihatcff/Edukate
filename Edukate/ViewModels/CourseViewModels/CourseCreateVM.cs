using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.CourseViewModels
{
    public class CourseCreateVM
    {
        [Required,MaxLength(512),MinLength(3)]
        public string Title { get; set; } = string.Empty;
        [Required,Range(0,5)]
        public double Rating { get; set; }
        [Required]
        public IFormFile Image { get; set; } = null!; 
    }
}
