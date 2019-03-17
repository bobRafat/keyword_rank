using System;
using System.Net.Http;
using InfoTrack.KeywordRank.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Test
{
    public class GoogleServiceTest
    {
        [Fact]
        public void SearchShouldThrowExceptionIfKeywordIsNull()
        {
            var stubClientFactory = new Mock<IHttpClientFactory>();
            var stubConfiguration = new Mock<IConfiguration>();
            stubConfiguration.Setup(u => u.GetSection("google:numberOfResults").Value).Returns("20");


            var sut = new GoogleService(stubClientFactory.Object, stubConfiguration.Object);

            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => sut.Search("")).Result;

            Assert.Equal("keyword", exception.ParamName);

        }

        [Fact]
        public void SearchShouldThrowExceptionIfKeywordHasMoreThan100Characters()
        {
            var stubClientFactory = new Mock<IHttpClientFactory>();
            var stubConfiguration = new Mock<IConfiguration>();
            var keyword = "igjhwbkebrjebfjbrefbefberfbekrbfkerbfkberfbebrjberjfbejrbfjhebrhjf ehrfe rfhjebrjhfbehjrbfjher f fejhrbfjhbe";
            stubConfiguration.Setup(u => u.GetSection("google:numberOfResults").Value).Returns("20");


            var sut = new GoogleService(stubClientFactory.Object, stubConfiguration.Object);

            var exception = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => sut.Search(keyword)).Result;

            Assert.Equal("keyword", exception.ParamName);

        }
    }
}
