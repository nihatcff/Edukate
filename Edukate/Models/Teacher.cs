using Edukate.Models.Common;

namespace Edukate.Models
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public Course Course = null!;
    }
}
