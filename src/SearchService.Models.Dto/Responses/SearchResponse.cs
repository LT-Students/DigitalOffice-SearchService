using LT.DigitalOffice.SearchService.Models.Dto.Models;
using System.Collections.Generic;

namespace LT.DigitalOffice.SearchService.Models.Dto.Responses
{
    public class SearchResponse
    {
        public List<SearchResultInfo> Items { get; set; } = new();
        public List<string> Errors { get; set; } = new();
    }
}
