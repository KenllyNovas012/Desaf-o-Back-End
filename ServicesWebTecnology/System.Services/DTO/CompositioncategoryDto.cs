namespace System.Infrastructure.DTO
{
    public class CompositioncategoryDto
    {
        public string Name { get; set; } = null!;
        public string? Uuid { get; set; }
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
