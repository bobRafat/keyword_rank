using System;
using System.IO;
using InfoTrack.KeywordRank.Services;
using Xunit;

namespace Test
{
    public class ContentParserTest
    {

        [Fact]
        public void ParserShouldReturn1WhenPassedInputWith1InfotrackUrl()
        {

            var searchResult = "<div class=\"rc\"><div class=\"r\"> <a href=\"https://www.infotrack.com.au\"/> </div> </div>";
            var sut = new ContentParser();

            var result = sut.Parse(searchResult, "https://www.infotrack.com.au");

            Assert.Equal(1, result);
        }

        [Fact]
        public void ParserShouldReturn2WhenPassedInputWith2InfotrackUrl()
        {

            var searchResult = "<!doctype html><html itemscope=\"\" itemtype=\"http://schema.org/SearchResultsPage\" lang=\"en-AU\"><head></head> <div> <div class=\"rc\"><div class=\"r\"> <a href=\"https://www.infotrack.com.au\"/> </div> </div><div class=\"rc\"><div class=\"r\"> <a href=\"https://www.infotrack.com.au\"/> </div> </div></div></html>";
            var sut = new ContentParser();

            var result = sut.Parse(searchResult, "https://www.infotrack.com.au");

            Assert.Equal(2, result);

        }

    }
}
