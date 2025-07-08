namespace Domain.DTO.Serilog
{
    public class LogsData
    {
        public int Id { get; set; }

        public string Timestamp { get; set; } = string.Empty;

        public string Level { get; set; } = string.Empty;

        public string? Exception { get; set; }

        public string? RenderedMessage { get; set; }

        public string? Properties { get; set; }
    }
}
