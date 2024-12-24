using KaryeraPortal.Services;
using KaryeraPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KaryeraPortal.Controllers
{
    public class JobListController : Controller
    {
        private readonly HelloJobScraperService _hellojob;
        private readonly JobSearchScraperService _jobsearchService;
        private readonly BusyScraperService _busyscraperService;
        private readonly SmartJobScraperService _smartJobScraperService;
        private readonly JobsGlorriScraperService _jobsGlorriScraperService;

        public JobListController(HelloJobScraperService hellojob, JobSearchScraperService jobsearchService, BusyScraperService busyscraperService,
            SmartJobScraperService smartJobScraperService, JobsGlorriScraperService jobsGlorriScraperService)
        {
            _hellojob = hellojob;
            _jobsearchService = jobsearchService;
            _busyscraperService = busyscraperService;
            _smartJobScraperService = smartJobScraperService;
            _jobsGlorriScraperService = jobsGlorriScraperService;
        }

        public IActionResult Index(string sort,int page =1)
        {
            var AllJobs = new List<JobModelVM>();
            AllJobs.AddRange(_hellojob.ScrapJobList());
            AllJobs.AddRange(_busyscraperService.JobList());
            AllJobs.AddRange(_smartJobScraperService.JobList());
            AllJobs.AddRange(_jobsGlorriScraperService.JobList());
            //AllJobs.AddRange(_jobsearchService.JobList()); //some problem
            if (AllJobs == null) return NotFound();
            switch (sort)
            {
                case "newest":
                    AllJobs = AllJobs.OrderByDescending(x => x.DatePosted).Skip((page - 1) * 10).Take(10).ToList();
                    break;
                case "oldest":
                    AllJobs = AllJobs.OrderBy(x => x.DatePosted).Skip((page - 1) * 10).Take(10).ToList();
                    break;
                case "glorri":
                    AllJobs = AllJobs.Where(x => x.SourceSite == "Glorri.az").Skip((page - 1) * 10).Take(10).ToList();
                    break;
                case "busy":
                    AllJobs = AllJobs.Where(x => x.SourceSite == "Busy.az").Skip((page - 1) * 10).Take(10).ToList();
                    break;
                case "hellojob":
                    AllJobs = AllJobs.Where(x => x.SourceSite == "Hellojob.az").Skip((page - 1) * 10).Take(10).ToList();
                    break;
                case "smartjob":
                    AllJobs = AllJobs.Where(x => x.SourceSite == "SmartJob.az").Skip((page - 1) * 10).Take(10).ToList();
                    break;
                default:
                    AllJobs = AllJobs.Skip((page - 1) * 10).Take(10).ToList();
                    break;
            }
            if (page <= 0) return BadRequest();
            double count = AllJobs.Count();
            if (count <= 0) return NotFound();
            double totalpage = Math.Ceiling((double)count / 5);
            if (page > totalpage) return BadRequest();
           
            PaginationVm<JobModelVM> paginationVm = new PaginationVm<JobModelVM>
            {
                Items = AllJobs,
                CurrentPage = page,
                TotalPage = totalpage,
                JobsCount = count,
                Sort = sort,
            }; 
            return View(paginationVm);
        }
    }
}
