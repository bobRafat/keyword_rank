using System;
using InfoTrack.KeywordRank.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Test
{
    public class KeywordSearchServiceTest
    {
        [Fact]
        public void GetOccurenceShouldThrowErrorIfKeywordEmpty()
        {
            //arange
            var keyword = string.Empty;
            var url = "infotrack.com.au";
            var expected = new Exception();
            var stubLogger = new Mock<ILogger<KeywordSearchService>>();
            var stubGooglService = new Mock<IGoogleService>();
            var stubCcontentParser = new Mock<IContentParser>();


            //act
            var sut = new KeywordSearchService(stubGooglService.Object, stubCcontentParser.Object, stubLogger.Object);
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetOccurence(keyword, url)).Result;

            //assert
            Assert.Equal("keyword", ex.ParamName);
        }


        [Fact]
        public void GetOccurenceShouldThrowErrorIfUrlEmpty()
        {
            //arange
            var keyword = "online title search";
            var url = string.Empty;
            var expected = new Exception();
            var stubLogger = new Mock<ILogger<KeywordSearchService>>();
            var stubGooglService = new Mock<IGoogleService>();
            var stubCcontentParser = new Mock<IContentParser>();

            //act
            var sut = new KeywordSearchService(stubGooglService.Object, stubCcontentParser.Object, stubLogger.Object);
            var ex = Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetOccurence(keyword, url)).Result;

            //assert
            Assert.Equal("url", ex.ParamName);
        }
    }
}
