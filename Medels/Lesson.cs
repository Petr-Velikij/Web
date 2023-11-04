namespace WebTutor.Medels
{
    public class Lesson
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime DatePublic { get; set; }
        public string Title { get; set; } = "Без темы";
        public string Description { get; set; } = "Без описания";
        public List<int> Groups { get; set; } = new(); 
        public Type Type { get; set; }
        public string? Venue { get; set; }
    }
    public enum Type
    {
        None,
        Online,
        InReally,
        News
    }
}
