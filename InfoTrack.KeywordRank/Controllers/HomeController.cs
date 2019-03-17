using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfoTrack.KeywordRank.Models;
using InfoTrack.KeywordRank.Services;

namespace InfoTrack.KeywordRank.Controllers
{
    public class HomeController : Controller
    {

        private readonly IKeywordSearchService _keywordSearchService;
        public HomeController(IKeywordSearchService keywordSearchService)
        {
            _keywordSearchService = keywordSearchService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(KeywordSearchModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var occurences = await _keywordSearchService.GetOccurence(model.Keyword, model.Url);
                    var resultModel = new ResultViewModel
                    {
                        Keyword = model.Keyword,
                        Url = model.Url,
                        Occurence = occurences
                    };

                    return View("Result", resultModel);
                }
                catch (Exception ex)
                {

                    return View("Error", 
                        new ErrorViewModel() {
                            Message =ex.Message
                    });
                }
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
