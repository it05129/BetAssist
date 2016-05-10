using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BetStatsAssist.Classes;
using BetStatsAssistWeb.DataAccessLayer;

namespace BetStatsAssistWeb.Models
{
    public class BusinessLayer
    {
        public List<LEAGUE> GetLeagues()
        {
            var betStatsAssistDal = new BetStatsAssistDal();
            return betStatsAssistDal.Leagues.ToList();
        } 
    }
}