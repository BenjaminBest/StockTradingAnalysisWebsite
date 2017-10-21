using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Core.Common;
using System;
using System.Threading.Tasks;

namespace StockTradingAnalysis.Core.Tests.Common
{
    [TestClass]
    public class HtmlDownloadTests
    {
        private const string HttpWorkingUrl = "http://google.de";
        private const string HttpsWorkingUrl = "https://google.de";
        private const string HttpsNonWorkingUrl = "https://gooooooogle.de";

        [TestMethod]
        public async Task CreateHttpClientShouldDownloadAHttpUri()
        {
            var result = await HtmlDownload.CreateHttpClient(new Uri(HttpWorkingUrl));

            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task CreateHttpClientShouldDownloadAHttpsUri()
        {
            var result = await HtmlDownload.CreateHttpClient(new Uri(HttpsWorkingUrl));

            result.Should().NotBeEmpty();
        }

        //TODO: Test non working async url

        [TestMethod]
        public void CreateHttpClientSyncShouldDownloadAHttpUri()
        {
            var result = HtmlDownload.CreateHttpClientSync(new Uri(HttpWorkingUrl));

            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public void CreateHttpClientSyncShouldDownloadAHttpsUri()
        {
            var result = HtmlDownload.CreateHttpClientSync(new Uri(HttpsWorkingUrl));

            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public void CreateHttpClientSyncShouldThrowWhenUriNotExist()
        {
            Action act = () => HtmlDownload.CreateHttpClientSync(new Uri(HttpsNonWorkingUrl));

            act.ShouldThrow<Exception>();
        }


        [TestMethod]
        public async Task CreateWebRequestShouldDownloadAHttpUri()
        {
            var result = await HtmlDownload.CreateWebRequest(new Uri(HttpWorkingUrl));

            result.Should().NotBeEmpty();
        }

        [TestMethod]
        public async Task CreateWebRequestShouldDownloadAHttpsUri()
        {
            var result = await HtmlDownload.CreateWebRequest(new Uri(HttpsWorkingUrl));

            result.Should().NotBeEmpty();
        }

        //TODO: Test non working async url
    }
}
