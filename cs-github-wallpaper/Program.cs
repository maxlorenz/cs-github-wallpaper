using System;
using System.Net;

namespace cs_github_wallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            var gitHubEvents = new GitHubEvents("maxlorenz");

            try
            {
                foreach (var evt in gitHubEvents.getEvents())
                {
                    var eventSummary = String.Format("{}: {} -> {}", evt.created_at, evt.type, evt.repo.name);
                    Console.WriteLine(eventSummary);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine("Could not fetch the json data.");
                Console.WriteLine("Error message: " + ex.Message);
            }

            Console.ReadKey();
        }
    }
}
