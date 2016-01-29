using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetStatsAssist.DataAdapters;

namespace BetStatsAssist.Classes
{
    public class CalculateStats
    {
        SelectCommands Commands = new SelectCommands();

        public List<TEAM_LEAGUE_STAT> GetTeamLeagueStats()
        {

            var allFixtures = Commands.GetHistoricFixByLeagueId(3);

            var allTeams = Commands.GetAllTeams();


            

            var teamLeagueStats = new List<TEAM_LEAGUE_STAT>();

            foreach (var team in allTeams)
            {
                var allTeamFixs = allFixtures.Where(f => f.H_TEAM_ID == team.API_TEAM_ID || f.A_TEAM_ID == team.API_TEAM_ID);

                var homeTeamFixs = allTeamFixs.Where(f => f.H_TEAM_ID == team.API_TEAM_ID);

                var awayTeamFixs = allTeamFixs.Where(f => f.H_TEAM_ID == team.API_TEAM_ID);

                var tempTeamStats = new TEAM_LEAGUE_STAT
                {
                    LEAGUE_ID = 3,
                    TEAM_ID = team.Id,
                    TOT_OVER = allTeamFixs.Count(f=>f.H_GOALS+f.A_GOALS >= 3),
                    TOT_GG = homeTeamFixs.Count(f=>f.H_GOALS >0 && f.A_GOALS>0) + awayTeamFixs.Count(f => f.H_GOALS > 0 && f.A_GOALS > 0),
                    TOT_R_CARDS = allTeamFixs.Sum(f=>(f.H_Y_CARDS + f.H_R_CARDS + f.AWAY_R_CARDS + f.AWAY_Y_CARDS)),
                    TOT_AVER_CARDS = allTeamFixs.Average(f=> (f.H_Y_CARDS + f.H_R_CARDS +f.AWAY_R_CARDS + f.AWAY_Y_CARDS)),
                    TOT_AVER_G_FOR = allTeamFixs.Average(f=> (homeTeamFixs.Sum(h=>h.H_GOALS) + awayTeamFixs.Sum(a=>a.A_GOALS))),
                    TOT_AVER_G_AG = allTeamFixs.Average(f => (homeTeamFixs.Sum(h => h.A_GOALS) + awayTeamFixs.Sum(a => a.H_GOALS))),
                    TOT_AVER_AWAY_G_FOR = awayTeamFixs.Average(f => f.A_GOALS),
                    TOT_AVER_AWAY_G_AG = awayTeamFixs.Average(f=>f.H_GOALS),
                    TOT_AVER_HOME_G_FOR = homeTeamFixs.Average(f=>f.H_GOALS),
                    TOT_AVER_HOME_G_AG = homeTeamFixs.Average(f=>f.A_GOALS),
                    TOT_AVER_SHOTS = allTeamFixs.Average(f=>(homeTeamFixs.Sum(h => h.H_SHOTS) + awayTeamFixs.Sum(a => a.A_SHOTS))),
                    T   OT_AVER_SHOTS_ON_T = allTeamFixs.Average(f=>(homeTeamFixs.Sum(h => h.H_SHOTS_ON_T) + awayTeamFixs.Sum(a => a.A_SHOTS_ON_T))),
                    
                };



            }




            return null;
        } 
    }
}
