using System.ComponentModel.DataAnnotations;

namespace MAINPROJECT.ModelDto
{
    public class StudentDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
