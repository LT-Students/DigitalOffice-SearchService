using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.SearchService.Models.Dto.Requests;

public record SearchFilter
{
  /// <summary>
  /// If true, the answer includes search results by department.
  /// </summary>
  [FromQuery(Name = "includedepartments")]
  public bool IncludeDepartments { get; set; } = false;

  /// <summary>
  /// If true, the answer includes news search results.
  /// </summary>
  [FromQuery(Name = "includenews")]
  public bool IncludeNews { get; set; } = false;


  /// <summary>
  /// If true, the answer includes office search results.
  /// </summary>
  [FromQuery(Name = "includeoffices")]
  public bool IncludeOffices { get; set; } = false;

  /// <summary>
  /// If true, the answer includes project search results.
  /// </summary>
  [FromQuery(Name = "includeprojects")]
  public bool IncludeProjects { get; set; } = false;

  /// <summary>
  /// If true, the response includes user search results.
  /// </summary>
  [FromQuery(Name = "includeusers")]
  public bool IncludeUsers { get; set; } = false;

  /// <summary>
  /// If true, the answer includes search results for articles and rubrics.
  /// </summary>
  [FromQuery(Name = "includewiki")]
  public bool IncludeWiki { get; set; } = false;
}
