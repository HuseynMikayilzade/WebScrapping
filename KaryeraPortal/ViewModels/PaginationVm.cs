namespace KaryeraPortal.ViewModels
{
    public class PaginationVm<T> where T : class
    {
        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPage { get; set; }
        public double JobsCount { get; set; }
        public string Sort { get; set; }
    }
}
