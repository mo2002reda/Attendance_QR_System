using System.ComponentModel.DataAnnotations;

namespace Dll.Entity
{
    public class Subject
    {

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Subject Code Is Required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Subject Name Is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? QRCodeText { get; set; }
        #region Extera Relations
        //public List<AttendanceTable>? attendanceTables { get; set; }

        //public List<Student>? students { get; set; }

        //public Doctor? Doctors { get; set; }

        // public List<AttendanceTable> attendances { get; set; } 
        #endregion

    }
}
