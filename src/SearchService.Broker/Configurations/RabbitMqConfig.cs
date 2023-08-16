using DigitalOffice.Models.Broker.Requests.Wiki;
using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Requests.News;
using LT.DigitalOffice.Models.Broker.Requests.Office;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;

namespace LT.DigitalOffice.SearchService.Broker.Configurations;

public class RabbitMqConfig : BaseRabbitMqConfig
{
  #region request endpoints

  [AutoInjectRequest(typeof(ISearchDepartmentsRequest))]
  public string SearchDepartmentsEndpoint { get; init; }

  [AutoInjectRequest(typeof(ISearchNewsRequest))]
  public string SearchNewsEndpoint { get; init; }

  [AutoInjectRequest(typeof(ISearchOfficesRequest))]
  public string SearchOfficesEndpoint { get; init; }

  [AutoInjectRequest(typeof(ISearchProjectsRequest))]
  public string SearchProjectsEndpoint { get; init; }

  [AutoInjectRequest(typeof(ISearchUsersRequest))]
  public string SearchUsersEndpoint { get; init; }

  [AutoInjectRequest(typeof(ISearchWikiRequest))]
  public string SearchWikiEndpoint { get; init; }

  #endregion
}
