using DigitalOffice.Models.Broker.Models.Department;
using DigitalOffice.Models.Broker.Models.News;
using DigitalOffice.Models.Broker.Models.Office;
using DigitalOffice.Models.Broker.Models.Project;
using DigitalOffice.Models.Broker.Models.User;
using DigitalOffice.Models.Broker.Responses.Search;
using LT.DigitalOffice.Models.Broker.Responses.Search;

namespace LT.DigitalOffice.SearchService.Models.Dto.Response;

public class SearchResultResponse
{
  public ISearchResponse<DepartmentSearchData> Department { get; set; }
  public ISearchResponse<NewsSearchData> News { get; set; }
  public ISearchResponse<OfficeSearchData> Office { get; set; }
  public ISearchResponse<ProjectSearchData> Project { get; set; }
  public ISearchResponse<UserSearchData> User { get; set; }
  public ISearchWikiResponse Wiki { get; set; }

  public SearchResultResponse(
    ISearchResponse<DepartmentSearchData> department = null,
    ISearchResponse<NewsSearchData> news = null,
    ISearchResponse<OfficeSearchData> office = null,
    ISearchResponse<ProjectSearchData> project = null,
    ISearchResponse<UserSearchData> user = null,
    ISearchWikiResponse wiki = null)
  {
    Department = department;
    News = news;
    Office = office;
    Project = project;
    User = user;
    Wiki = wiki;
  }
}
