using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Responses;

namespace SearchService.Bussines.Commands.Search.Interfaces
{
  [AutoInject]
  public interface ISearchCommand
  {
    Task<SearchResponse> ExecuteAsync(string text, SearchFilter filter);
  }
}
