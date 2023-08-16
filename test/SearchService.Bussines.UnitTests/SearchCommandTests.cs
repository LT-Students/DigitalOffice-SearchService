//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
//using LT.DigitalOffice.Models.Broker.Models;
//using LT.DigitalOffice.Models.Broker.Requests.Department;
//using LT.DigitalOffice.Models.Broker.Requests.Project;
//using LT.DigitalOffice.Models.Broker.Requests.User;
//using LT.DigitalOffice.Models.Broker.Responses.Search;
//using LT.DigitalOffice.SearchService.Models.Dto.Enums;
//using LT.DigitalOffice.SearchService.Models.Dto.Models;
//using LT.DigitalOffice.SearchService.Models.Dto.Requests;
//using LT.DigitalOffice.SearchService.Models.Dto.Responses;
//using LT.DigitalOffice.UnitTestKernel;
//using MassTransit;
//using Moq;
//using Moq.AutoMock;
//using NUnit.Framework;
//using SearchService.Bussines.Commands.Search;
//using SearchService.Bussines.Commands.Search.Interfaces;

//namespace SearchService.Bussines.UnitTests
//{
//  public class SearchCommandTests
//  {
//    private AutoMocker _mocker;
//    private ISearchCommand _command;

//    List<SearchInfo> entities;
//    List<SearchResultInfo> _result;

//    private void ProjectsSetUp()
//    {
//      entities = new()
//      {
//        new SearchInfo(Guid.NewGuid(), "Project1"),
//        new SearchInfo(Guid.NewGuid(), "Project3"),
//        new SearchInfo(Guid.NewGuid(), "Project2"),
//      };

//      _result = new()
//      {
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.Project,
//          Id = entities[0].Id,
//          Value = entities[0].Value
//        },
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.Project,
//          Id = entities[1].Id,
//          Value = entities[1].Value
//        },
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.Project,
//          Id = entities[2].Id,
//          Value = entities[2].Value
//        },
//      };

//      var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchResponse>>>();
//      _brokerSearchResponseMock
//          .Setup(x => x.Message.Body.Entities)
//          .Returns(entities);

//      _brokerSearchResponseMock
//          .Setup(x => x.Message.IsSuccess)
//          .Returns(true);
//      _brokerSearchResponseMock
//          .Setup(x => x.Message.Errors)
//          .Returns(null as List<string>);

//      _mocker
//          .Setup<IRequestClient<ISearchProjectsRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//              x => x.GetResponse<IOperationResult<ISearchResponse>>(
//                  It.IsAny<object>(), default, default))
//          .Returns(Task.FromResult(_brokerSearchResponseMock.Object));
//    }

//    private void UsersSetUp()
//    {
//      entities = new()
//      {
//        new SearchInfo(Guid.NewGuid(), "User1"),
//        new SearchInfo(Guid.NewGuid(), "User2"),
//        new SearchInfo(Guid.NewGuid(), "User3"),
//      };

//      _result = new()
//      {
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.User,
//          Id = entities[0].Id,
//          Value = entities[0].Value
//        },
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.User,
//          Id = entities[1].Id,
//          Value = entities[1].Value
//        },
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.User,
//          Id = entities[2].Id,
//          Value = entities[2].Value
//        },
//      };

//      var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchResponse>>>();
//      _brokerSearchResponseMock
//          .Setup(x => x.Message.Body.Entities)
//          .Returns(entities);

//      _brokerSearchResponseMock
//          .Setup(x => x.Message.IsSuccess)
//          .Returns(true);
//      _brokerSearchResponseMock
//          .Setup(x => x.Message.Errors)
//          .Returns(null as List<string>);

//      _mocker
//          .Setup<IRequestClient<ISearchUsersRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//              x => x.GetResponse<IOperationResult<ISearchResponse>>(
//                  It.IsAny<object>(), default, default))
//          .Returns(Task.FromResult(_brokerSearchResponseMock.Object));
//    }

//    private void DepartmetnsSetUp()
//    {
//      entities = new()
//      {
//        new SearchInfo(Guid.NewGuid(), "Department1"),
//        new SearchInfo(Guid.NewGuid(), "Department2"),
//        new SearchInfo(Guid.NewGuid(), "Department3"),
//      };

//      _result = new()
//      {
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.Department,
//          Id = entities[0].Id,
//          Value = entities[0].Value
//        },
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.Department,
//          Id = entities[1].Id,
//          Value = entities[1].Value
//        },
//        new SearchResultInfo
//        {
//          Type = SearchResultObjectType.Department,
//          Id = entities[2].Id,
//          Value = entities[2].Value
//        },
//      };

//      var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchResponse>>>();
//      _brokerSearchResponseMock
//          .Setup(x => x.Message.Body.Entities)
//          .Returns(entities);

//      _brokerSearchResponseMock
//          .Setup(x => x.Message.IsSuccess)
//          .Returns(true);
//      _brokerSearchResponseMock
//          .Setup(x => x.Message.Errors)
//          .Returns(null as List<string>);

//      _mocker
//          .Setup<IRequestClient<ISearchDepartmentsRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//              x => x.GetResponse<IOperationResult<ISearchResponse>>(
//                  It.IsAny<object>(), default, default))
//          .Returns(Task.FromResult(_brokerSearchResponseMock.Object));
//    }

//    [SetUp]
//    public void SetUp()
//    {
//      _mocker = new AutoMocker();
//      _command = _mocker.CreateInstance<SearchCommand>();
//    }

//    [Test]
//    public async Task ShouldSuccesfulFindProjects()
//    {
//      ProjectsSetUp();

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = _result,
//        Errors = new List<string>()
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("Project", new SearchFilter { IncludeProjects = true }));
//      _mocker.Verify<IRequestClient<ISearchProjectsRequest>>(x =>
//          x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//          Times.Once);
//    }

//    [Test]
//    public async Task ShouldReturnEmptyListOfItemsWhenProjectsRequestIsNotSuccessful()
//    {
//      var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchResponse>>>();
//      _brokerSearchResponseMock
//        .Setup(x => x.Message.Body.Entities)
//        .Returns(entities);

//      _brokerSearchResponseMock
//        .Setup(x => x.Message.IsSuccess)
//        .Returns(false);
//      _brokerSearchResponseMock
//        .Setup(x => x.Message.Errors)
//        .Returns(null as List<string>);

//      _mocker
//        .Setup<IRequestClient<ISearchProjectsRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//          x => x.GetResponse<IOperationResult<ISearchResponse>>(
//            It.IsAny<object>(), default, default))
//        .Returns(Task.FromResult(_brokerSearchResponseMock.Object));

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = new List<SearchResultInfo>(),
//        Errors = new List<string> { "Cannot search projects. Please try again later." }
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("Project", new SearchFilter { IncludeProjects = true }));
//      _mocker.Verify<IRequestClient<ISearchProjectsRequest>>(x =>
//        x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//        Times.Once);
//    }

//    [Test]
//    public async Task ShouldReturnEmptyListOfItemsWhenProjectsRequestThrowException()
//    {
//      _mocker
//        .Setup<IRequestClient<ISearchProjectsRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//          x => x.GetResponse<IOperationResult<ISearchResponse>>(
//            It.IsAny<object>(), default, default))
//        .Throws(new Exception());

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = new List<SearchResultInfo>(),
//        Errors = new List<string> { "Cannot search projects. Please try again later." }
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("Project", new SearchFilter { IncludeProjects = true }));
//      _mocker.Verify<IRequestClient<ISearchProjectsRequest>>(x =>
//        x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//        Times.Once);
//    }

//    [Test]
//    public async Task ShouldSuccesfulFindUsers()
//    {
//      UsersSetUp();

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = _result,
//        Errors = new List<string>()
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("User", new SearchFilter { IncludeUsers = true }));
//      _mocker.Verify<IRequestClient<ISearchUsersRequest>>(x =>
//        x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//        Times.Once);
//    }

//    [Test]
//    public async Task ShouldReturnEmptyListOfItemsWhenUsersRequestIsNotSuccessful()
//    {
//      var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchResponse>>>();
//      _brokerSearchResponseMock
//        .Setup(x => x.Message.Body.Entities)
//        .Returns(entities);

//      _brokerSearchResponseMock
//        .Setup(x => x.Message.IsSuccess)
//        .Returns(false);
//      _brokerSearchResponseMock
//        .Setup(x => x.Message.Errors)
//        .Returns(null as List<string>);

//      _mocker
//        .Setup<IRequestClient<ISearchUsersRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//          x => x.GetResponse<IOperationResult<ISearchResponse>>(
//            It.IsAny<object>(), default, default))
//        .Returns(Task.FromResult(_brokerSearchResponseMock.Object));

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = new List<SearchResultInfo>(),
//        Errors = new List<string> { "Cannot search users. Please try again later." }
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("User", new SearchFilter { IncludeUsers = true }));
//      _mocker.Verify<IRequestClient<ISearchUsersRequest>>(x =>
//          x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//          Times.Once);
//    }

//    [Test]
//    public async Task ShouldReturnEmptyListOfItemsWhenUsersRequestThrowException()
//    {
//      _mocker
//        .Setup<IRequestClient<ISearchUsersRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//          x => x.GetResponse<IOperationResult<ISearchResponse>>(
//            It.IsAny<object>(), default, default))
//        .Throws(new Exception());

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = new List<SearchResultInfo>(),
//        Errors = new List<string> { "Cannot search users. Please try again later." }
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("User", new SearchFilter { IncludeUsers = true }));
//      _mocker.Verify<IRequestClient<ISearchUsersRequest>>(x =>
//          x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//          Times.Once);
//    }

//    [Test]
//    public async Task ShouldSuccesfulFindDepartments()
//    {
//      DepartmetnsSetUp();

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = _result,
//        Errors = new List<string>()
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("Department", new SearchFilter { IncludeDepartments = true }));
//      _mocker.Verify<IRequestClient<ISearchDepartmentsRequest>>(x =>
//        x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//        Times.Once);
//    }

//    [Test]
//    public async Task ShouldReturnEmptyListOfItemsWhenDepartmentsRequestIsNotSuccessful()
//    {
//      var _brokerSearchResponseMock = new Mock<Response<IOperationResult<ISearchResponse>>>();
//      _brokerSearchResponseMock
//        .Setup(x => x.Message.Body.Entities)
//        .Returns(entities);

//      _brokerSearchResponseMock
//        .Setup(x => x.Message.IsSuccess)
//        .Returns(false);
//      _brokerSearchResponseMock
//        .Setup(x => x.Message.Errors)
//        .Returns(null as List<string>);

//      _mocker
//        .Setup<IRequestClient<ISearchDepartmentsRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//          x => x.GetResponse<IOperationResult<ISearchResponse>>(
//            It.IsAny<object>(), default, default))
//        .Returns(Task.FromResult(_brokerSearchResponseMock.Object));

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = new List<SearchResultInfo>(),
//        Errors = new List<string> { "Cannot search departments. Please try again later." }
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("Project", new SearchFilter { IncludeDepartments = true }));
//      _mocker.Verify<IRequestClient<ISearchDepartmentsRequest>>(x =>
//        x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//        Times.Once);
//    }

//    [Test]
//    public async Task ShouldReturnEmptyListOfItemsWhenDepartmentsRequestThrowException()
//    {
//      _mocker
//        .Setup<IRequestClient<ISearchDepartmentsRequest>, Task<Response<IOperationResult<ISearchResponse>>>>(
//          x => x.GetResponse<IOperationResult<ISearchResponse>>(
//            It.IsAny<object>(), default, default))
//          .Throws(new Exception());

//      var expectedResponse = new SearchResultResponse
//      {
//        Items = new List<SearchResultInfo>(),
//        Errors = new List<string> { "Cannot search departments. Please try again later." }
//      };

//      SerializerAssert.AreEqual(expectedResponse, await _command.ExecuteAsync("Project", new SearchFilter { IncludeDepartments = true }));
//      _mocker.Verify<IRequestClient<ISearchDepartmentsRequest>>(x =>
//        x.GetResponse<IOperationResult<ISearchResponse>>(It.IsAny<object>(), default, default),
//        Times.Once);
//    }
//  }
//}
