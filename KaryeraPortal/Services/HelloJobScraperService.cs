using HtmlAgilityPack;
using KaryeraPortal.Models;
using KaryeraPortal.ViewModels;

namespace KaryeraPortal.Services
{
    public class HelloJobScraperService
    {
        public List<JobModelVM> ScrapJobList()
        {
            string url = "https://www.hellojob.az"; // Vakansiyaların əsas səhifəsi
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            // Bütün vakansiya elementlərini tapırıq
            var vacancyNodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'vacancies__item')]");
            List<JobModelVM> Jobs = new List<JobModelVM>();
            if (vacancyNodes != null)
            {
                foreach (var node in vacancyNodes)
                {
                    JobModelVM jobvm = new JobModelVM();
                   // İş başlığını çıxar
                    var titleNode = node.SelectSingleNode(".//h3");
                    jobvm.Title = titleNode != null ? titleNode.InnerText.Trim() : "N/A";

                    // Şirkət adını çıxar
                    var companyNode = node.SelectSingleNode(".//p[@class='vacancy_item_company']");
                    jobvm.Company = companyNode != null ? companyNode.InnerText.Trim() : "N/A";

                    // Tarixi çıxar
                    var dateNode = node.SelectSingleNode(".//span[@class='vacancy_item_time']");
                    jobvm.DatePosted = dateNode != null ? dateNode.InnerText.Trim() : "N/A";

                    // Maaşı çıxar
                    var salaryNode = node.SelectSingleNode(".//span[@class='vacancies__price']");
                    jobvm.Salary = salaryNode != null ? salaryNode.InnerText.Trim() : "N/A";
                    //şəkil
                    var imageNode = node.SelectSingleNode(".//div[@class='vacancies__icon']/img");
                    jobvm.ImageUrl = imageNode != null ? imageNode.GetAttributeValue("src", "N/A") : "N/A";
                    // Linki çıxar
                    jobvm.JobUrl = "https://www.hellojob.az" + node.GetAttributeValue("href", "N/A");
                    jobvm.SourceSite = "Hellojob.az";
                    Jobs.Add(jobvm);
                }
            }
                return Jobs;
        }
    }
}
