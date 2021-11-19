using System.Threading.Tasks;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Responses;
using Microsoft.AspNetCore.Mvc;
using SearchService.Bussines.Commands.Search.Interfaces;

namespace LT.DigitalOffice.SearchService.Controllers
{
  [Route("[controller]")]
  [ApiController]
  public class SearchController : ControllerBase
  {
    [HttpGet]
    public async Task<SearchResponse> SearchAsync(
      [FromServices] ISearchCommand command,
      [FromQuery] string text,
      [FromQuery] SearchFilter filter)
    {
      return await command.ExecuteAsync(text, filter);
    }
  }
}
