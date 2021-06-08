using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Company;
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchService.Bussines.Commands.Search
{
    public class SearchCommand : ISearchCommand
    {
        private IRequestClient<ISearchProjectsRequest> _rcProjects;
        private IRequestClient<ISearchUsersRequests> _rcUsers;
        private IRequestClient<ISearchDepartmentsRequests> _rcDepartments;
        private ILogger<SearchCommand> _logger;

        #region requests

        private List<SearchResultInfo> SearchProjects(string text, List<string> errors)
        {
            List<SearchResultInfo> projects = new();

            string errorMessage = "Cannot search projects. Please try again later.";

            try
            {
                var response = _rcProjects.GetResponse<IOperationResult<ISearchResponse>>(
                    ISearchProjectsRequest.CreateObj(text)).Result;

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

                errors.Add(errorMessage);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, errorMessage);

                errors.Add(errorMessage);
            }

            return projects;
        }

        private List<SearchResultInfo> SearchDepartments(string text, List<string> errors)
        {
            List<SearchResultInfo> departments = new();

            string errorMessage = "Cannot search departments. Please try again later.";

            try
            {
                var response = _rcDepartments.GetResponse<IOperationResult<ISearchResponse>>(
                    ISearchDepartmentsRequests.CreateObj(text)).Result;

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

                errors.Add(errorMessage);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, errorMessage);

                errors.Add(errorMessage);
            }

            return departments;
        }

        private List<SearchResultInfo> SearchUsers(string text, List<string> errors)
        {
            List<SearchResultInfo> users = new();

            string errorMessage = "Cannot search users. Please try again later.";

            try
            {
                var response = _rcUsers.GetResponse<IOperationResult<ISearchResponse>>(
                    ISearchUsersRequests.CreateObj(text)).Result;

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

                errors.Add(errorMessage);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, errorMessage);

                errors.Add(errorMessage);
            }

            return users;
        }

        #endregion

        public SearchCommand(
            IRequestClient<ISearchProjectsRequest> rcProjects,
            IRequestClient<ISearchUsersRequests> rcUsers,
            IRequestClient<ISearchDepartmentsRequests> rcDepartments,
            ILogger<SearchCommand> logger)
        {
            _rcProjects = rcProjects;
            _rcUsers = rcUsers;
            _rcDepartments = rcDepartments;
            _logger = logger;
        }

        public SearchResponse Execute(string text, SearchFilter filter)
        {
            List<string> errors = new();

            List<SearchResultInfo> response = new();

            if (filter.IncludeAll || filter.IsIncludeProjects)
            {
                response.AddRange(SearchProjects(text, errors));
            }

            if (filter.IncludeAll || filter.IsIncludeUsers)
            {
                response.AddRange(SearchUsers(text, errors));
            }

            if (filter.IncludeAll || filter.IsIncludeDepartments)
            {
                response.AddRange(SearchDepartments(text, errors));
            }

            return new SearchResponse
            {
                Items = response,
                Errors = errors
            };
        }
    }
}
