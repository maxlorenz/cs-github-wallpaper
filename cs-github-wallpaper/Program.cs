using System.Windows.Forms;
using System.Drawing;
using System.Xml;
using System;

namespace cs_github_wallpaper
{
    class Program
    {
        static void Main(string[] args)
        {
            var downloader = new GitHubSVGDownloader("test");
            Console.ReadKey();
        }
    }
}
