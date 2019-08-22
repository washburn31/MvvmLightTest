namespace MvvmLightTest.Services
{
    public interface IRssService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Model.FeedItem>> GetNewsAsync(string url);
    }
}