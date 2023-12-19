using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace lab3_Testing.Steps.Part2
{
    [Binding]
    public class BinanceApiSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;
        private string _endpoint;

        public BinanceApiSteps()
        {
            _client = new RestClient();
        }

        [Given(@"I have a valid Binance API endpoint '(.*)'")]
        public void GivenIHaveAValidBinanceApiEndpoint(string url)
        {
            _endpoint = url;
            _client = new RestClient(_endpoint);
        }

        [When(@"I make a GET request to the Binance API for recent trades")]
        public void WhenIMakeAGetRequestToTheBinanceApiForRecentTrades()
        {
            string baseUrl = "https://api.binance.com";

            _request = new RestRequest("api/v3/trades", Method.Get);
            _request.AddParameter("symbol", "BTCUSDT");

            _client = new RestClient(baseUrl);

            _response = _client.Execute(_request);
        }

        [Then(@"I should receive a successful response")]
        public void ThenIShouldReceiveASuccessfulResponse()
        {
            Console.WriteLine($"Response Status Code: {_response.StatusCode}");
            Console.WriteLine($"Response Content: {_response.Content}");

            Assert.AreEqual(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }

        [Then(@"the response should contain recent trades information")]
        public void ThenTheResponseShouldContainRecentTradesInformation()
        {
            Assert.IsTrue(_response.StatusCode == System.Net.HttpStatusCode.OK);

            var recentTrades = JsonConvert.DeserializeObject<List<RecentTradeInfo>>(_response.Content);
            Assert.IsNotNull(recentTrades);
            Assert.IsTrue(recentTrades.Count > 0); 

            var firstTrade = recentTrades[0];
            Assert.IsNotNull(firstTrade.Id);
            Assert.IsNotNull(firstTrade.Price);
            Assert.IsNotNull(firstTrade.Qty);
            Assert.IsNotNull(firstTrade.QuoteQty);
            Assert.IsNotNull(firstTrade.Time);
            Assert.IsNotNull(firstTrade.IsBuyerMaker);
            Assert.IsNotNull(firstTrade.IsBestMatch);
        }
    }

    public class RecentTradeInfo
    {
        public long Id { get; set; }
        public string Price { get; set; }
        public string Qty { get; set; }
        public string QuoteQty { get; set; }
        public long Time { get; set; }
        public bool IsBuyerMaker { get; set; }
        public bool IsBestMatch { get; set; }
    }
}
