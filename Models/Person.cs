namespace ormTARpv23.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string PersonCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public bool Admin { get; set; }
        public int DocumentId { get; set; }
        public Document Document { get; set; }
        public int ContactDataId { get; set; }
        public ContactData ContactData { get; set; }
    }
}
