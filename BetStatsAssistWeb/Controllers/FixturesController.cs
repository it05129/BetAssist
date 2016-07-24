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
    public class FixturesController : Controller
    {
        // GET: Fixture
        public ActionResult Index()
        {
            var call = new XmlSoccerCalls();

            var fixtures = new List<FixtureViewModels>();

            //var result = call.DataCommands

            return View("_Fixtures");
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";
            var dbAdapter = new SelectCommands();

            var _leagues = dbAdapter.GetAllLeagues();

            if (!_leagues.Any())
                return HttpNotFound();

            var leagues = new LeaguesModel()
            {
                Leagues = _leagues
            };
            

            return View("About", leagues);
        }

        public ActionResult Fixtures(Int32 id)
        {
            var dbAdapter = new SelectCommands();

            var _fixtures = dbAdapter.GetHistoricFixByLeagueId(id);
            var teams = dbAdapter.GetAllTeams();
            if (!_fixtures.Any())
                return HttpNotFound();

            var fixtures = new List<FixtureViewModels>();

            var leagues = dbAdapter.GetAllLeagues();

            foreach (var _fixture in _fixtures.Where(f=>f.H_SHOTS ==null))
            {
                var tempFixture = new FixtureViewModels()
                {
                    HomeTeam = teams.FirstOrDefault(team => team.API_TEAM_ID == Convert.ToInt32(_fixture.H_TEAM_ID))?.TEAM_NAME,
                    AwayTeam = teams.FirstOrDefault(team => team.API_TEAM_ID == Convert.ToInt32(_fixture.A_TEAM_ID))?.TEAM_NAME,
                    FixDate = _fixture.FIX_DATE.ToString()

                };

                fixtures.Add(tempFixture);
            }

            return View("_Fixtures", fixtures);
            
        }
    }
}