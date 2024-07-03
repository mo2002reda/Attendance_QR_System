using System.ComponentModel.DataAnnotations;

namespace PllDoctor.Models
{
    public class SubjectViewModel
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Subject Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Of Subject Is Required")]
        public string Code { get; set; }
    }
}