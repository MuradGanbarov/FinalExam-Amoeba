namespace FinalExam_Amoeba.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string? Twitter { get; set; }
        public string? Instagram { get; set; }
        public string? Facebook { get; set; }
        public string? LinkedIn { get; set; }
        public int? PositionId { get; set; }
        public Position? Position{ get; set; }
    }
}
