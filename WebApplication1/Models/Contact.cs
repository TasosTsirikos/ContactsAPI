namespace WebApplication1.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName {  get; set; }

        public string? Phone {  get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
