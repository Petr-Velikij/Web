namespace WebTutor.Medels
{
    public class Authorization
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Role { get; set; } = "Tutor";
    }
}
