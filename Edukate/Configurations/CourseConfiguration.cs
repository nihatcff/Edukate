using Edukate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edukate.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(512);
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.Rating).IsRequired();

            builder.ToTable(x =>
            {
                x.HasCheckConstraint("CK_Course_Rating","[Rating] between 0 and 5");
            });
            builder.HasMany(x => x.Teachers).WithOne(x => x.Course).HasForeignKey(x => x.CourseId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
