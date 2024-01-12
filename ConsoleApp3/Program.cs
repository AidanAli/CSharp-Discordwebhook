using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

class Program
{


    static async Task Main()

    {
        var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);

        var config = builder.Build();

        var url = config["DISCORD_URL_WEBHOOK:url"];
        string webhookUrl = ($"{url}");
        Console.WriteLine("Enter Message");
        string message = Console.ReadLine();
        await SendDiscordMessage(webhookUrl, message);
    }

    static async Task SendDiscordMessage(String webhookUrl, String message)
    {
        using (HttpClient httpsClient = new HttpClient())
        {
            var payload = new
            {
                content = message
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await httpsClient.PostAsync(webhookUrl, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Message Sent Successfully!");
            }
            else
            {
                Console.WriteLine($"Failed to send message. Satus Code: {response.StatusCode}");
            }
        }
    }
}