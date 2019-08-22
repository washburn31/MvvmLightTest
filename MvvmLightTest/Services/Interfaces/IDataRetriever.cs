using MvvmLightTest.Model;

namespace MvvmLightTest.Services.Interfaces
{
    public interface IDataRetriever
    {
        CurrentWeather GetWeatherInformation(string city);
    }
}
