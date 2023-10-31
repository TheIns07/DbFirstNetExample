namespace DbFirst.Models.DTO
{
    public class RecordDTO
    {
        public int Id { get; set; }

        public string TitleRecord { get; set; } = null!;

        public string TextRecord { get; set; } = null!;

        public DateTime DateRecorded { get; set; }

        public int RegisterId { get; set; }

        public int TeamId { get; set; }
    }
}
