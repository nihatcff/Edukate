using Edukate.Models.Common;

namespace Edukate.Models
{
    public class Course : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public ICollection<Teacher> Teachers { get; set; } = [];
    }
}
