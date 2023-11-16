namespace WebTutor.Medels
{
    public class Person
    {
        public int Id { get; set; }
        public int IdAut { get; set; }
        public string Name { get; set; } = "Не указано";
        public string Surname { get; set; } = "Не указано";
        public string? Patronymic { get; set; }
        public ulong? Telefone { get; set; }
        public ulong? Telefone2 { get; set; }
        public string? Email { get; set; }
        public string? Email2 { get; set; }
    }
}
