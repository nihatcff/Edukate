using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.TeacherViewModels
{
    public class TeacherCreateVM
    {
        [Required,MaxLength(512),MinLength(3)]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        public int CourseId { get; set; }
    }
}
