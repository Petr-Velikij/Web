namespace WebTutorCore.Data.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public ulong Telephone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
