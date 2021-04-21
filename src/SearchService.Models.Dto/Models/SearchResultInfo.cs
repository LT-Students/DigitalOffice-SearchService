using LT.DigitalOffice.SearchService.Models.Dto.Enums;
using System;

namespace LT.DigitalOffice.SearchService.Models.Dto.Models
{
    public class SearchResultInfo
    {
        public SearchResultObjectType Type { get; init; }
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
