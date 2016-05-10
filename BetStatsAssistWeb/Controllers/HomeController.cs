using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetStatsAssist.Classes;
using BetStatsAssist.DataAdapters;
using BetStatsAssistWeb.Models;

namespace BetStatsAssistWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            var dbAdapter = new SelectCommands();
            
            var leagues = dbAdapter.GetAllLeagues();

            return View(leagues);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}