using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetStatsAssist.Classes;

namespace BetStatsAssistWeb.Controllers
{
    public class UpdateDbController : Controller
    {
        // GET: UpdateDb
        public void UpdateDb()
        {
            XmlSoccerCalls call = new XmlSoccerCalls();

            //call.GetAllLeagues();
            //call.GetAllTeams();
            call.GetHistoricFixtureByDate();
            //return View();
        }


       
    }
}