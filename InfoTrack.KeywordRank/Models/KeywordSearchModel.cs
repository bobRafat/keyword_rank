
using System.ComponentModel.DataAnnotations;

namespace InfoTrack.KeywordRank.Models
{
  public class KeywordSearchModel
  {
    [Required(ErrorMessage = "Please enter the keyword.")]
    public string Keyword { get; set; }

    [Required(ErrorMessage = "Please enter the url.")]
    [Url]
    public string Url { get; set; }
  }
}
