using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetStatsAssist.Classes;

namespace BetStatsAssistWeb.Models
{
    public class LeaguesModel
    {
        public LEAGUE SelectedLeague { get; set; }
        public string SelectedLeagueId { get; set; }

        public List<LEAGUE> Leagues { get; set; }

        public List<FixtureViewModels> Fixtures { get; set; }

    }
}