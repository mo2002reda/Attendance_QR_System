using System.ComponentModel.DataAnnotations;

namespace Dll.Entity
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int level { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        #region Relations
        // public List<AttendanceTable> Attendance { get; set; }




        #region Subject Relation
        // public List<Subject> subjects { get; set; }
        #endregion

        #region Doctor Relation
        //public List<Doctor> Doctors { get; set; }

        #endregion

        //public List<Attendance> Attendances { get; set; } 

        #endregion


    }
}
