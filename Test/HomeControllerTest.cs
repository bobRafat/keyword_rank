using System;
using InfoTrack.KeywordRank.Controllers;
using InfoTrack.KeywordRank.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test
{
    public class HomeControllerTest
    {
        [Fact]
        public void HomeControllerIndexModelIsNull()
        {
            var stubService = new Mock<IKeywordSearchService>();

            var sut = new HomeController(stubService.Object);

            ViewResult viewResult = (ViewResult)sut.Index();

            Assert.Null(viewResult.Model);
        }
    }
}
