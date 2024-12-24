namespace KaryeraPortal.ViewModels
{
    public class JobModelVM
    {
        public string Title { get; set; } = null!;
        public string? Company { get; set; }
        public string? Location { get; set; }
        public string? DatePosted { get; set; }
        public string? Salary { get; set; }
        public string? ImageUrl { get; set; }
        public string? JobUrl { get; set; }
        public string? SourceSite { get; set; }
    }
}
