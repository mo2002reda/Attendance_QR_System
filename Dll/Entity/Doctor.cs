namespace Dll.Entity
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Relations
        //public List<Student> Students { get; set; }
        //public List<Subject> Subjects { get; set; } 
        #endregion
    }
}