using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using PersonalSite.Models;
using Octokit;

namespace PersonalSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async  Task<ActionResult> Projects()
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool-app"));
            client.Credentials = new Credentials("411285ddbf8d8b4afc075ef014317e8ab5bc4a89");

            var repos = await client.Repository.GetAllForUser("sonofakel");
            var starsDict = new Dictionary<string, int> { };

            foreach (var repo in repos)
            {
                starsDict.Add(repo.Name, repo.StargazersCount);
            }

            var newStarsDict = starsDict.OrderByDescending(pair => pair.Value).Take(3);

            ViewBag.Repos = newStarsDict;
            return View();
        }

        
    }
}
