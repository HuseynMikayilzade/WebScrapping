using HtmlAgilityPack;
using KaryeraPortal.ViewModels;

namespace KaryeraPortal.Services
{
    public class JobsGlorriScraperService
    {
        public List<JobModelVM> JobList()
        {
            string url = "https://jobs.glorri.az"; // Vakansiya səhifəsi
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // Vakansiya elementlərini seç
            var vacancyNodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'flex w-full shrink-0 cursor-pointer')]");

            List<JobModelVM> jobs = new List<JobModelVM>();

            if (vacancyNodes != null)
            {
                foreach (var node in vacancyNodes)
                {
                    JobModelVM jobVm = new JobModelVM();

                    // Vakansiyanın başlığını çıxar
                    var titleNode = node.SelectSingleNode(".//h3");
                    jobVm.Title = titleNode != null ? titleNode.InnerText.Trim() : "N/A";

                    // Şirkət adını çıxar
                    var companyNode = node.SelectSingleNode(".//p[contains(@class, 'font-medium')]");
                    jobVm.Company = companyNode != null ? companyNode.InnerText.Split('●')[0].Trim() : "N/A";

                    // Yer məlumatını çıxar
                    var locationNode = node.SelectSingleNode(".//span[contains(text(), 'Azərbaycan')]");
                    jobVm.Location = locationNode != null ? locationNode.InnerText.Trim() : "N/A";

                    // Tarixi çıxar
                    var dateNode = node.SelectSingleNode(".//p[contains(@class, 'text-sm') and contains(text(), 'Dünən')]");
                    jobVm.DatePosted = dateNode != null ? dateNode.InnerText.Trim() : "N/A";

                    // Şəkil URL-ni çıxar
                    var imageNode = node.SelectSingleNode(".//div[contains(@class, 'rounded-full')]/img");
                    jobVm.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", "N/A") : "N/A";

                    // Linki çıxar
                    jobVm.JobUrl = node.GetAttributeValue("href", "N/A");
                    jobVm.SourceSite = "Glorri.az";

                    jobs.Add(jobVm);
                }
            }

            return jobs;
        }
    }
}
