namespace MAINPROJECT.DomaiLayer
{
    public class Employee
    {
        public  int  Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        
        public double Salary {  get; set; } 
        public int? StudentId { get; set; } //foreign key

        public Student Student { get; set; } //navigation
    }
}
