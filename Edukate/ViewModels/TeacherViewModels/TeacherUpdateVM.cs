using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.TeacherViewModels
{
    public class TeacherUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(256), MinLength(3)]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        public int CourseId { get; set; }
    }
}
