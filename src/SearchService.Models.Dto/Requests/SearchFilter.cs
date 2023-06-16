using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SearchService.Models.Dto.Requests;

public record SearchFilter
{
  [FromQuery(Name = "includedepartments")]
  public bool IncludeDepartments { get; set; } = false;

  [FromQuery(Name = "includenews")]
  public bool IncludeNews { get; set; } = false;

  [FromQuery(Name = "includeoffices")]
  public bool IncludeOffices { get; set; } = false;

  [FromQuery(Name = "includeprojects")]
  public bool IncludeProjects { get; set; } = false;

  [FromQuery(Name = "includeusers")]
  public bool IncludeUsers { get; set; } = false;

  [FromQuery(Name = "includewiki")]
  public bool IncludeWiki { get; set; } = false;
}
