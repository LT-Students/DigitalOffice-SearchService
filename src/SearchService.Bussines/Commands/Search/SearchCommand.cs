using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DigitalOffice.Models.Broker.Models.Department;
using DigitalOffice.Models.Broker.Models.News;
using DigitalOffice.Models.Broker.Models.Office;
using DigitalOffice.Models.Broker.Models.Project;
using DigitalOffice.Models.Broker.Models.User;
using DigitalOffice.Models.Broker.Models.Wiki;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Requests.News;
using LT.DigitalOffice.Models.Broker.Requests.Office;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Requests.Wiki;
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

    Regex regex = new Regex("[^а-яёА-ЯЁa-zA-Z0-9\\s]");
    text = regex.Replace(text, " ");

    if (string.IsNullOrEmpty(text))
    {
      return result;
    }

    string[] words = text.ToLower().Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

    if (filter.IncludeDepartments)
    {
      ISearchResponse<DepartmentSearchData> departments =
        await _rcDepartments.ProcessRequest<ISearchDepartmentsRequest, ISearchResponse<DepartmentSearchData>>(
          ISearchDepartmentsRequest.CreateObj(words));

      if (departments is null)
      {
        _logger.LogError($"No response from DepartmentService");
      }

      result.Department = departments;
    }

    if (filter.IncludeNews)
    {
      ISearchResponse<NewsSearchData> news =
        await _rcNews.ProcessRequest<ISearchNewsRequest, ISearchResponse<NewsSearchData>>(
          ISearchNewsRequest.CreateObj(words));

      if (news is null)
      {
        _logger.LogError($"No response from NewsService");
      }

      result.News = news;
    }

    if (filter.IncludeOffices)
    {
      ISearchResponse<OfficeSearchData> offices =
        await _rcOffices.ProcessRequest<ISearchOfficesRequest, ISearchResponse<OfficeSearchData>>(
          ISearchOfficesRequest.CreateObj(words));

      if (offices is null)
      {
        _logger.LogError($"No response from OfficeService");
      }

      result.Office = offices;
    }

    if (filter.IncludeProjects)
    {
      ISearchResponse<ProjectSearchData> projects =
        await _rcProjects.ProcessRequest<ISearchProjectsRequest, ISearchResponse<ProjectSearchData>>(
          ISearchProjectsRequest.CreateObj(words));

      if (projects is null)
      {
        _logger.LogError($"No response from ProjectService");
      }

      result.Project = projects;
    }

    if (filter.IncludeUsers)
    {
      ISearchResponse<UserSearchData> users =
        await _rcUsers.ProcessRequest<ISearchUsersRequest, ISearchResponse<UserSearchData>>(
          ISearchUsersRequest.CreateObj(words));

      if (users is null)
      {
        _logger.LogError($"No response from UserService");
      }

      result.User = users;
    }

    if (filter.IncludeWiki)
    {
      ISearchResponse<WikiSearchData> wiki =
        await _rcWiki.ProcessRequest<ISearchWikiRequest, ISearchResponse<WikiSearchData>>(
          ISearchWikiRequest.CreateObj(words));

      if (wiki is null)
      {
        _logger.LogError($"No response from WikiService");
      }

      result.Wiki = wiki;
    }

    return result;
  }
}
