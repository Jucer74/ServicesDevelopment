namespace School.Entities
{
   public class CoursesStudent
   {
      public int CourseId { get; set; }
      public int StudentId { get; set; }
      public byte Grade { get; set; }

      public virtual Course Course { get; set; } = null!;
      public virtual Student Student { get; set; } = null!;
   }
}