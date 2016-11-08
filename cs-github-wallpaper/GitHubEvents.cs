using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace cs_github_wallpaper
{
    class GitHubEvent
    {
        public class Repo
        {
            public string name;
        }

        public DateTime created_at;
        public Repo repo;
        public string type;
    }

    class GitHubEvents
    {
        const int MAX_PAGES = 10;

        private string userName;
        private JavaScriptSerializer serializer;

        public GitHubEvents(string UserName)
        {
            userName = UserName;
            serializer = new JavaScriptSerializer();
        }

        public IEnumerable<GitHubEvent> getEvents()
        {
            var jsonLists = getJsonLists();

            foreach (var json in jsonLists)
            {
                var jsonObjects = serializer.Deserialize<List<GitHubEvent>>(json);

                foreach (var obj in jsonObjects)
                {
                    yield return obj;
                }
            }
        }

        private IEnumerable<string> getJsonLists()
        {
            for (var i = 1; i <= MAX_PAGES; i++)
            {
                var client = new WebClient();

                client.Headers.Add(HttpRequestHeader.UserAgent, "custom");
                client.QueryString.Set("page", i.ToString());

                yield return client.DownloadString("https://api.github.com/users/" + userName + "/events");
            }
        }
    }
}
