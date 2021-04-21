using LT.DigitalOffice.SearchService.Models.Dto.Responses;
using SearchService.Bussines.Commands.Search.Interfaces;
using System;

namespace SearchService.Bussines.Commands.Search
{
    public class SearchCommand : ISearchCommand
    {
        public SearchResponse Execute(string text)
        {
            throw new NotImplementedException();
        }
    }
}
