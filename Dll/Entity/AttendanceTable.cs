namespace Dll.Entity
{
    public class AttendanceTable
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Today;
        public string Name { get; set; }
        public string StudentID { get; set; }
        public string subjectName { get; set; }
        #region Relations
        //public List<Student> StudentId { get; set; }

        // public List<Subject> subjects { get; set; }
        #endregion
    }
}