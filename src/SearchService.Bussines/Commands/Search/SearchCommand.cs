using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DigitalOffice.Models.Broker.Models.Department;
using DigitalOffice.Models.Broker.Models.News;
using DigitalOffice.Models.Broker.Models.Office;
using DigitalOffice.Models.Broker.Models.Project;
using DigitalOffice.Models.Broker.Models.User;
using DigitalOffice.Models.Broker.Requests.Wiki;
using DigitalOffice.Models.Broker.Responses.Search;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Requests.News;
using LT.DigitalOffice.Models.Broker.Requests.Office;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Response;
using MassTransit;
using Microsoft.Extensions.Logging;
using SearchService.Bussines.Commands.Search.Interfaces;

namespace SearchService.Bussines.Commands.Search;

public class SearchCommand : ISearchCommand
{
  private readonly IRequestClient<ISearchDepartmentsRequest> _rcDepartments;
  private readonly IRequestClient<ISearchNewsRequest> _rcNews;
  private readonly IRequestClient<ISearchOfficesRequest> _rcOffices;
  private readonly IRequestClient<ISearchProjectsRequest> _rcProjects;
  private readonly IRequestClient<ISearchUsersRequest> _rcUsers;
  private readonly IRequestClient<ISearchWikiRequest> _rcWiki;
  private readonly ILogger<SearchCommand> _logger;

  public SearchCommand(
    IRequestClient<ISearchDepartmentsRequest> rcDepartments,
    IRequestClient<ISearchNewsRequest> rcNews,
    IRequestClient<ISearchOfficesRequest> rcOffices,
    IRequestClient<ISearchProjectsRequest> rcProjects,
    IRequestClient<ISearchUsersRequest> rcUsers,
    IRequestClient<ISearchWikiRequest> rcWiki,
    ILogger<SearchCommand> logger)
  {
    _rcUsers = rcUsers;
    _rcDepartments = rcDepartments;
    _rcOffices = rcOffices;
    _rcProjects = rcProjects;
    _rcNews = rcNews;
    _rcWiki = rcWiki;
    _logger = logger;
  }

  public async Task<SearchResultResponse> ExecuteAsync(string text, SearchFilter filter)
  {
    if (!filter.IncludeDepartments &&
        !filter.IncludeNews &&
        !filter.IncludeOffices &&
        !filter.IncludeProjects &&
        !filter.IncludeUsers &&
        !filter.IncludeWiki)
    {
      filter.IncludeDepartments = filter.IncludeNews = filter.IncludeOffices =
        filter.IncludeProjects = filter.IncludeUsers = filter.IncludeWiki = true;
    }

    SearchResultResponse result = new();

    Regex regex = new ("[^а-яёА-ЯЁa-zA-Z0-9\\s]");
    text = regex.Replace(text, " ");

    if (string.IsNullOrEmpty(text))
    {
      return result;
    }

    string[] words = text.ToLower().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    Task<ISearchResponse<DepartmentSearchData>> departmentsSearchResponse = null;
    Task<ISearchResponse<NewsSearchData>> newsSearchResponse = null;
    Task<ISearchResponse<OfficeSearchData>> officesSearchResponse = null;
    Task<ISearchResponse<ProjectSearchData>> projectsSearchResponse = null;
    Task<ISearchResponse<UserSearchData>> usersSearchResponse = null;
    Task<ISearchWikiResponse> wikiSearchResponse = null;

    if (filter.IncludeDepartments)
    {
      departmentsSearchResponse = _rcDepartments.ProcessRequest<ISearchDepartmentsRequest, ISearchResponse<DepartmentSearchData>>(
        ISearchDepartmentsRequest.CreateObj(words));
    }

    if (filter.IncludeNews)
    {
      newsSearchResponse = _rcNews.ProcessRequest<ISearchNewsRequest, ISearchResponse<NewsSearchData>>(
        ISearchNewsRequest.CreateObj(words));
    }

    if (filter.IncludeOffices)
    {
      officesSearchResponse = _rcOffices.ProcessRequest<ISearchOfficesRequest, ISearchResponse<OfficeSearchData>>(
        ISearchOfficesRequest.CreateObj(words));
    }

    if (filter.IncludeProjects)
    {
      projectsSearchResponse = _rcProjects.ProcessRequest<ISearchProjectsRequest, ISearchResponse<ProjectSearchData>>(
        ISearchProjectsRequest.CreateObj(words));
    }

    if (filter.IncludeUsers)
    {
      usersSearchResponse = _rcUsers.ProcessRequest<ISearchUsersRequest, ISearchResponse<UserSearchData>>(
        ISearchUsersRequest.CreateObj(words));
    }

    if (filter.IncludeWiki)
    {
      wikiSearchResponse = _rcWiki.ProcessRequest<ISearchWikiRequest, ISearchWikiResponse>(
        ISearchWikiRequest.CreateObj(words));
    }

    result.Department = filter.IncludeDepartments
      ? await departmentsSearchResponse
      : null;

    if (filter.IncludeDepartments && result.Department is null)
    {
      _logger.LogError("No response from DepartmentService");
    }

    result.News = filter.IncludeNews
      ? await newsSearchResponse
      : null;

    if (filter.IncludeNews && result.News is null)
    {
      _logger.LogError("No response from NewsService");
    }

    result.Office = filter.IncludeOffices
      ? await officesSearchResponse
      : null;

    if (filter.IncludeOffices && result.Office is null)
    {
      _logger.LogError("No response from OfficeService");
    }

    result.Project = filter.IncludeProjects
      ? await projectsSearchResponse
      : null;

    if (filter.IncludeProjects && result.Project is null)
    {
      _logger.LogError("No response from ProjectService");
    }

    result.User = filter.IncludeUsers
      ? await usersSearchResponse
      : null;

    if (filter.IncludeUsers && result.User is null)
    {
      _logger.LogError("No response from UserService");
    }

    result.Wiki = filter.IncludeWiki
      ? await wikiSearchResponse
      : null;

    if (filter.IncludeWiki && result.Wiki is null)
    {
      _logger.LogError("No response from WikiService");
    }

    return result;
  }
}
