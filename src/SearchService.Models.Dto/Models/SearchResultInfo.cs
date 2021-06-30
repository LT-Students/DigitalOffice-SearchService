using LT.DigitalOffice.SearchService.Models.Dto.Enums;
using System;

namespace LT.DigitalOffice.SearchService.Models.Dto.Models
{
    public record SearchResultInfo
    {
        public SearchResultObjectType Type { get; init; }
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
