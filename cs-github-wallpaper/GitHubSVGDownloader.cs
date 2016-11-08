using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace cs_github_wallpaper
{
    class GitHubEvent
    {
        public string created_at;
    }

    class GitHubSVGDownloader
    {
        private string userName;
        private JavaScriptSerializer serializer;

        public GitHubSVGDownloader(string UserName)
        {
            userName = UserName;
            serializer = new JavaScriptSerializer();

            foreach (var date in getDates(getJsonLists()))
                Console.WriteLine(date.ToString());
        }

        private IEnumerable<string> getJsonLists()
        {
            for (var i = 0; i <= 10; i++)
            {
                using (var client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "custom");
                    client.QueryString.Set("page", i.ToString());

                    yield return client.DownloadString("https://api.github.com/users/" + userName + "/events");
                }
            }
        }

        public IEnumerable<DateTime> getDates(IEnumerable<string> jsonList)
        {
            foreach (var json in jsonList)
            {
                var jsonObjects = serializer.Deserialize<List<GitHubEvent>>(json);

                foreach (var obj in jsonObjects)
                {
                    yield return DateTime.Parse(obj.created_at).Date;
                }
            }
        }
    }
}
