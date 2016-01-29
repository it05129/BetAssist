using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetStatsAssist.Classes;

namespace BetStatsAssist.DataAdapters
{
    public class SelectCommands
    {
        public DataClassesDataContext Db = new DataClassesDataContext("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\Visual Studio 2015\\Projects\\BetStatsAssistWeb\\BetStatsAssistWeb\\App_Data\\BetStatsAssistDB.mdf;Integrated Security=True");


        public List<FIXTURE> GetHistoricFixByLeagueId(Int32 leagueId)
        {
            
            var fixtures = Db.GetTable<FIXTURE>();

            var q = from f in fixtures where f.LEAGUE_ID == leagueId select f;


            return q.ToList();
 
        }

        public List<TEAM> GetAllTeams()
        {
            var teams = Db.GetTable<TEAM>();

            var q = from t in teams select t;

            return q.ToList();
        }  
    }
}
