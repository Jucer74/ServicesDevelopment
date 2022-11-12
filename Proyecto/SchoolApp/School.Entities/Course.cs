namespace School.Entities
{
   public class Course
   {
      public Course()
      {
         CoursesStudents = new HashSet<CoursesStudent>();
      }

      public int Id { get; set; }
      public string Description { get; set; } = null!;
      public byte HoursByWeek { get; set; }
      public byte Grade { get; set; }

      public virtual ICollection<CoursesStudent> CoursesStudents { get; set; }
   }
}