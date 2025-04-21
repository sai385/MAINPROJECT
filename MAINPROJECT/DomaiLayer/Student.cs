using System.ComponentModel.DataAnnotations;

namespace MAINPROJECT.DomaiLayer
{
    public class Student
    {
        public  int  Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender {  get; set; }

        public ICollection<Employee>Employees { get; set; }=new List<Employee>(); //one to many relation
    }
}
