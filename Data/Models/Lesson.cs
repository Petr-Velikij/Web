namespace WebTutorCore.Data.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public uint Day { get; set; }
        public uint Month { get; set; }
        public uint Year { get; set; }

    }
}
