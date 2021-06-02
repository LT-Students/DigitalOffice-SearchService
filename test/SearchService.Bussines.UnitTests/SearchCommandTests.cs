using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Responses.Project;
using LT.DigitalOffice.SearchService.Models.Dto.Enums;
using LT.DigitalOffice.SearchService.Models.Dto.Models;
using LT.DigitalOffice.SearchService.Models.Dto.Requests;
using LT.DigitalOffice.SearchService.Models.Dto.Responses;
using LT.DigitalOffice.UnitTestKernel;
using MassTransit;
using Moq;
using Moq.AutoMock;
using NUnit.Framework;
using SearchService.Bussines.Commands.Search;
using SearchService.Bussines.Commands.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchService.Bussines.UnitTests
{
    public class SearchCommandTests
    {
        private AutoMocker _mocker;
        private ISearchCommand _command;

        List<SearchInfo> _projects;
        List<SearchResultInfo> _result;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
            _command = _mocker.CreateInstance<SearchCommand>();

            _projects = new()
            {
                new SearchInfo(Guid.NewGuid(), "Project1"),
                new SearchInfo(Guid.NewGuid(), "Project3"),
                new SearchInfo(Guid.NewGuid(), "Project2"),
            };

            _result = new()
            {
                new SearchResultInfo
                {
                    Type = SearchResultObjectType.Project,
                    Id = _projects[0].Id,
                    Value = _projects[0].Value
                },
                new SearchResultInfo
                {
                    Type = SearchResultObjectType.Project,
                    Id = _projects[1].Id,
                    Value = _projects[1].Value
                },
                new SearchResultInfo
                {
                    Type = SearchResultObjectType.Project,
                    Id = _projects[2].Id,
                    Value = _projects[2].Value
                },
            };

            var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchProjectsResponse>>>();
            _brokerSearchResponseMock
                .Setup(x => x.Message.Body.Projects)
                .Returns(_projects);

            _brokerSearchResponseMock
                .Setup(x => x.Message.IsSuccess)
                .Returns(true);
            _brokerSearchResponseMock
                .Setup(x => x.Message.Errors)
                .Returns(null as List<string>);

            _mocker
                .Setup<IRequestClient<ISearchProjectsRequest>, Task<Response<IOperationResult<ISearchProjectsResponse>>>>(
                    x => x.GetResponse<IOperationResult<ISearchProjectsResponse>>(
                        It.IsAny<object>(), default, default))
                .Returns(Task.FromResult(_brokerSearchResponseMock.Object));
        }

        [Test]
        public void ShouldSuccesfulFindProjects()
        {
            var expectedResponse = new SearchResponse
            {
                Items = _result,
                Errors = new List<string>()
            };

            SerializerAssert.AreEqual(expectedResponse, _command.Execute("Project", new SearchFilter { IncludeProjects = true } ));
        }

        [Test]
        public void ShouldReturnEmptyListOfItemsWhenRequestIsNotSuccessful()
        {
            var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchProjectsResponse>>>();
            _brokerSearchResponseMock
                .Setup(x => x.Message.Body.Projects)
                .Returns(_projects);

            _brokerSearchResponseMock
                .Setup(x => x.Message.IsSuccess)
                .Returns(false);
            _brokerSearchResponseMock
                .Setup(x => x.Message.Errors)
                .Returns(null as List<string>);

            _mocker
                .Setup<IRequestClient<ISearchProjectsRequest>, Task<Response<IOperationResult<ISearchProjectsResponse>>>>(
                    x => x.GetResponse<IOperationResult<ISearchProjectsResponse>>(
                        It.IsAny<object>(), default, default))
                .Returns(Task.FromResult(_brokerSearchResponseMock.Object));

            var expectedResponse = new SearchResponse
            {
                Items = new List<SearchResultInfo>(),
                Errors = new List<string> { "Cannot search projects. Please try again later." }
            };

            SerializerAssert.AreEqual(expectedResponse, _command.Execute("Project", new SearchFilter { IncludeProjects = true }));
        }

        [Test]
        public void ShouldReturnEmptyListOfItemsWhenRequestThrowException()
        {
            _mocker
                .Setup<IRequestClient<ISearchProjectsRequest>, Task<Response<IOperationResult<ISearchProjectsResponse>>>>(
                    x => x.GetResponse<IOperationResult<ISearchProjectsResponse>>(
                        It.IsAny<object>(), default, default))
                .Throws(new Exception());

            var expectedResponse = new SearchResponse
            {
                Items = new List<SearchResultInfo>(),
                Errors = new List<string> { "Cannot search projects. Please try again later." }
            };

            SerializerAssert.AreEqual(expectedResponse, _command.Execute("Project", new SearchFilter { IncludeProjects = true }));
        }
    }
}