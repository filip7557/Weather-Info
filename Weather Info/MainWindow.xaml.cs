using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Weather_Info
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiKey = "5f2c5b65b64b29b045a9c119720479ea";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SearchTown_Click(object sender, RoutedEventArgs e)
        {
            await GetData();
        }

        private void city_GotFocus(object sender, RoutedEventArgs e)
        {
            city.Text = "";
        }

        private async void city_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                await GetData();
        }

        private async Task GetData()
        {
            Application.Current.MainWindow.Height = 583;
            var streamTask = client.GetStreamAsync($"https://api.openweathermap.org/data/2.5/weather?q={city.Text}&appid={apiKey}&units=metric");
            var repo = new Repository();
            try
            {
                repo = await JsonSerializer.DeserializeAsync<Repository>(await streamTask);
            }
            catch (HttpRequestException exception)
            {
                MessageBox.Show("Town not found.", "Error", MessageBoxButton.OK);
                return;
            }

            currentWeatherText.Content = $"Current weather in {char.ToUpper(city.Text[0]) + city.Text.Substring(1)}:";

            temperature.Content = "Temperature: " + repo.Main.Temperature + "°C";
            feelsLike.Content = "Feels like: " + repo.Main.FeelsLike + "°C";
            humidity.Content = "Humidity: " + repo.Main.Humidity + "%";
            windSpeed.Content = "Wind speed: " + repo.Wind.Speed + "km/h";

            description.Content = char.ToUpper(repo.Weather[0].Description[0]) + repo.Weather[0].Description.Substring(1);
            var path = $"http://openweathermap.org/img/wn/{repo.Weather[0].Icon}@2x.png";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();

            weatherIcon.Source = bitmap;

            //TODO: Add forecast for next 4 days
            streamTask = client.GetStreamAsync($"https://api.openweathermap.org/data/2.5/onecall?lat={repo.Coord.Latitude}&lon={repo.Coord.Longitude}&exclude=current,minutely,hourly,alerts&appid={apiKey}&units=metric");
            var dailyRepo = new DailyRepository();
            try
            {
                dailyRepo = await JsonSerializer.DeserializeAsync<DailyRepository>(await streamTask);
            }
            catch (HttpRequestException exception)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButton.OK);
                return;
            }
            var date1 = UnixTimeStampToDateTime(dailyRepo.DailyForecasts[1].Dt);
            var date2 = UnixTimeStampToDateTime(dailyRepo.DailyForecasts[2].Dt);
            var date3 = UnixTimeStampToDateTime(dailyRepo.DailyForecasts[3].Dt);
            var date4 = UnixTimeStampToDateTime(dailyRepo.DailyForecasts[4].Dt);
            var day1 = dailyRepo.DailyForecasts[1];
            var day2 = dailyRepo.DailyForecasts[2];
            var day3 = dailyRepo.DailyForecasts[3];
            var day4 = dailyRepo.DailyForecasts[4];

            _1stDay.Content = date1.DayOfWeek;
            _1stDayDesc.Content = char.ToUpper(day1.Weathers[0].Description[0]) + day1.Weathers[0].Description.Substring(1);
            _1stDayTemperature.Content = $"Temperature: {day1.Temperature.Max}°C / {day1.Temperature.Min}°C";
            _1stDayFeelsLike.Content = $"Feels like: {day1.FeelsLike.Max}°C";
            _1stDayWindSpeed.Content = $"Wind speed: {day1.WindSpeed}km/h";
            _1stDayHumidity.Content = $"Humidity: {day1.Humidity}%";

            path = $"http://openweathermap.org/img/wn/{day1.Weathers[0].Icon}@2x.png";
            BitmapImage bitmap1 = new BitmapImage();
            bitmap1.BeginInit();
            bitmap1.UriSource = new Uri(path, UriKind.Absolute);
            bitmap1.EndInit();
            _1stDayImage.Source = bitmap1;

            _2ndDay.Content = date2.DayOfWeek;
            _2ndDayDesc.Content = char.ToUpper(day2.Weathers[0].Description[0]) + day2.Weathers[0].Description.Substring(1);
            _2ndDayTemperature.Content = $"Temperature: {day2.Temperature.Max}°C / {day2.Temperature.Min}°C";
            _2ndDayFeelsLike.Content = $"Feels like: {day2.FeelsLike.Max}°C";
            _2ndDayWindSpeed.Content = $"Wind speed: {day2.WindSpeed}km/h";
            _2ndDayHumidity.Content = $"Humidity: {day2.Humidity}%";

            path = $"http://openweathermap.org/img/wn/{day2.Weathers[0].Icon}@2x.png";
            BitmapImage bitmap2 = new BitmapImage();
            bitmap2.BeginInit();
            bitmap2.UriSource = new Uri(path, UriKind.Absolute);
            bitmap2.EndInit();
            _2ndDayImage.Source = bitmap2;

            _3rdDay.Content = date3.DayOfWeek;
            _3rdDayDesc.Content = char.ToUpper(day3.Weathers[0].Description[0]) + day3.Weathers[0].Description.Substring(1);
            _3rdDayTemperature.Content = $"Temperature: {day3.Temperature.Max}°C / {day3.Temperature.Min}°C";
            _3rdDayFeelsLike.Content = $"Feels like: {day3.FeelsLike.Max}°C";
            _3rdDayWindSpeed.Content = $"Wind speed: {day3.WindSpeed}km/h";
            _3rdDayHumidity.Content = $"Humidity: {day3.Humidity}%";

            path = $"http://openweathermap.org/img/wn/{day3.Weathers[0].Icon}@2x.png";
            BitmapImage bitmap3 = new BitmapImage();
            bitmap3.BeginInit();
            bitmap3.UriSource = new Uri(path, UriKind.Absolute);
            bitmap3.EndInit();
            _3rdDayImage.Source = bitmap3;

            _4thDay.Content = date4.DayOfWeek;
            _4thDayDesc.Content = char.ToUpper(day4.Weathers[0].Description[0]) + day4.Weathers[0].Description.Substring(1);
            _4thDayTemperature.Content = $"Temperature: {day4.Temperature.Max}°C / {day4.Temperature.Min}°C";
            _4thDayFeelsLike.Content = $"Feels like: {day4.FeelsLike.Max}°C";
            _4thDayWindSpeed.Content = $"Wind speed: {day4.WindSpeed}km/h";
            _4thDayHumidity.Content = $"Humidity: {day4.Humidity}%";

            path = $"http://openweathermap.org/img/wn/{day4.Weathers[0].Icon}@2x.png";
            BitmapImage bitmap4 = new BitmapImage();
            bitmap4.BeginInit();
            bitmap4.UriSource = new Uri(path, UriKind.Absolute);
            bitmap4.EndInit();
            _4thDayImage.Source = bitmap4;
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
