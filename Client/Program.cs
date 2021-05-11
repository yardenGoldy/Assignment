using System;
using System.Net.Http;
using System.Threading;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Random randomRequests = new Random();
            var tasks = randomRequests.Next(100000);
            HttpClient http = new HttpClient();
            string urlPerform = $"https://localhost:44389/api/task/perform/{tasks}";
            var requestResponse = http.GetAsync(urlPerform).Result;
            if(requestResponse.IsSuccessStatusCode)
            {
                int delay = randomRequests.Next(30, 120);
                Thread.Sleep(delay * 10);
                var data = requestResponse.Content.ReadAsAsync<string>().Result;
                string urlNotify = $"https://localhost:44389//api/task/notify/{data}";
                var result = http.GetAsync(urlNotify).Result;
            }
        }
    }
}
