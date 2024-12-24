namespace KaryeraPortal.Models
{
    public class JobModel
    {
        public string Title { get; set; } = null!;
        public string? Company { get; set; } = "N/A";
        public string? Location { get; set; } = "N/A";
        public string? DatePosted { get; set; }
        public string? Salary { get; set; } = "N/A";
        public string? ImageUrl { get; set; }
        public string? JobUrl { get; set; } 
    }
}
