using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BetStatsAssist.DataAdapters;
using BetStatsAssist.XMLSoccerService;

namespace BetStatsAssist.Classes
{
    public class XmlSoccerCalls
    {
        public const string ApiKey = "XIKHEXMIJXSFHOXTZHSJGDLFGXWZMGZCPYIYLKYVUFIAZZBMXS";
        public const string baseUrl = "http://www.xmlsoccer.com/FootballDataDemo.asmx";
        public XmlDeserializer D = new XmlDeserializer();
        public InsertCommands DataCommands = new InsertCommands();
        public SelectCommands SelectDataCommands = new SelectCommands();

        public void GetAllLeagues()
        {

            var endpoint = new EndpointAddress(new Uri(baseUrl));
            var binding = new BasicHttpBinding { Name = "FootballDataSoap" };
            
            var serviceClient = new FootballDataDemoSoapClient(binding,endpoint);


            var serviceResult = serviceClient.GetAllLeagues(ApiKey);


            
            var leagues = D.AllLeagues(serviceResult.OuterXml);

            DataCommands.InsertAllLeaguesCommand(leagues);


            //return serviceResult.OuterXml;//listOfLeagues;
        }

        public void GetAllTeams()
        {



            var endpoint = new EndpointAddress(new Uri(baseUrl));
            var binding = new BasicHttpBinding { Name = "FootballDataSoap" };

            var serviceClient = new FootballDataDemoSoapClient(binding, endpoint);

            var serviceResult = serviceClient.GetAllTeams(ApiKey);

            var teams = D.AllTeams(serviceResult.OuterXml);

            DataCommands.InsertAllTeamsCommand(teams);
            

        }

        public void GetNextFixturesByDate(string leagueId, string dateFrom, string dateTo)
        {
            var endpoint = new EndpointAddress(new Uri(baseUrl));
            var binding = new BasicHttpBinding { Name = "FootballDataSoap" };

            binding.MaxReceivedMessageSize = Int32.MaxValue;

            var serviceClient = new FootballDataDemoSoapClient(binding, endpoint);

            serviceClient.GetFixturesByDateIntervalAndLeague(leagueId, ApiKey, dateFrom, dateTo);


        }


        public void GetHistoricFixtureByDate()
        {


            var lastFixture = SelectDataCommands.GetHistoricFixByLeagueId(3).OrderByDescending(f => f.FIX_DATE).Take(1).ToList();
            var lastFixtureDate = new DateTime();
            if (lastFixture.Count > 0)
            {
                var fixDate = lastFixture[0].FIX_DATE;
                
                if (fixDate != null)
                {
                    lastFixtureDate = (DateTime)fixDate.Value;
                }
            }
            else
            {
                lastFixtureDate = DateTime.Now.AddYears(-1);
            }


            var fixs = SelectDataCommands.GetHistoricFixByLeagueId(3);

            var endpoint = new EndpointAddress(new Uri(baseUrl));
            var binding = new BasicHttpBinding {Name = "FootballDataSoap"};

            binding.MaxReceivedMessageSize = Int32.MaxValue;

            var serviceClient = new FootballDataDemoSoapClient(binding, endpoint);

            var serviceResult = serviceClient.GetHistoricMatchesByLeagueAndDateInterval(ApiKey, "3", lastFixtureDate.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));

            var serviceResult2 = serviceClient.GetFixturesByDateIntervalAndLeague("3", ApiKey, DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), DateTime.Now.AddDays(6).ToString("yyyy-MM-dd"));

            var fixtures = D.HistoricFixtures(serviceResult.OuterXml, 3, true);
            var nextFixtures = D.HistoricFixtures(serviceResult2.OuterXml, 3, false);



            
            DataCommands.InsertFixturesCommand(fixtures);
            DataCommands.InsertFixturesCommand(nextFixtures);
        }
    }
}
