namespace ormTARpv23.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
    }
}
