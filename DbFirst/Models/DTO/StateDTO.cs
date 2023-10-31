namespace DbFirst.Models.DTO
{
    public class StateDTO
    {
        public int Id { get; set; }

        public string NameBorough { get; set; } = null!;

        public string NameCity { get; set; } = null!;

        public string NameState { get; set; } = null!;

        public string CountryState { get; set; } = null!;
    }
}
