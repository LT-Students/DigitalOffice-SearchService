using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SearchService.Models.Dto.Requests
{
  public record SearchFilter
  {
    [FromQuery(Name = "includeusers")]
    public bool IncludeUsers { get; set; } = false;

    [FromQuery(Name = "includeprojects")]
    public bool IncludeProjects { get; set; } = false;

    [FromQuery(Name = "includedepartments")]
    public bool IncludeDepartments { get; set; } = false;

    [FromQuery(Name = "includenews")]
    public bool IncludeNews { get; set; } = false;

    public bool IncludeAll => !IncludeUsers
      && !IncludeProjects
      && !IncludeDepartments
      && !IncludeNews;
  }
}
