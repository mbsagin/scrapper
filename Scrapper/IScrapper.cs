namespace Scrapper
{
    public interface IScrapper
    {
        public IScrappedModel Scrap(string url);
    }

    public interface IScrappedModel { 
    }
}
