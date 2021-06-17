using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SearchService.Models.Dto.Requests
{
    public class SearchFilter
    {
        [FromQuery(Name = "includeusers")]
        public bool? IncludeUsers { get; set; }

        [FromQuery(Name = "includeprojects")]
        public bool? IncludeProjects { get; set; }

        [FromQuery(Name = "includedepartments")]
        public bool? IncludeDepartments { get; set; }

        public bool IsIncludeUsers => IncludeUsers.HasValue && IncludeUsers.Value;
        public bool IsIncludeProjects => IncludeProjects.HasValue && IncludeProjects.Value;
        public bool IsIncludeDepartments => IncludeDepartments.HasValue && IncludeDepartments.Value;
        public bool IncludeAll => !IncludeUsers.HasValue
                                    && !IncludeProjects.HasValue
                                    && !IncludeDepartments.HasValue;
    }
}
