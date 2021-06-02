using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Responses.Project;
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
        private ILogger<SearchCommand> _logger;

        private List<SearchResultInfo> SearchProjects(string text, List<string> errors)
        {
            List<SearchResultInfo> projects = new();

            string errorMessage = "Cannot search projects. Please try again later.";

            try
            {
                var response = _rcProjects.GetResponse<IOperationResult<ISearchProjectsResponse>>(
                    ISearchProjectsRequest.CreateObj(text)).Result;

                if (response.Message.IsSuccess)
                {
                    projects.AddRange(
                        response.Message.Body.Projects.Select(
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
                _logger.LogWarning(exc, errorMessage);

                errors.Add(errorMessage);
            }

            return projects;
        }

        public SearchCommand(
            IRequestClient<ISearchProjectsRequest> rcProjects,
            ILogger<SearchCommand> logger)
        {
            _rcProjects = rcProjects;
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

            return new SearchResponse
            {
                Items = response,
                Errors = errors
            };
        }
    }
}
