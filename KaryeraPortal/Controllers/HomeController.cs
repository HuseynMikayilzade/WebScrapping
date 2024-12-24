using KaryeraPortal.Models;
using KaryeraPortal.Services;
using KaryeraPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KaryeraPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly HelloJobScraperService _hellojob;
        private readonly JobSearchScraperService _jobsearchService;
        private readonly BusyScraperService _busyscraperService;
        private readonly SmartJobScraperService _smartJobScraperService;
        private readonly JobsGlorriScraperService _jobsGlorriScraperService;

        public HomeController(HelloJobScraperService hellojob, JobSearchScraperService jobsearchService,
            BusyScraperService busyScraperService,SmartJobScraperService smartJobScraperService, JobsGlorriScraperService jobsGlorriScraperService)
        {
            _hellojob = hellojob;
            _jobsearchService = jobsearchService;
            _busyscraperService = busyScraperService;
            _smartJobScraperService = smartJobScraperService;
            _jobsGlorriScraperService = jobsGlorriScraperService;
        }

        public IActionResult Index()
        {
            var AllJobs = new List<JobModelVM>();
            //AllJobs.AddRange(_hellojob.ScrapJobList());
            //AllJobs.AddRange(_jobsearchService.JobList()); //some problem
            //AllJobs.AddRange(_busyscraperService.JobList());
            //AllJobs.AddRange(_smartJobScraperService.JobList());
            //AllJobs.AddRange(_jobsGlorriScraperService.JobList());
            if (AllJobs == null) return NotFound();
            var RecentJobs = AllJobs.OrderByDescending(x => x.DatePosted).TakeLast(10).ToList();
            return View(RecentJobs);
        }   
    }
}