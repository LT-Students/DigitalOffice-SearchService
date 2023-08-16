using System.Threading.Tasks;
using DigitalOffice.Kernel.OpenApi.OperationFilters;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchService.Bussines.Commands.Search.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace LT.DigitalOffice.SearchService.Controllers;

[Route("[controller]")]
[ApiController]
public class SearchController : ControllerBase
{
  /// <summary>
  /// Global search.
  /// </summary>
  /// <param name="text">Search text.</param>
  [HttpGet("search")]
  [SwaggerOperationFilter(typeof(TokenOperationFilter))]
  [ProducesResponseType(typeof(SearchResultResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<SearchResultResponse> SearchAsync(
    [FromServices] ISearchCommand command,
    [FromQuery] string text,
    [FromQuery] SearchFilter filter)
  {
    return await command.ExecuteAsync(text, filter);
  }
}
