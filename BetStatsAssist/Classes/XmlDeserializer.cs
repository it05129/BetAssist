using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace BetStatsAssist.Classes
{
    public class XmlDeserializer
    {
        public List<LEAGUE> AllLeagues(string xml)
        {
            
            var leagues = new List<LEAGUE>();
            var leaguesNodes = new List<XmlNode>();

            var xmlLeagues = new XmlDocument();
            xmlLeagues.LoadXml(xml);
            var el = xmlLeagues.DocumentElement;
            var nodelist = el.ChildNodes;
            for (var i = 0; i < nodelist.Count; i++)
            {
                if (nodelist[i].Name == "League") { leaguesNodes.Add(nodelist[i]); }
            }



            foreach (var node in leaguesNodes)
            {

                var nodeEl = XElement.Parse(node.OuterXml);

                var tempLeague = new LEAGUE
                {
                    API_LEAGUE_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='Id']").Value),
                    COUNTRY = nodeEl.XPathSelectElement("//*[local-name()='Country']").Value,
                    LEAGUE_NAME = nodeEl.XPathSelectElement("//*[local-name()='Name']").Value,
                    LATEST_MATCH_DATE = DateTime.Parse(nodeEl.XPathSelectElement("//*[local-name()='LatestMatch']").Value)
                };

                leagues.Add(tempLeague);
            }

            return leagues;
        }

        public List<TEAM> AllTeams(string xml)
        {
            var teams = new List<TEAM>();
            var teamNodes = new List<XmlNode>();

            var xmlTeams = new XmlDocument();
            xmlTeams.LoadXml(xml);
            var el = xmlTeams.DocumentElement;
            var nodelist = el.ChildNodes;
            for (var i = 0; i < nodelist.Count; i++)
            {
                if (nodelist[i].Name == "Team") { teamNodes.Add(nodelist[i]); }
            }



            foreach (var node in teamNodes)
            {

                var nodeEl = XElement.Parse(node.OuterXml);

                var tempTeam = new TEAM()
                {
                    API_TEAM_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='Team_Id']").Value),
                    COUNTRY = nodeEl.XPathSelectElement("//*[local-name()='Country']").Value,
                    TEAM_NAME = nodeEl.XPathSelectElement("//*[local-name()='Name']").Value,
                    STADIUM = nodeEl.XPathSelectElement("//*[local-name()='Stadium']").Value
                };

                teams.Add(tempTeam);
            }


            return teams;
        }

        public List<FIXTURE> HistoricFixtures(string xml, int leagueId, bool isfinished)
        {
            var fixtures = new List<FIXTURE>();
            var fixtureNodes = new List<XmlNode>();

            var xmlfixtures = new XmlDocument();
            xmlfixtures.LoadXml(xml);
            var el = xmlfixtures.DocumentElement;
            var nodelist = el.ChildNodes;
            for (var i = 0; i < nodelist.Count; i++)
            {
                if (nodelist[i].Name == "Match") { fixtureNodes.Add(nodelist[i]); }
            }

            if (isfinished)
            {
                foreach (var node in fixtureNodes)
                {

                    var nodeEl = XElement.Parse(node.OuterXml);

                    var tempFixture = new FIXTURE()
                    {
                        API_FIXTURE_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='FixtureMatch_Id']").Value),
                        FIX_DATE = DateTime.Parse(nodeEl.XPathSelectElement("//*[local-name()='Date']").Value),
                        LEAGUE_ID = leagueId,
                        LEAGUE_ROUND = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='Round']").Value),
                        H_TEAM_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeTeam_Id']").Value),
                        H_CORNERS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeCorners']").Value),
                        H_GOALS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeGoals']").Value),
                        HALFTIME_H_GOALS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HalfTimeHomeGoals']").Value),
                        H_SHOTS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeShots']").Value),
                        H_SHOTS_ON_T = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeShotsOnTarget']").Value),
                        H_FOULS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeFouls']").Value),
                        H_Y_CARDS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeYellowCards']").Value),
                        H_R_CARDS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeRedCards']").Value),
                        A_TEAM_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayTeam_Id']").Value),
                        A_CORNERS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayCorners']").Value),
                        A_GOALS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayGoals']").Value),
                        HALFTIME_A_GOALS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HalfTimeAwayGoals']").Value),
                        A_SHOTS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayShots']").Value),
                        A_SHOTS_ON_T = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayShotsOnTarget']").Value),
                        A_FOULS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayFouls']").Value),
                        AWAY_Y_CARDS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayYellowCards']").Value),
                        AWAY_R_CARDS = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayRedCards']").Value)
                    };

                    fixtures.Add(tempFixture);
                }
            }
            else
            {
                foreach (var node in fixtureNodes)
                {

                    var nodeEl = XElement.Parse(node.OuterXml);

                    var tempFixture = new FIXTURE()
                    {
                        API_FIXTURE_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='Id']").Value),
                        FIX_DATE = DateTime.Parse(nodeEl.XPathSelectElement("//*[local-name()='Date']").Value),
                        LEAGUE_ID = leagueId,
                        LEAGUE_ROUND = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='Round']").Value),
                        H_TEAM_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='HomeTeam_Id']").Value),
                        A_TEAM_ID = Convert.ToInt32(nodeEl.XPathSelectElement("//*[local-name()='AwayTeam_Id']").Value)
                    };

                    fixtures.Add(tempFixture);
                }
            }

            


            return fixtures;
        } 
    }
}
