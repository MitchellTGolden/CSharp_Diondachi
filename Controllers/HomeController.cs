using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Diondachi.Models;
using Microsoft.AspNetCore.Http;

namespace Diondachi.Controllers
{
    public class HomeController : Controller
    {
        public static int EggCrack = -1;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Console.WriteLine(EggCrack);
            EggCrack++;
            ViewBag.EggCrack = EggCrack;
            return View();
        }
        [HttpPost("/create")]
        public IActionResult Create(string name)
        {
            HttpContext.Session.SetString("Dachi.Name", name);
            string LocalVariable = HttpContext.Session.GetString("Dachi.Name");
            Console.WriteLine(LocalVariable);
            Dachi newDachi = new Dachi();
            HttpContext.Session.SetObjectAsJson("Dachi", newDachi);
            ViewBag.Name = LocalVariable;
            ViewBag.Dachi = newDachi;



            return View("Dachi", newDachi);
        }

        [HttpGet("dachi")]
        public IActionResult Dachi()
        {
            Dachi newDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            string LocalVariable = HttpContext.Session.GetString("Dachi.Name");
            ViewBag.Name = LocalVariable;
            ViewBag.Dachi = newDachi;

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("crack")]

        [HttpGet("feed")]
        public IActionResult feed()
        {
            Dachi newDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            string LocalVariable = HttpContext.Session.GetString("Dachi.Name");
            ViewBag.Name = LocalVariable;
            int CurrentFill = newDachi.Fullness;
            newDachi.Feed();
            int newFill = newDachi.Fullness;
            if (newFill > CurrentFill)
            {
                ViewBag.Result = $"You fed {LocalVariable}! Fullness has gone up by {newFill - CurrentFill} at the cost of 1 meal";
                ViewBag.Emote = 0;
            }
            else
            {
                ViewBag.Result = $"You fed {LocalVariable}! Fullness hasn't gone up at all. {LocalVariable} said he is sick of this diet!";
                ViewBag.Emote = 1;
            }
            HttpContext.Session.SetObjectAsJson("Dachi", newDachi);
            return View("Dachi", newDachi);
        }
        [HttpGet("play")]
        public IActionResult Play()
        {
            string LocalVariable = HttpContext.Session.GetString("Dachi.Name");
            Dachi newDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            int CurrentHap = newDachi.Happiness;
            newDachi.Play();
            int newHap = newDachi.Happiness;
            if (newHap > CurrentHap)
            {
                ViewBag.Result = $"You played with {LocalVariable}! Happiness has gone up by {newHap - CurrentHap} at the cost of 5 Energy";
                ViewBag.Emote = 2;
            }
            else
            {
                ViewBag.Result = $"You played with {LocalVariable}! He just wasn't feeling this half-assed 'play' you still lost 5 Energy";
                ViewBag.Emote = 3;
            }
            HttpContext.Session.SetObjectAsJson("Dachi", newDachi);

            ViewBag.Name = LocalVariable;
            return View("Dachi", newDachi);
        }
        [HttpGet("work")]
        public IActionResult Work()
        {
            Dachi newDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            int CurrentMeal = newDachi.Meals;
            newDachi.Work();
            int newMeal = newDachi.Meals;
            HttpContext.Session.SetObjectAsJson("Dachi", newDachi);
            string LocalVariable = HttpContext.Session.GetString("Dachi.Name");
            ViewBag.Name = LocalVariable;
            ViewBag.Result = $"Your Diondachi {LocalVariable} goes to work and recieved {newMeal - CurrentMeal} meals.";
            ViewBag.Emote = 4;
            return View("Dachi", newDachi);
        }
        [HttpGet("sleep")]
        public IActionResult Sleep()
        {
            Dachi newDachi = HttpContext.Session.GetObjectFromJson<Dachi>("Dachi");
            newDachi.Sleep();
            HttpContext.Session.SetObjectAsJson("Dachi", newDachi);
            string LocalVariable = HttpContext.Session.GetString("Dachi.Name");
            ViewBag.Name = LocalVariable;
            ViewBag.Result = $"Your Diondachi {LocalVariable} is taking a nap";
            ViewBag.Emote = 5;
            return View("Dachi", newDachi);
        }

        [HttpGet("reset")]
        public IActionResult Reset()
        {
            EggCrack = -1;
            HttpContext.Session.Clear();
            return Redirect("/");

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
