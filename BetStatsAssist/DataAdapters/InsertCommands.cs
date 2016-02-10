using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetStatsAssist.Classes;

namespace BetStatsAssist.DataAdapters
{


    public class InsertCommands
    {
        public DataClassesDataContext Db =
            new DataClassesDataContext(
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\Visual Studio 2015\\Projects\\BetStatsAssistWeb\\BetStatsAssistWeb\\App_Data\\BetStatsAssistDB.mdf;Integrated Security=True");

        public SqlConnection DataConnection =
            new SqlConnection(
                connectionString:
                    "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\user\\Documents\\Visual Studio 2015\\Projects\\BetStatsAssistWeb\\BetStatsAssistWeb\\App_Data\\BetStatsAssistDB.mdf;Integrated Security=True");

        public void InsertTeamsLeagueStatsCommand(List<TEAM_LEAGUE_STAT> teamsStats)
        {

            Db.TEAM_LEAGUE_STATs.InsertAllOnSubmit(teamsStats);

            try
            {
                Db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Db.SubmitChanges();
            }


        }


        public void InsertAllLeaguesCommand(List<LEAGUE> leagues)
        {
            SqlCommand cmd =
                new SqlCommand(
                    "INSERT INTO LEAGUES (ID,API_LEAGUE_ID,SEASON,LATEST_MATCH_DATE,COUNTRY,LEAGUE_NAME) VALUES (@ID,@API_LEAGUE_ID,@SEASON,@LATEST_MATCH_DATE,@COUNTRY,@LEAGUE_NAME)")
                {
                    Connection = DataConnection
                };

            DataConnection.Open();
            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@API_LEAGUE_ID", SqlDbType.Int);
            cmd.Parameters.Add("@SEASON", SqlDbType.Char);
            cmd.Parameters.Add("@LATEST_MATCH_DATE", SqlDbType.Date);
            cmd.Parameters.Add("@COUNTRY", SqlDbType.Char);
            cmd.Parameters.Add("@LEAGUE_NAME", SqlDbType.Char);

            //int i = 0;
            foreach (var league in leagues)
            {
                //cmd.Parameters["@ID"].Value = i;
                cmd.Parameters["@API_LEAGUE_ID"].Value = league.Id;
                cmd.Parameters["@SEASON"].Value = "1516";
                cmd.Parameters["@LATEST_MATCH_DATE"].Value = league.LATEST_MATCH_DATE;
                cmd.Parameters["@COUNTRY"].Value = league.COUNTRY;
                cmd.Parameters["@LEAGUE_NAME"].Value = league.LEAGUE_NAME;
                cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //i ++;
            }

            DataConnection.Close();
        }

        public void InsertAllTeamsCommand(List<TEAM> teams)
        {
            var cmd =
                new SqlCommand(
                    "INSERT INTO TEAMS (API_TEAM_ID,TEAM_NAME,COUNTRY,STADIUM) VALUES (@API_TEAM_ID,@TEAM_NAME,@COUNTRY,@STADIUM)")
                {
                    Connection = DataConnection
                };

            DataConnection.Open();
            //cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters.Add("@API_TEAM_ID", SqlDbType.Int);
            cmd.Parameters.Add("@TEAM_NAME", SqlDbType.Char);
            cmd.Parameters.Add("@COUNTRY", SqlDbType.Char);
            cmd.Parameters.Add("@STADIUM", SqlDbType.Char);

            //int i = 0;
            foreach (var team in teams)
            {
                //cmd.Parameters["@ID"].Value = i;
                cmd.Parameters["@API_TEAM_ID"].Value = team.API_TEAM_ID;
                cmd.Parameters["@COUNTRY"].Value = team.COUNTRY;
                cmd.Parameters["@TEAM_NAME"].Value = team.TEAM_NAME;
                cmd.Parameters["@STADIUM"].Value = team.TEAM_NAME;
                cmd.ExecuteNonQuery();
                //cmd.Dispose();
                // i++;
            }

            DataConnection.Close();
        }

        public void InsertFixturesCommand(List<FIXTURE> fixtures)
        {
            Db.FIXTUREs.InsertAllOnSubmit(fixtures);

            try
            {
                Db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                Db.SubmitChanges();
            }


        }
    }
}
