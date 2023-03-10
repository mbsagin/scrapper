using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace Scrapper
{
    public class MedyaTakipScrapper : IScrapper
    {
        public MedyaTakipScrapper()
        {

        }

        public IScrappedModel Scrap(string url)
        {
            if (url.Contains("clips.medyatakip.com") is false)
                throw new Exception("URL is not recognized!");

            var web = new HtmlWeb();
            var doc = web.Load(url);

            if (url.Contains("/pm/clip/"))
                return ScrapYaziliBasin(doc);

            if (url.Contains("/dm/clip/"))
                return ScrapOnlineBasin(doc);

            throw new Exception("URL could not scrapped!");
        }

        private MedyaTakipOnlineBasinModel ScrapOnlineBasin(HtmlDocument doc)
        {
            var titleNode = doc.DocumentNode.SelectSingleNode("//*[@id='nav-tabContent']/div/center/h4");
            var dateNode = doc.DocumentNode.SelectSingleNode("//html/body/div/div[1]/div/div[1]/div/div/div/div/div[1]/div[2]/div");
            var webNode = doc.DocumentNode.SelectSingleNode("//html/body/div/div[1]/div/div[1]/div/div/div/div/div[2]/div[2]/div");
            var keywordNodes = doc.DocumentNode.SelectNodes("//*[@id='words']/div/*[@class='brandName']/a");

            var model = new MedyaTakipOnlineBasinModel()
            {
                Url = webNode.InnerText.Replace("\n", "").Trim(),
                Title = titleNode.InnerText.Replace("\n", "").Trim().Replace("&quot;", ""),
                DateStr = dateNode.InnerText.Replace("\n", "").Trim(),
                Keywords = keywordNodes.Select(n => n.InnerText.Split("(")[0].Trim()).Distinct()
            };

            return model;
        }

        private MedyaTakipYaziliBasinModel ScrapYaziliBasin(HtmlDocument doc)
        {
            var keywordNodes = doc.DocumentNode.SelectNodes("//*[@id='words']/div/*[@class='brandName']/a");
            
            var model = new MedyaTakipYaziliBasinModel()
            {
                PublishName = GetItemByIndex(2, 1),
                Period = GetItemByIndex(1,2),
                PageNo = Convert.ToInt32(GetItemByIndex(3, 1)),
                PublishDateStr = GetItemByIndex(1, 1),
                AdEquivalent = Convert.ToDecimal(GetItemByIndex(3,2).Replace(',', '.').Replace("₺", string.Empty)),
                City = GetItemByIndex(4, 1),
                Circulation = Convert.ToInt32(GetItemByIndex(2, 2).Replace(".", "")),
                Keywords = keywordNodes.Select(n => n.InnerText.Split("(")[0].Trim()).Distinct()
            };
            
            return model;

            string GetItemByIndex(int colIndex, int rowIndex)
            {
                var node = doc.DocumentNode.SelectSingleNode($"//html/body/div/div[1]/div/div[4]/div/div/div/div[{rowIndex}]/div[{colIndex}]/div");
                return node?.InnerText.Replace("\n", "").Split(":")[1].Trim() ?? string.Empty;
            }
        }
    }

    public class MedyaTakipOnlineBasinModel : IScrappedModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string DateStr { get; set; }
        public DateTime Date { get => DateTime.Parse(this.DateStr); }
        public IEnumerable<string> Keywords { get; set; }
    }

    public class MedyaTakipYaziliBasinModel : IScrappedModel
    {
        public string PublishName { get; set; }
        public string Period { get; set; }
        public int PageNo { get; set; }
        public string PublishDateStr { get; set; }
        public DateTime PublishDate { get => DateTime.Parse(this.PublishDateStr); }
        public IEnumerable<string> Keywords { get; set; }
        public decimal AdEquivalent { get; set; }
        public string City { get; set; }
        public int Circulation { get; set; }
    }
}