using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;

namespace ThisPersonDoesntExist
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter No of Pictures to download");
            int noOfPictures = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter download folder path");
            string path = Console.ReadLine();
            Run(noOfPictures,path);
            Console.ReadLine();
        }

        public static async void Run(int noOfPictures, string path)
        {
            
            int c = 0;
            using (WebClient imgClient = new WebClient())
            {
                HttpClient client = new HttpClient();
                while (c < noOfPictures)
                {
                    long now = DateTime.Now.Ticks;
                    string json = await client.GetStringAsync("https://this-person-does-not-exist.com/en?new=" + now);
                    Model model = JsonConvert.DeserializeObject<Model>(json);
                    Console.WriteLine(model.Name);
                
                    Uri uri = new Uri("https://this-person-does-not-exist.com/img/" + model.Name);
                    Console.WriteLine(uri.ToString());
                    imgClient.DownloadFile(uri, path+"\\picture"+c+".jpg");
                    c++;
                }
            }

        }

    }
}
