using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Responses;

namespace SearchService.Bussines.Commands.Search.Interfaces
{
    [AutoInject]
    public interface ISearchCommand
    {
        SearchResponse Execute(string text, SearchFilter filter);
    }
}
