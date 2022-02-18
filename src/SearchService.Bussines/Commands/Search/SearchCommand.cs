using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Requests.News;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;
using LT.DigitalOffice.Models.Broker.Responses.Search;
using LT.DigitalOffice.SearchService.Models.Dto.Enums;
using LT.DigitalOffice.SearchService.Models.Dto.Models;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Responses;
using MassTransit;
using Microsoft.Extensions.Logging;
using SearchService.Bussines.Commands.Search.Interfaces;

namespace SearchService.Bussines.Commands.Search
{
  public class SearchCommand : ISearchCommand
  {
    private IRequestClient<ISearchProjectsRequest> _rcProjects;
    private IRequestClient<ISearchUsersRequest> _rcUsers;
    private IRequestClient<ISearchDepartmentsRequest> _rcDepartments;
    private IRequestClient<ISearchNewsRequest> _rcNews;
    private ILogger<SearchCommand> _logger;

    #region requests

    private async Task<List<SearchResultInfo>> SearchProjectsAsync(string text, List<string> errors)
    {
      List<SearchResultInfo> projects = new();

      string errorMessage = "Cannot search projects. Please try again later.";

      try
      {
        var response = await _rcProjects.GetResponse<IOperationResult<ISearchResponse>>(
          ISearchProjectsRequest.CreateObj(text));

        if (response.Message.IsSuccess)
        {
          projects.AddRange(
            response.Message.Body.Entities.Select(
              p => new SearchResultInfo
              {
                Type = SearchResultObjectType.Project,
                Id = p.Id,
                Value = p.Value
              }).ToList());

          return projects;
        }

        _logger.LogWarning(errorMessage);
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, errorMessage);
      }
      errors.Add(errorMessage);

      return projects;
    }

    private async Task<List<SearchResultInfo>> SearchDepartmentsAsync(string text, List<string> errors)
    {
      List<SearchResultInfo> departments = new();

      string errorMessage = "Cannot search departments. Please try again later.";

      try
      {
        var response = await _rcDepartments.GetResponse<IOperationResult<ISearchResponse>>(
          ISearchDepartmentsRequest.CreateObj(text));

        if (response.Message.IsSuccess)
        {
          departments.AddRange(
            response.Message.Body.Entities.Select(
              p => new SearchResultInfo
              {
                Type = SearchResultObjectType.Department,
                Id = p.Id,
                Value = p.Value
              }).ToList());

          return departments;
        }

        _logger.LogWarning(errorMessage);
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, errorMessage);
      }
      errors.Add(errorMessage);

      return departments;
    }

    private async Task<List<SearchResultInfo>> SearchUsersAsync(string text, List<string> errors)
    {
      List<SearchResultInfo> users = new();

      string errorMessage = "Cannot search users. Please try again later.";

      try
      {
        var response = await _rcUsers.GetResponse<IOperationResult<ISearchResponse>>(
          ISearchUsersRequest.CreateObj(text));

        if (response.Message.IsSuccess)
        {
          users.AddRange(
            response.Message.Body.Entities.Select(
              p => new SearchResultInfo
              {
                Type = SearchResultObjectType.User,
                Id = p.Id,
                Value = p.Value
              }).ToList());

          return users;
        }

        _logger.LogWarning(errorMessage);
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, errorMessage);
      }
      errors.Add(errorMessage);

      return users;
    }

    private async Task<List<SearchResultInfo>> SearchNewsAsync(string text, List<string> errors)
    {
      List<SearchResultInfo> news = new();

      string errorMessage = "Cannot search news. Please try again later.";

      try
      {
        var response = await _rcNews.GetResponse<IOperationResult<ISearchResponse>>(
          ISearchNewsRequest.CreateObj(text));

        if (response.Message.IsSuccess)
        {
          news.AddRange(
            response.Message.Body.Entities.Select(
              n => new SearchResultInfo
              {
                Type = SearchResultObjectType.News,
                Id = n.Id,
                Value = n.Value
              }).ToList());

          return news;
        }

        _logger.LogWarning(errorMessage);
      }
      catch (Exception exc)
      {
        _logger.LogError(exc, errorMessage);
      }
      errors.Add(errorMessage);

      return news;
    }

    #endregion

    public SearchCommand(
      IRequestClient<ISearchProjectsRequest> rcProjects,
      IRequestClient<ISearchUsersRequest> rcUsers,
      IRequestClient<ISearchDepartmentsRequest> rcDepartments,
      IRequestClient<ISearchNewsRequest> rcNews,
      ILogger<SearchCommand> logger)
    {
      _rcProjects = rcProjects;
      _rcUsers = rcUsers;
      _rcDepartments = rcDepartments;
      _rcNews = rcNews;
      _logger = logger;
    }

    public async Task<SearchResponse> ExecuteAsync(string text, SearchFilter filter)
    {
      List<string> errors = new();

      List<SearchResultInfo> response = new();

      Task<List<SearchResultInfo>> projectTask = filter.IncludeAll || filter.IncludeProjects
        ? SearchProjectsAsync(text, errors) : Task.FromResult(null as List<SearchResultInfo>);

      Task<List<SearchResultInfo>> userTask = filter.IncludeAll || filter.IncludeUsers
        ? SearchUsersAsync(text, errors) : Task.FromResult(null as List<SearchResultInfo>);

      Task<List<SearchResultInfo>> departmentTask = filter.IncludeAll || filter.IncludeDepartments
        ? SearchDepartmentsAsync(text, errors) : Task.FromResult(null as List<SearchResultInfo>);

      Task<List<SearchResultInfo>> newsTask = filter.IncludeAll || filter.IncludeNews
        ? SearchNewsAsync(text, errors) : Task.FromResult(null as List<SearchResultInfo>);

      await Task.WhenAll(projectTask, userTask, departmentTask, newsTask);

      List<SearchResultInfo> projects = await projectTask;
      List<SearchResultInfo> departments = await departmentTask;
      List<SearchResultInfo> users = await userTask;
      List<SearchResultInfo> news = await newsTask;

      if (projects is not null)
      {
        response.AddRange(projects);
      }

      if (users is not null)
      {
        response.AddRange(users);
      }

      if (departments is not null)
      {
        response.AddRange(departments);
      }

      if (news is not null)
      {
        response.AddRange(news);
      }

      return new SearchResponse
      {
        Items = response,
        Errors = errors
      };
    }
  }
}
