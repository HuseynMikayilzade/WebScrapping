using HtmlAgilityPack;
using KaryeraPortal.ViewModels;

namespace KaryeraPortal.Services
{
    public class SmartJobScraperService
    {
        public List<JobModelVM> JobList()
        {
            string url = "https://smartjob.az/vacancies"; // Vakansiyaların əsas səhifəsi
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // Bütün vakansiya elementlərini tapırıq
            var vacancyNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'brows-job-list')]");
            List<JobModelVM> jobs = new List<JobModelVM>();

            if (vacancyNodes != null)
            {
                foreach (var node in vacancyNodes)
                {
                    JobModelVM jobVm = new JobModelVM();

                    // İş başlığını çıxar
                    var titleNode = node.SelectSingleNode(".//h3/a");
                    jobVm.Title = titleNode != null ? titleNode.InnerText.Trim() : "N/A";

                    // Şirkət adını çıxar
                    var companyNode = node.SelectSingleNode(".//span[contains(@class, 'company-title')]/a");
                    jobVm.Company = companyNode != null ? companyNode.InnerText.Trim() : "N/A";

                    // Yer məlumatını çıxar
                    var locationNode = node.SelectSingleNode(".//span[contains(@class, 'location-pin')]");
                    jobVm.Location = locationNode != null ? locationNode.InnerText.Trim() : "N/A";

                    // Tarixi çıxar
                    var dateNode = node.SelectSingleNode(".//div[contains(@class, 'created-date')]");
                    jobVm.DatePosted = dateNode != null ? dateNode.InnerText.Replace("Yerləşdirilib", "").Trim() : "N/A";

                    // Maaşı çıxar
                    var salaryNode = node.SelectSingleNode(".//div[contains(@class, 'salary-val')]");
                    jobVm.Salary = salaryNode != null ? salaryNode.InnerText.Trim() : "N/A";

                    // Şəkil linkini çıxar
                    var imageNode = node.SelectSingleNode(".//div[contains(@class, 'brows-job-company-img')]/a/img");
                    jobVm.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", "N/A") : "N/A";

                    // Vakansiya linkini çıxar
                    var jobLinkNode = node.SelectSingleNode(".//h3/a");
                    jobVm.JobUrl = jobLinkNode != null ? jobLinkNode.GetAttributeValue("href", "N/A") : "N/A";

                    jobVm.SourceSite = "SmartJob.az";
                    jobs.Add(jobVm);
                }
            }

            return jobs;
        }
    }
}
