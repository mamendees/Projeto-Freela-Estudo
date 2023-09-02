namespace Freelancer.API.Models
{
    public class CreateProjectModel
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
    }
}
