using HtmlAgilityPack;
using KaryeraPortal.ViewModels;

namespace KaryeraPortal.Services
{
    public class BusyScraperService
    {
        public List<JobModelVM> JobList()
        {
            string url = "https://busy.az/vacancies"; // Vakansiyaların əsas səhifəsi
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // Bütün vakansiya elementlərini tapırıq
            var vacancyNodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'job-listing')]");
            List<JobModelVM> jobs = new List<JobModelVM>();
            if (vacancyNodes != null)
            {
                foreach (var node in vacancyNodes)
                {
                    JobModelVM jobVm = new JobModelVM();
                    //İş başlığını çıxar
                    var titleNode = node.SelectSingleNode(".//h3[@class='job-listing-title']");
                    jobVm.Title = titleNode != null ? titleNode.InnerText.Trim() : "N/A";

                    // Şirkət adını çıxar
                    var companyNode = node.SelectSingleNode(".//li[i[contains(@class, 'icon-material-outline-business')]]");
                    jobVm.Company = companyNode != null ? companyNode.InnerText.Trim() : "N/A";
                    // Yer məlumatını çıxar
                    var locationNode = node.SelectSingleNode(".//li[i[contains(@class, 'icon-material-outline-location-on')]]");
                    jobVm.Location = locationNode != null ? locationNode.InnerText.Trim() : "N/A";

                    // Tarixi çıxar
                    var dateNode = node.SelectSingleNode(".//li[i[contains(@class, 'icon-material-outline-access-time')]]");
                    jobVm.DatePosted = dateNode != null ? dateNode.InnerText.Trim() : "";

                    //// Maaşı çıxar
                    //var salaryNode = node.SelectSingleNode(".//span[@class='vacancies__price']");
                    //jobVm.Salary = salaryNode != null ? salaryNode.InnerText.Trim() : "";
                    //şəkil
                    var imageNode = node.SelectSingleNode(".//div[@class='vacancies__icon']/img");
                    jobVm.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", "N/A") : "N/A";
                    // Linki çıxar
                    jobVm.JobUrl =node.GetAttributeValue("href", "N/A");
                    jobVm.SourceSite = "Busy.Az";
                    jobs.Add(jobVm);
                }
            }
            return jobs;
        }
    }
}
