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
        public SearchResponse Search(
            [FromServices] ISearchCommand command,
            [FromQuery] string text)
        {
            return command.Execute(text);
        }
    }
}
