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


        public void GetHistoricFixtureByDate()
        {

            var fixs = SelectDataCommands.GetHistoricFixByLeagueId(3);

            var endpoint = new EndpointAddress(new Uri(baseUrl));
            var binding = new BasicHttpBinding {Name = "FootballDataSoap"};

            binding.MaxReceivedMessageSize = Int32.MaxValue;

            var serviceClient = new FootballDataDemoSoapClient(binding, endpoint);

            var serviceResult = serviceClient.GetHistoricMatchesByLeagueAndDateInterval(ApiKey, "3", "2015-07-30", "2016-01-15");

            var Fixtures = D.HistoricFixtures(serviceResult.OuterXml, 3);


            
            DataCommands.InsertFixturesCommand(Fixtures);

        }
    }
}
