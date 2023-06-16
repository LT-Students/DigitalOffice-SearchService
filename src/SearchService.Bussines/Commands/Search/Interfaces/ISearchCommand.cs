using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Response;

namespace SearchService.Bussines.Commands.Search.Interfaces;

[AutoInject]
public interface ISearchCommand
{
  Task<SearchResultResponse> ExecuteAsync(string text, SearchFilter filter);
}
