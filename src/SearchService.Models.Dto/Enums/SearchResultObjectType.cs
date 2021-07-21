using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LT.DigitalOffice.SearchService.Models.Dto.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SearchResultObjectType
    {
        User,
        Project,
        Department
    }
}
