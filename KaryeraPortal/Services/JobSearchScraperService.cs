using HtmlAgilityPack;
using KaryeraPortal.ViewModels;

namespace KaryeraPortal.Services
{
    public class JobSearchScraperService
    {
        public List<JobModelVM> JobList()
        {
            string url = "https://jobsearch.az/vacancies"; // Vakansiya səhifəsi
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // Vakansiya elementlərini seç
            var vacancyNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'list__item')]");

            List<JobModelVM> jobs = new List<JobModelVM>();

            if (vacancyNodes != null)
            {
                foreach (var node in vacancyNodes)
                {
                    JobModelVM jobVm = new JobModelVM();

                    // Vakansiyanın başlığını çıxar
                    var titleNode = node.SelectSingleNode(".//h3[contains(@class, 'list__item__title')]");
                    jobVm.Title = titleNode != null ? titleNode.InnerText.Trim() : "N/A";

                    // Şirkət adını çıxar
                    var companyNode = node.SelectSingleNode(".//a[contains(@class, 'list__item__text')]");
                    jobVm.Company = companyNode != null ? companyNode.InnerText.Split('\n')[1].Trim() : "N/A";

                    // Tarixi çıxar
                    var dateNode = node.SelectSingleNode(".//span[contains(@class, 'text-transform-none')]");
                    jobVm.DatePosted = dateNode != null ? dateNode.InnerText.Trim() : "N/A";

                    // Şəkil URL-ni çıxar
                    var imageNode = node.SelectSingleNode(".//a[contains(@class, 'list__item__logo')]/img");
                    jobVm.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", "N/A") : "N/A";

                    // Linki çıxar
                    jobVm.JobUrl = node.SelectSingleNode(".//a[contains(@class, 'list__item__text')]")
                        ?.GetAttributeValue("href", "N/A");
                    if (!string.IsNullOrEmpty(jobVm.JobUrl))
                    {
                        jobVm.JobUrl = "https://jobsearch.az" + jobVm.JobUrl; // Əgər link nisbidir, tam linkə çevir
                    }

                    jobVm.SourceSite = "JobSearch.az";

                    jobs.Add(jobVm);
                }
            }

            return jobs;
        }
    }
}
